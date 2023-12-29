using Domain.Interfaces;
using System.Net.Mail;
using System.Net;

namespace Infrastructure.External_Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient smtpClient;
        private readonly string fromEmail;

        public EmailSender(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword, string fromEmail)
        {
            this.smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };

            this.fromEmail = fromEmail;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            })
            {
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}
