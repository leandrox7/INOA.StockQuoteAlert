using System;
using INOA.StockQuoteAlert.Domain;
using INOA.StockQuoteAlert.Infra;
using Microsoft.Extensions.Configuration;

namespace INOA.StockQuoteAlert.ApplicationWorker
{
    public class MailService : IMailService
    {
        private readonly ILogger<MailService> _logger;
        private readonly ISMTPService _smtpService;

		public MailService(ILogger<MailService> logger, ISMTPService smtpService)
		{
            _logger = logger;
            _smtpService = smtpService;

        }
        public async Task SendAlertBuyQuoteAsync(string to, string subject, string body)

		{
            _logger.LogInformation("MailService.SendAlertBuyQuote => Sending email...");
            await _smtpService.SendEmailAsync("destinatario@example.com", "Assunto do E-mail", "Corpo do E-mail");
        }

        public async Task SendAlertSellQuoteAsync(string to, string subject, string body)
        {
            _logger.LogInformation("MailService.SendAlertSellQuote => Sending email...");
            await _smtpService.SendEmailAsync("destinatario@example.com", "Assunto do E-mail", "Corpo do E-mail");


        }
    }
}

