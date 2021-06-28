using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace EShop.MsSql
{
    public class IdentityStorage : IIdentityStorage
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly JwtSettings _jwtSettings;

        public IdentityStorage(UserManager<UserEntity> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }
        
        public async Task<string> Login(string email, string password)
        {
            var existUser = await _userManager.FindByEmailAsync(email);
            if (existUser != null)
            {
                if (await _userManager.CheckPasswordAsync(existUser, password))
                {
                    return GenerateJwtToken(existUser);
                }
            }

            return null;
        }

        public async Task<IdentityResult> Registration(User user)
        {
            if (user.Password != user.ConfirmationPassword)
            {
                return IdentityResult.Failed();
            }
            
            return await _userManager.CreateAsync(
                new UserEntity
                {
                    UserName = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                }, user.Password);
        }

        private string GenerateJwtToken(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}