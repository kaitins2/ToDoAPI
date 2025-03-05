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

        public AuthRepository(ToDoDbContext context)
        {
            _context = context;
        }

        public string GenerateJWTToken(User user)
        {
            var claims  = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: "your_app_name_or_domain", // Typically your app's name or domain
                audience: "your_app_name_or_domain", // Again, typically your app's name or domain
                claims: claims,
                expires: DateTime.Now.AddHours(24), // Set expiration time (e.g., 1 hour)
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public authenticatedResponseDto Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == username);
            
            if(user == null)
            {
                return null;
            }
            
            var passwordHasher = new PasswordHasher<User>();
            var verifiedResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash,password);

            if (verifiedResult == PasswordVerificationResult.Failed) 
            {
                return null;
            }
            
            var jwtToken = GenerateJWTToken(user);

            return new authenticatedResponseDto { token = jwtToken, username = user.Username };
        }
        public authenticatedResponseDto Register(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            _context.SaveChanges();

            var token = GenerateJWTToken(user);
            return new authenticatedResponseDto { token = token, username = user.Username };
        }

        public bool UserExists(string username)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == username);
            return user != null;

        }
    }
}
