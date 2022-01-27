using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace EShop.Domain.Smtp;

public class SmtpService : ISmtpService
{
    private readonly SmtpSettings _smtpSettings;

    public SmtpService(SmtpSettings smtpSettings)
    {
        _smtpSettings = smtpSettings;
    }

    public Task<bool> SentToAll(IEnumerable<EmailData> email)
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> SentToOne(string emailTo, string emailToName, string subject, string body)
    {
        try
        {
            var emailMessage = new MimeMessage();

            var emailFrom = new MailboxAddress(_smtpSettings.Name, _smtpSettings.EmailId);
            emailMessage.From.Add(emailFrom);

            var receiver = new MailboxAddress(emailToName, emailTo);
            emailMessage.To.Add(receiver);

            emailMessage.Subject = subject;

            var emailBodyBuilder = new BodyBuilder
            {
                TextBody = body
            };

            emailMessage.Body = emailBodyBuilder.ToMessageBody();

            var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, _smtpSettings.UseSsl);
            await smtpClient.AuthenticateAsync(_smtpSettings.EmailId, _smtpSettings.Password);
            await smtpClient.SendAsync(emailMessage);
            await smtpClient.DisconnectAsync(true);
            smtpClient.Dispose();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}