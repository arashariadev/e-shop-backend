namespace EShop.Api.Models.Smtp;

public class EmailViewmodel
{
    /// <summary>
    /// Receiver email
    /// </summary>
    public string EmailTo { get; set; }
    
    /// <summary>
    /// Receiver name
    /// </summary>
    public string EmailToName { get; set; }
    
    /// <summary>
    /// Subject
    /// </summary>
    public string EmailSubject { get; set; }
    
    /// <summary>
    /// Email body
    /// </summary>
    public string EmailBody { get; set; }
}