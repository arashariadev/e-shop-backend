using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Cache;
using EShop.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace EShop.MsSql
{
    public class IdentityStorage : IIdentityStorage
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ICacheIdentityStorage _cacheStorage;

        public IdentityStorage(UserManager<UserEntity> userManager, JwtSettings jwtSettings, ICacheIdentityStorage cacheStorage)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _cacheStorage = cacheStorage;
        }
        
        public async Task<LoginResult> Login(string email, string password)
        {
            var existUser = await _userManager.FindByEmailAsync(email);
            
            if (existUser != null)
            {
                if (await _userManager.CheckPasswordAsync(existUser, password))
                {
                    var jwtToken = GenerateJwtToken(existUser.Id);
                    var refreshToken = GenerateRefreshToken(jwtToken, existUser.Id);

                    var loginResult = new LoginResult()
                    {
                        JwtToken = jwtToken,
                        RefreshToken = refreshToken.Token
                    };

                    await _cacheStorage.SetCacheValueAsync(refreshToken.Token, refreshToken);
                    
                    return loginResult;
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
            
            var result = await _userManager.CreateAsync(
                new UserEntity
                {
                    UserName = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                }, user.Password);

            return !result.Succeeded ? IdentityResult.Failed() : IdentityResult.Success;
        }

        public async Task<LoginResult> RefreshToken(string refreshToken)
        {
            var existUserSession = await _cacheStorage.GetCacheValueAsync(refreshToken);

            if (existUserSession == null || existUserSession.IsExpired)
            {
                return default;
            }

            var newJwtToken = GenerateJwtToken(existUserSession.Id);

            existUserSession.LinkedJwtToken = newJwtToken;

            await _cacheStorage.SetCacheValueAsync(existUserSession.Token, existUserSession);

            var result = new LoginResult()
            {
                JwtToken = newJwtToken,
                RefreshToken = existUserSession.Token
            };

            return result;
        }

        private string GenerateJwtToken(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        private static RefreshToken GenerateRefreshToken(string jwtToken, string userId)
        {
            using var rngCryptoServiceProvider = RandomNumberGenerator.Create();
        
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpirationTime = DateTimeOffset.UtcNow.AddDays(1),
                CreatedTime = DateTimeOffset.UtcNow,
                LinkedJwtToken = jwtToken,
                Id = userId
            };
        }
    }
}