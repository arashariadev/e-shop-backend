namespace EShop.Domain.Profile
{
    public class UserProfile
    {
        public UserProfile(string id, string email, string firstName, string lastName, string phoneNumber)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }
        
        public string Id { get; }
        
        public string Email { get; }
        
        public string FirstName { get; private set; }
        
        public string LastName { get; private set; }
        
        public string PhoneNumber { get; private set; }

        public void Update(string firstName, string lastName, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }
    }
}