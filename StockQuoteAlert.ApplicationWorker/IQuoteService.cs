using System;
namespace INOA.StockQuoteAlert.ApplicationWorker
{
	public interface IQuoteService
	{
        public void Monitoring(string _quoteCode, decimal sellPrice, decimal buyPrice);


    }
}

