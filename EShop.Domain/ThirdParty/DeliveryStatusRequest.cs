using System.Collections.Generic;
using Newtonsoft.Json;

namespace EShop.Domain.ThirdParty;

public class DeliveryStatusRequest
{
    public string ApiKey { get; set; }
    
    public string ModelName { get; set; }
    
    public string CalledMethod { get; set; }
    
    public MethodProperties MethodProperties { get; set; }
}

public class MethodProperties
{
    public Document[] Documents { get; set; }
}

public class Document
{
    public string DocumentNumber { get; set; }

    public string Phone { get; set; }
}