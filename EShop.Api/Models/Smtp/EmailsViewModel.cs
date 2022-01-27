using System.Collections.Generic;

namespace EShop.Api.Models.Smtp;

public class EmailsViewModel
{
    /// <summary>
    /// Receivers email
    /// </summary>
    public IEnumerable<string> EmailsTo { get; set; }

    /// <summary>
    /// Email subject
    /// </summary>
    public string EmailSubject { get; set; }
    
    /// <summary>
    /// Email body
    /// </summary>
    public string EmailBody { get; set; }
}