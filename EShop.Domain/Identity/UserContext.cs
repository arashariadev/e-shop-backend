namespace EShop.Domain.Identity
{
    public class UserContext
    {
        public UserContext(string firstName, string lastName, string phoneNumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
        }
        
        public string FirstName { get; }
        
        public string LastName { get; }
        
        public string PhoneNumber { get; }
        
        public string Email { get; }
    }
}