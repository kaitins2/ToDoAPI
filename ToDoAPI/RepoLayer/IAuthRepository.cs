
using ToDoAPI.Model;
using ToDoAPI.Model.DTO;

namespace ToDoAPI.RepoLayer
{
    public interface IAuthRepository
    {
        authenticatedResponseDto Register(User user);
        authenticatedResponseDto Login(string username, string password);
        bool UserExists(string username);
        string GenerateJWTToken(User user);
        //string GenerateRefreshToken();  Optional for advanced authentication
       // bool ValidateRefreshToken(string refreshToken); // Optional
    }
}
