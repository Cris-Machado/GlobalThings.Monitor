using GlobalThings.Domain.Configuration;
using GlobalThings.Domain.Interfaces.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace GlobalThings.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<MonitorConfiguration> _options;

        public EmailService(IOptions<MonitorConfiguration> options)
        {
            _options = options;
        }

        public void SendEmail(string body)
        {
            //var message = new MailMessage(_options.Value.Emailsettings.FromAddress ?? "", _options.Value.Emailsettings.ToAddress ?? "")
            //{
            //    Subject = _options.Value.Emailsettings.Subject ?? "",
            //    Body = body
            //};

            //var smtpClient = new SmtpClient(_options.Value.SmtpSettings.Server)
            //{
            //    Port = _options.Value.SmtpSettings.Port,
            //    Credentials = new NetworkCredential(_options.Value.SmtpSettings.Username, _options.Value.SmtpSettings.Password),
            //    EnableSsl = true
            //};

            //try
            //{
            //    smtpClient.Send(message);
            //    Console.WriteLine("E-mail enviado com sucesso!");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Erro ao enviar o e-mail: {ex.Message}");
            //}

            MailMessage emailMessage = new MailMessage();
            try
            {
                var smtpClient = new SmtpClient(_options.Value.SmtpSettings.Server, _options.Value.SmtpSettings.Port);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 60 * 60;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_options.Value.SmtpSettings.Username, _options.Value.SmtpSettings.Password);

                emailMessage.From = new MailAddress(_options.Value.Emailsettings.FromAddress, _options.Value.Emailsettings.FromName);
                emailMessage.Body = body;
                emailMessage.Subject = _options.Value.Emailsettings.Subject;
                emailMessage.IsBodyHtml = true;
                emailMessage.Priority = MailPriority.Normal;
                emailMessage.To.Add(_options.Value.Emailsettings.ToAddress);

                smtpClient.Send(emailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
