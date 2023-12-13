using System;
namespace INOA.StockQuoteAlert.Domain
{
	public class EmailSettings
    { 
        public string? SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string? SmtpUsername { get; set; }
        public string? SmtpPassword { get; set; }
        public string? SenderName { get; set; }
        public string? SenderAddress { get; set; }
        public string? DestinationEmail { get; set; }
        public string? BuyTitle { get; set; }
        public string? SellTitle { get; set; }




    }
}

