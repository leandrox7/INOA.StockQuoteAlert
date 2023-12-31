﻿
using System.Net;
using System.Net.Mail;
using INOA.StockQuoteAlert.Domain;
using MailKit.Security;
using MimeKit;

namespace INOA.StockQuoteAlert.Infra
{
	public class SMTPService: ISMTPService
	{
        private readonly EmailSettings _emailSettings;

        public SMTPService(EmailSettings emailSettings)
		{
            _emailSettings = emailSettings;

        }

        
        public async Task SendEmailAsync(string to, string subject, string body)
		{

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderAddress));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                 client.Authenticate(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
                 client.Send(message);
                 client.Disconnect(true);
            }
            
        }

    }
}

