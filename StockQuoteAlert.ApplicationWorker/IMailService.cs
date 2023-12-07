using System;
namespace INOA.StockQuoteAlert.ApplicationWorker
{
    public interface IMailService
	{

        Task SendAlertBuyQuoteAsync(string to, string subject, string body);

        Task SendAlertSellQuoteAsync(string to, string subject, string body);



    }
}

