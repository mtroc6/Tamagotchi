using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebPWrecover.Services;

namespace Tamagotchi.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly SmtpClient _smtpClient;

        public EmailSender(IOptions<AuthMessageSenderOptions> smtpOptionsAccessor, ILogger<EmailSender> logger)
        {
            _logger = logger;

            var smtpOptions = smtpOptionsAccessor.Value;

            if (smtpOptions == null || string.IsNullOrEmpty(smtpOptions.SmtpServer) || smtpOptions.SmtpPort == 0)
            {
                throw new Exception("SMTP configuration is invalid.");
            }

            _smtpClient = new SmtpClient(smtpOptions.SmtpServer)
            {
                Port = smtpOptions.SmtpPort,
                Credentials = new NetworkCredential(smtpOptions.SmtpUsername, smtpOptions.SmtpPassword),
                EnableSsl = smtpOptions.EnableSsl,
            };
        }


        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("your-email@example.com", "TamagotchiWebApp"),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            try
            {
                await _smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation($"Email to {toEmail} sent successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send email to {toEmail}: {ex.Message}");
                throw;
            }
        }
    }
}
