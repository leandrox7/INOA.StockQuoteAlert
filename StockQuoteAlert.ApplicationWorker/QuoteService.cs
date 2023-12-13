using System;
using INOA.StockQuoteAlert.Domain;
using INOA.StockQuoteAlert.Infra;

namespace INOA.StockQuoteAlert.ApplicationWorker
{
    public class QuoteService: IQuoteService
    {
		private readonly ILogger<QuoteService> _logger;
		private readonly EmailSettings _emailSettings;
		private readonly IAlphaVantageConnector _connector;
        private readonly IMailService _mailService;
        private decimal lastPrice;

		public QuoteService(ILogger<QuoteService> logger, IAlphaVantageConnector connector, IMailService mailService, EmailSettings emailSettings)
        {
            _logger = logger;
            _connector = connector;
            _mailService = mailService;
            _emailSettings = emailSettings;
            lastPrice = 0;
        }

        public async void Monitoring(string quoteCode, decimal sellPrice, decimal buyPrice)
		{
			try
			{
                _logger.LogInformation("QuoteService.Monitoring quote {quote}...", quoteCode);
                GlobalQuoteData quote = await GetQuote(quoteCode);
                QuoteDecisor(quote, sellPrice, buyPrice);

            }
			catch (Exception ex)
			{
                _logger.LogError(ex, "QuoteService.Monitoring Error {error}",  ex.Message);
            }

        }


        private async Task<GlobalQuoteData> GetQuote(string quoteCode)
		{
            GlobalQuoteData quote =  await _connector.GetGlobalQuoteAsync(quoteCode);
			_logger.LogInformation("QuoteService.GetQuote quote {quote} - price: {price}", quoteCode, quote.Price);
            return quote;
		}

        private void QuoteDecisor(GlobalQuoteData quote, decimal sellPrice, decimal buyPrice)
        {
            if (lastPrice == Convert.ToDecimal(quote.Price))
                return;

            if (Convert.ToDecimal(quote.Price) > sellPrice)
			{
				_logger.LogInformation("QuoteService.QuoteDecisor - price: {price} - Decisor Alert to Sell", quote.Price);
				_mailService.SendAlertSellQuoteAsync(_emailSettings?.DestinationEmail, _emailSettings?.SellTitle, $"SELL QUOTE {quote.Symbol} - PRICE: {quote.Price}");

			}


			if (Convert.ToDecimal(quote.Price) < buyPrice)
				{
					_logger.LogInformation("QuoteService.QuoteDecisor  - price: {price} - Decisor Alert to Sell", quote.Price);
					_mailService.SendAlertBuyQuoteAsync(_emailSettings.DestinationEmail, _emailSettings.BuyTitle, $"BUY QUOTE {quote.Symbol} - PRICE: {quote.Price}");

            }

            lastPrice = Convert.ToDecimal(quote.Price);
		}


    }
}

