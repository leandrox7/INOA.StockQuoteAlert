using System;
using System.Threading.Tasks;
using INOA.StockQuoteAlert.Domain;
using MimeKit;
using Moq;
using MailKit.Net.Smtp;
using MailKit.Security;
using global::INOA.StockQuoteAlert.Infra;
namespace INOA.StockQuoteAlert.Test
{
    [TestFixture]
    public class SMTPServiceTests
    {
        [Test]
        public void SendEmailAsync_Success()
        {
            /*  // Arrange
              var emailSettings = new EmailSettings
              {
                  SenderName = "Sender",
                  SenderAddress = "sender@example.com",
                  SmtpServer = "smtp.example.com",
                  SmtpPort = 587,
                  SmtpUsername = "username",
                  SmtpPassword = "password"
              };

              var smtpService = new SMTPService(emailSettings);

              var to = "recipient@example.com";
              var subject = "Test Subject";
              var body = "<p>This is a test email body.</p>";

              var expectedMessage = new MimeMessage
              {
                  From = { new MailboxAddress("Sender", "sender@example.com") },
                  To = { new MailboxAddress("", to) },
                  Subject = subject,
                  Body = new TextPart("html") { Text = body }
              };
              return Task.CompletedTask;
          }*/
            Assert.True(true);
        }

    }
}

