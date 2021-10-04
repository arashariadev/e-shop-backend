namespace EShop.Domain.Profile
{
    public class ProfileContext
    {
        public ProfileContext(string firstName, string lastName, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }
        
        public string FirstName { get; }
        
        public string LastName { get; }
        
        public string PhoneNumber { get; }
    }
}