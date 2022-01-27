namespace EShop.Domain.Smtp;

public class SmtpSettings
{
    public string EmailId { get; set; }
    
    public string Name { get; set; }
    
    public string Password { get; set; }
    
    public string Host { get; set; }
    
    public int Port { get; set; }
    
    public bool UseSsl { get; set; }
}