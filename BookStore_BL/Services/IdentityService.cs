using Amazon.Runtime.Internal.Util;
using BookStore_BL.Interfaces;
using BookStore_Models.Configurations.Identity;
using BookStore_Models.Models;
using BookStore_Models.Models.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore_BL.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(ILogger<IdentityService> logger, UserManager<User> userManager, SignInManager<User> signInManager, JwtSettings jwtSettings)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
        }
        public async Task<AuthenticationResult> LoginAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                _logger.LogWarning($"User/Password combination is wrong");
                return new AuthenticationResult()
                {
                    isSuccessfull = false,
                    Errors = new string[]
                    {
                        $"User/Password combination is wrong"
                    }
                };
            }

            var validPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!validPassword)
            {
                _logger.LogWarning($"Invalid password");
                return new AuthenticationResult()
                {
                    isSuccessfull = false,
                    Errors = new string[] { $"Invalid password !" }
                };
            }
            return await GenerateAuthenticationResult(user);

        }

        public async Task<AuthenticationResult> RegisterAsync(string userName, string password, string email)
        {
            var exictingUser = await _userManager.FindByNameAsync(userName);

            if (exictingUser != null)
            {
                _logger.LogWarning($"User already exist!");
                return new AuthenticationResult()
                {
                    isSuccessfull = false,
                    Errors = new string[] {$"User already exist!" }
                };
            }

            var user = new User()
            {
                UserName = userName,
                Email = email,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded) {
                _logger.LogWarning($"Failed to register user");
                return new AuthenticationResult()
                {
                    isSuccessfull = false,
                    Errors = new string[] { $"User already exist!" }
                };
            }

            return await GenerateAuthenticationResult(user);

        }

        public async Task<AuthenticationResult> GenerateAuthenticationResult(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("View", "View"),
                }
                ),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            await _signInManager.SignInAsync(user, false);
            return new AuthenticationResult()
            {
                isSuccessfull = true,
                Errors = new string[] { },
                Token = tokenHandler.WriteToken(token)
            };

        }

        public async Task LogOff()
        {
            await _signInManager.SignOutAsync();
        }

    
    }
}
