using EShop.Domain.Smtp;

namespace EShop.Domain.Identity
{
    public class User
    {
        public User(string firstName, string lastName, string phoneNumber, string email, bool receiveSpam, string password, string confirmationPassword)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            ReceiveSpam = receiveSpam;
            Password = password;
            ConfirmationPassword = confirmationPassword;
        }

        public string FirstName { get; private set; }
        
        public string LastName { get; private set; }
        
        public string PhoneNumber { get; private set; }
        
        public string Email { get; private set; }
        
        public bool ReceiveSpam { get; private set; }
        
        public string Password { get; private set; }
        
        public string ConfirmationPassword { get; private set; }
    }
}