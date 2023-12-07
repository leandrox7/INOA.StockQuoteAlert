using System;
using INOA.StockQuoteAlert.Domain;

namespace INOA.StockQuoteAlert.Infra
{
	public interface IAlphaVantageConnector
	{
        public  Task<GlobalQuote> GetGlobalQuoteAsync(string quoteCode);


    }
}

