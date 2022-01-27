namespace EShop.Domain.Smtp;

public class EmailData
{
    public string EmailTo { get; set; }
    
    public string EmailToName { get; set; }
    
    public string EmailSubject { get; set; }
    
    public string EmailBody { get; set; }
}