using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.RepoLayer;

namespace ToDoAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
