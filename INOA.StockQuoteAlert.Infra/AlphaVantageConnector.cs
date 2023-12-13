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


        public async Task<GlobalQuoteData> GetGlobalQuoteAsync(string quoteCode)
        {
            _logger.LogInformation("StockQuoteAlert.Infra.AlphaVantageConnector.GetQuote");

               var response = await _httpClient.GetAsync($"{_url}query?function=GLOBAL_QUOTE&symbol={quoteCode}.SA&apikey={_apiKey}");

               response.EnsureSuccessStatusCode();

               string stream = await response.Content.ReadAsStringAsync();

               var error = JsonSerializer.Deserialize<ErrorResponse>(stream);

               if(error?.Information != null)
               {
                   throw new Exception(error?.Information);
               }


               GlobalQuoteResponse result = JsonSerializer.Deserialize<GlobalQuoteResponse>(stream) ?? throw new ArgumentException("");
               _logger.LogInformation($"StockQuoteAlert.Infra.AlphaVantageConnector.GetQuote Result price {result?.GlobalQuote?.Price}");

            // caso atinja o limite de requests diários da API, você pode mockar com a linha abaixo!
            //string jsonString = "{ \"Global Quote\": { \"01. symbol\": \"PETR4.SA\", \"02. open\": \"34.5500\", \"03. high\": \"34.6000\", \"04. low\": \"33.8000\", \"05. price\": \"34.0800\", \"06. volume\": \"30371500\", \"07. latest trading day\": \"2023-12-12\", \"08. previous close\": \"34.3600\", \"09. change\": \"-0.2800\", \"10. change percent\": \"-0.8149%\" } }";
           // GlobalQuoteResponse result = JsonSerializer.Deserialize<GlobalQuoteResponse>(jsonString);

               return result.GlobalQuote;

               

        }
    }

     class ErrorResponse
    {
        public string? Information { get; set; }
    }
}

