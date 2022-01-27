namespace EShop.Api.Models.Identity
{
    public class RegistrationViewModel
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }
        
        public bool ReceiveSpam { get; set; }

        public string Password { get; set; }
        
        public string ConfirmPassword { get; set; }
    }
}