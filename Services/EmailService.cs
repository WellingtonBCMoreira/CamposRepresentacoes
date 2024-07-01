using CamposRepresentacoes.Interfaces.Services;
using System.Net.Mail;
using System.Net;

namespace CamposRepresentacoes.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message, string nomeCliente, byte[] attachment = null)
        {
            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            var smtpClient = new SmtpClient(emailSettings.SmtpServer)
            {
                Port = emailSettings.SmtpPort,
                Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,                
            };

            mailMessage.To.Add(toEmail);
            mailMessage.CC.Add("juliocamposrepresentante@gmail.com");            

            if (attachment != null && attachment.Length > 0)
            {
                using (var stream = new MemoryStream(attachment))
                {
                    mailMessage.Attachments.Add(new Attachment(stream, "Pedido_" + nomeCliente + ".pdf", "application/pdf"));
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            else
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
        public async Task SendEmail(string toEmail, string subject, string message, string nomeCliente, byte[] attachment = null)
        {
            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            var smtpClient = new SmtpClient(emailSettings.SmtpServer)
            {
                Port = emailSettings.SmtpPort,
                Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);            

            if (attachment != null && attachment.Length > 0)
            {
                using (var stream = new MemoryStream(attachment))
                {
                    mailMessage.Attachments.Add(new Attachment(stream, "Pedido_" + nomeCliente + ".pdf", "application/pdf"));
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            else
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}


public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
