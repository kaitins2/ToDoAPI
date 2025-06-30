using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using ToDoAPI.Model;
using ToDoAPI.Model.DTO;
using ToDoAPI.RepoLayer;

namespace ToDoAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [EnableCors("AllowReactApp")]
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
        public async Task<ActionResult<authenticatedResponseDto>> Register([FromBody] RegisterUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_repository.UserExists(userDto.Username))
            {
                return BadRequest("User already exists");
            }

            // Call the repo method which returns the full authenticated response
            var response = await _repository.RegisterAsync(userDto);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<authenticatedResponseDto>> Login([FromBody] LoginDto loginDto)
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
