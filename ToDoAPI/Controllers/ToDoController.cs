using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Model;
using ToDoAPI.RepoLayer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ToDoAPI.Model.DTO;


namespace ToDoAPI.Controllers
{
    [Route("api/todo")]
    [Authorize]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;

        //public ToDoController(IToDoRepository repository)
        //{
        //    _repository = repository;
        //}

        public ToDoController(IToDoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> Get()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var todos = _repository.getAllTodos(userId);
            return Ok(todos);
        }



        [HttpGet("{id}")]
        public ActionResult<ToDoItem> getToDoById(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var todo = _repository.getToDoById(id, userId);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }


        [HttpPost]
        public ActionResult<ToDoItem> addTodoItem([FromBody] CreateToDoDto todoDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var newTodo = new ToDoItem
            {
                Title = todoDto.Title,
                Description = todoDto.Description,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            _repository.addTodo(newTodo);
            return CreatedAtAction("getToDoById", new { id = newTodo.Id }, newTodo);
        }


        [HttpPut("{id}")]
        public ActionResult updateTodo(int id, [FromBody] ToDoItem todo)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var existingTodo = _repository.getToDoById(id, userId);

            if (existingTodo == null)
                return NotFound();

            existingTodo.Title = todo.Title;
            existingTodo.IsCompleted = todo.IsCompleted;

            _repository.updateTodo(id, existingTodo);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public ActionResult deleteTodoItem(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var todo = _repository.getToDoById(id, userId);

            if (todo == null)
                return NotFound();

            _repository.deleteTodo(id);
            return NoContent();
        }

    }
}
