namespace EShop.Api.Models.Profile
{
    public class UpdateProfileViewModel
    {
        public string FirstName { get; }
        
        public string LastName { get; }
        
        public string PhoneNumber { get; }
        
        public bool ReceiveSpam { get; }
    }
}