using System;
using INOA.StockQuoteAlert.Domain;
using INOA.StockQuoteAlert.Infra;

namespace INOA.StockQuoteAlert.ApplicationWorker
{
    public class QuoteService: IQuoteService
    {
		private readonly ILogger<QuoteService> _logger;

		private readonly IAlphaVantageConnector _connector;
        private readonly IMailService _mailService;

		public QuoteService(ILogger<QuoteService> logger, IAlphaVantageConnector connector, IMailService mailService)
		{
			_logger = logger;
			_connector = connector;
			_mailService = mailService;
        }

		public async void Monitoring(string quoteCode, decimal sellPrice, decimal buyPrice)
		{
			_logger.LogInformation("QuoteService.Monitoring quote {quote}...", quoteCode);
			GlobalQuote quote = await GetQuote(quoteCode);
			QuoteDecisor(quote, sellPrice, buyPrice);
        }


        private async Task<GlobalQuote> GetQuote(string quoteCode)
		{
			GlobalQuote quote =  await _connector.GetGlobalQuoteAsync(quoteCode);
			_logger.LogInformation("QuoteService.GetQuote quote {quote} - price: {price}", quoteCode, quote.price);
            return quote;
		}

        private void QuoteDecisor(GlobalQuote quote, decimal sellPrice, decimal buyPrice)
        {

            if (Convert.ToDecimal(quote.price) >= sellPrice)
			{
				_logger.LogInformation("QuoteService.QuoteDecisor - price: {price} - Decisor Alert to Sell", quote.price);
				_mailService.SendAlertSellQuoteAsync("", "", "");

			}


			if (Convert.ToDecimal(quote.price) < buyPrice)
				{
					_logger.LogInformation("QuoteService.QuoteDecisor  - price: {price} - Decisor Alert to Sell", quote.price);
					_mailService.SendAlertBuyQuoteAsync("", "", "");

            }
		}


    }
}

