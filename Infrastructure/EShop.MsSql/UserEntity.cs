using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EShop.MsSql
{
    public class UserEntity : IdentityUser
    {
        [Column(TypeName = "nvarchar(75)")]
        [Required]
        public string FirstName { get; set; }
        
        [Column(TypeName = "nvarchar(75)")]
        [Required]
        public string LastName { get; set; }
        
        public bool ReceiveSpam { get; set; }
    }
}