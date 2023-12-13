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
            try
            {
                _logger.LogInformation("MailService.SendAlertBuyQuote => Sending email...");
                await _smtpService.SendEmailAsync(to, subject, body);
            }
            
            catch(Exception ex)
            {
                _logger.LogError(ex, "MailService.SendAlertBuyQuote => Erro ao enviar email");
            }
        }

        public async Task SendAlertSellQuoteAsync(string to, string subject, string body)
        {
            try
            {
                _logger.LogInformation("MailService.SendAlertSellQuote => Sending email...");
                await _smtpService.SendEmailAsync(to, subject, body);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "MailService.SendAlertSellQuote => Erro ao enviar email");
            }
           


        }
    }
}

