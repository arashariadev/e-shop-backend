using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Domain.Smtp;

public interface ISmtpService
{
    Task<bool> SentToAll(IEnumerable<EmailData> email);

    Task<bool> SentToOne(string emailTo, string emailToName, string subject, string body);
}