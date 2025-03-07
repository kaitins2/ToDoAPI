using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Model;
using ToDoAPI.Model.DTO;
using ToDoAPI.RepoLayer;

namespace ToDoAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public ActionResult<RegisterUserDto> Register([FromBody] RegisterUserDto registerDto) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if (_repository.UserExists(registerDto.Username)) 
            {
                return BadRequest("User already exists");
            }

            var user = _mapper.Map<User>(registerDto);
            _repository.Register(user);

            var token = _repository.GenerateJWTToken(user);

            return Ok(new authenticatedResponseDto { token = token, username = user.Username });
        }

        [HttpPost("login")]
        public ActionResult<authenticatedResponseDto> Login([FromBody] LoginDto loginDto) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if (!_repository.UserExists(loginDto.Username)) 
            {
                return Unauthorized("Invalid username or password");
            }

            var authResponse = _repository.Login(loginDto.Username, loginDto.Password);

            if (authResponse == null) 
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(authResponse);
        }
    }
}
