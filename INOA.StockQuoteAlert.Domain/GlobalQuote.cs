using System;
namespace INOA.StockQuoteAlert.Domain
{
	public class GlobalQuote
	{
        string? Symbol { get; set; }
        string? Open { get; set; }
        string? High { get; set; }
        string? Low { get; set; }
       public string? price { get; set; }
        string? Volume { get; set; }
        string? PreviousClose { get; set; }
        string? Change { get; set; }
        string? ChangePercent { get; set; }
    
	}
}

