using System;
using INOA.StockQuoteAlert.Domain;

namespace INOA.StockQuoteAlert.Infra
{
	public interface IAlphaVantageConnector
	{
        public  Task<GlobalQuoteData> GetGlobalQuoteAsync(string quoteCode);


    }
}

