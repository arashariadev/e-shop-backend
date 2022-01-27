namespace EShop.Api.Models.Profile
{
    public class UpdateProfileViewModel
    {
        public string FirstName { get; }
        
        public string LastName { get; }
 
        public bool ReceiveSpam { get; }
        
        public string PhoneNumber { get; set; }
        
        public bool ReceiveMails { get; set; }
    }
}