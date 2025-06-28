using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoAPI.Data;
using ToDoAPI.Model;
using ToDoAPI.Model.DTO;

namespace ToDoAPI.RepoLayer
{
    public class AuthRepository: IAuthRepository
    {
        private readonly ToDoDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository(ToDoDbContext context, IConfiguration configuration, ILogger<AuthRepository> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }



        public string GenerateJWTToken(User user, DateTime expiresAt)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var secretKey = _configuration["Jwt:Key"];

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public authenticatedResponseDto Login(string username, string password)
        {
            var user = _context.User.FirstOrDefault(x => x.Username == username);

            if (user == null)
            {
                return null;
            }

            var passwordHasher = new PasswordHasher<User>();
            var verifiedResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (verifiedResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var expires = DateTime.Now.AddHours(24);
            var jwtToken = GenerateJWTToken(user, expires);  

            return new authenticatedResponseDto
            {
                token = jwtToken,
                username = user.Username,
                UserId = user.Id,
                expiresAt = expires
            };
        }
        public async Task<authenticatedResponseDto> RegisterAsync(RegisterUserDto registerUserDto)
        {
            try
            {
                var user = new User
                {
                    Username = registerUserDto.Username,
                    Email = registerUserDto.Email
                };

                var passwordHasher = new PasswordHasher<User>();
                user.PasswordHash = passwordHasher.HashPassword(user, registerUserDto.Password);

                _context.User.Add(user);
                await _context.SaveChangesAsync(); // 👈 async

                var expires = DateTime.Now.AddHours(24);
                var jwtToken = GenerateJWTToken(user, expires);

                return new authenticatedResponseDto
                {
                    token = jwtToken,
                    username = user.Username,
                    UserId = user.Id,
                    expiresAt = expires
                };
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                _logger.LogError(ex, "Error occurred during registration for user {Username}", registerUserDto.Username);
                throw new InvalidOperationException("Registration failed.", ex);
            }
        }

        public bool UserExists(string username)
        {
            var user = _context.User.FirstOrDefault(x => x.Username == username);
            _logger.LogInformation("User {Username} registered successfully with ID {UserId}", user.Username, user.Id);
            return user != null;

        }
    }
}
