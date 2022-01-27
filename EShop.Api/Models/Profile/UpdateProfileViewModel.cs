namespace EShop.Api.Models.Profile
{
    public class UpdateProfileViewModel
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        
        public bool ReceiveMails { get; set; }
    }
}