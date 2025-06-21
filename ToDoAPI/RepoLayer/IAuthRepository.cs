
using ToDoAPI.Model;
using ToDoAPI.Model.DTO;

namespace ToDoAPI.RepoLayer
{
    public interface IAuthRepository
    {
        Task<authenticatedResponseDto> RegisterAsync(RegisterUserDto registerUserDto);
        authenticatedResponseDto Login(string username, string password);
        bool UserExists(string username);
        string GenerateJWTToken(User user, DateTime expiresAt);
        //string GenerateRefreshToken();  Optional for advanced authentication
       // bool ValidateRefreshToken(string refreshToken); // Optional
    }
}
