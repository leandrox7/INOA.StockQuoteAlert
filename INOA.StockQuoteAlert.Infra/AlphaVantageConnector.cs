using INOA.StockQuoteAlert.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace INOA.StockQuoteAlert.Infra
{
    public class AlphaVantageConnector: IAlphaVantageConnector
    {
		private readonly ILogger<AlphaVantageConnector> _logger;
        private readonly HttpClient _httpClient;

        private readonly string _apiKey;
        private readonly string _url;


        public AlphaVantageConnector(ILogger<AlphaVantageConnector> logger, IConfiguration configuration, HttpClient httpClient)
		{
			_logger = logger;
            _apiKey = configuration.GetSection("AlphaSettings:API_KEY")?.Value ?? throw new ArgumentException("ApiKey not found");
            _url = configuration.GetSection("AlphaSettings:BaseUrl")?.Value ?? throw new ArgumentException("Url not found");
            _httpClient = httpClient;
        }


        public async Task<GlobalQuote> GetGlobalQuoteAsync(string quoteCode)
        {
            _logger.LogInformation("StockQuoteAlert.Infra.AlphaVantageConnector.GetQuote");

            var response = await _httpClient.GetAsync($"{_url}query?function=TIME_SERIES_WEEKLY_ADJUSTED&symbol={quoteCode}&apikey={_apiKey}");

            response.EnsureSuccessStatusCode();

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
         

                GlobalQuote result = await JsonSerializer.DeserializeAsync<GlobalQuote>(stream) ?? throw new ArgumentException("");
                _logger.LogInformation($"StockQuoteAlert.Infra.AlphaVantageConnector.GetQuote Result price {result?.price}");
                return result;

            }

        }
	}
}

