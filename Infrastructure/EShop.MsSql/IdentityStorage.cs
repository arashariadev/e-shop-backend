using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Cache;
using EShop.Domain.Identity;
using EShop.Domain.Identity.JWT;
using EShop.Domain.Identity.Oauth2.Facebook;
using EShop.Domain.Identity.Oauth2.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace EShop.MsSql
{
    public class IdentityStorage : IIdentityStorage
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICacheIdentityStorage _cacheStorage;
        private readonly IFacebookService _facebookService;
        private readonly IGoogleService _googleService;

        public IdentityStorage(
            UserManager<UserEntity> userManager,
            JwtSettings jwtSettings,
            RoleManager<IdentityRole> roleManager,
            ICacheIdentityStorage cacheStorage,
            IFacebookService facebookService,
            IGoogleService googleService)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _roleManager = roleManager;
            _cacheStorage = cacheStorage;
            _facebookService = facebookService;
            _googleService = googleService;
        }
        
        public async Task<LoginResult> Login(string email, string password)
        {
            var existUser = await _userManager.FindByEmailAsync(email);
            
            if (existUser != null)
            {
                if (await _userManager.CheckPasswordAsync(existUser, password))
                {
                    var jwtToken = GenerateJwtToken(existUser);
                    var refreshToken = GenerateRefreshToken(existUser.Id);

                    var loginResult = new LoginResult()
                    {
                        JwtToken = await jwtToken,
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

            var newUser = new UserEntity
            {
                UserName = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                ReceiveMails = user.ReceiveMails
            };
            
            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Customer");
            }

            return !result.Succeeded ? IdentityResult.Failed() : IdentityResult.Success;
        }

        public async Task<LoginResult> RefreshToken(string refreshToken)
        {
            var existUserSession = await _cacheStorage.GetCacheValueAsync(refreshToken);

            if (existUserSession == null || existUserSession.IsExpired)
            {
                return default;
            }

            var user = await _userManager.FindByIdAsync(existUserSession.Id);

            var newJwtToken = await GenerateJwtToken(user);

            await _cacheStorage.SetCacheValueAsync(existUserSession.Token, existUserSession);

            var result = new LoginResult()
            {
                JwtToken = newJwtToken,
                RefreshToken = existUserSession.Token
            };

            return result;
        }

        //TODO search in cache first?
        public async Task<LoginResult> FacebookLoginAsync(string accessToken)
        {
            var validationResult = await _facebookService.ValidateAccessTokenAsync(accessToken);

            if (!validationResult.Data.IsValid)
            {
                return default;
            }

            var userInfo = await _facebookService.GetUserInfoByToken(accessToken);

            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var newUser = new UserEntity
                {
                    UserName = userInfo.Email,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Email = userInfo.Email
                };
            
                var result = await _userManager.CreateAsync(newUser);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "Customer");
                }
                else
                {
                    return default;
                }

                var jwt = await GenerateJwtToken(newUser);
                var refresh = GenerateRefreshToken(newUser.Id);

                await _cacheStorage.SetCacheValueAsync(refresh.Token, refresh);

                return new LoginResult
                {
                    JwtToken = jwt,
                    RefreshToken = refresh.Token
                };
            }

            var jwtToken = await GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(user.Id);

            await _cacheStorage.SetCacheValueAsync(refreshToken.Token, refreshToken);

            return new LoginResult
            {
                JwtToken = jwtToken,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<LoginResult> GoogleLoginAsync(string accessToken)
        {
            var userInfo = await _googleService.GetUserInfoFromTokenAsync(accessToken);

            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var newUser = new UserEntity
                {
                    UserName = userInfo.Email,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Email = userInfo.Email
                };
            
                var result = await _userManager.CreateAsync(newUser);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "Customer");
                }
                else
                {
                    return default;
                }

                var jwt = await GenerateJwtToken(newUser);
                var refresh = GenerateRefreshToken(newUser.Id);

                await _cacheStorage.SetCacheValueAsync(refresh.Token, refresh);

                return new LoginResult
                {
                    JwtToken = jwt,
                    RefreshToken = refresh.Token
                };
            }

            var jwtToken = await GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(user.Id);

            await _cacheStorage.SetCacheValueAsync(refreshToken.Token, refreshToken);

            return new LoginResult
            {
                JwtToken = jwtToken,
                RefreshToken = refreshToken.Token
            };
        }

        private async Task<string> GenerateJwtToken(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("id", user.Id)
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);
            
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                
                var role = await _roleManager.FindByNameAsync(userRole);
                
                if (role == null)
                {
                    continue;
                }

                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                    {
                        continue;
                    }

                    claims.Add(roleClaim);
                }
            }
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        private static RefreshToken GenerateRefreshToken(string userId)
        {
            using var rngCryptoServiceProvider = RandomNumberGenerator.Create();
        
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpirationTime = DateTimeOffset.UtcNow.AddDays(1),
                CreatedTime = DateTimeOffset.UtcNow,
                Id = userId
            };
        }
    }
}