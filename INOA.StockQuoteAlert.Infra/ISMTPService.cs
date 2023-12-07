using System;
namespace INOA.StockQuoteAlert.Infra
{
	public interface ISMTPService
	{
        public  Task SendEmailAsync(string to, string subject, string body);


    }
}

