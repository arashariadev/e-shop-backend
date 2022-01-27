using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Domain.Smtp;

public interface ISmtpService
{
    Task SendToAll(IEnumerable<string> emailsTo, string subject, string body);

    Task<bool> SendToOne(string emailTo, string emailToName, string subject, string body);
}