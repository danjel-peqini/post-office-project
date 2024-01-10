using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Helpers.Email
{
    public class EmailService
    {
        private readonly MailSettings _settings;
        public EmailService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;

        }
        public async Task SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress(_settings.From, "UPT");
                var toAddress = new MailAddress(toEmail);

                var smtp = new SmtpClient
                {
                    Host = _settings.Host, // Your SMTP server
                    Port = _settings.Port,                // SMTP port (usually 587 for TLS)
                    EnableSsl = true,          // Enable SSL/TLS
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_settings.From, _settings.Password) // Your email credentials
                };

                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true // Set to true if your email body is HTML
                };

                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
    }
}
