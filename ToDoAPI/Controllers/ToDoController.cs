using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Model;
using ToDoAPI.RepoLayer;

namespace ToDoAPI.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;

        public ToDoController(IToDoRepository repository)
        {
            _repository = repository;
        }

        public ToDoController(IToDoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> Get()
        {
            var todos = _repository.getAllTodos();
            var todoDtos = _mapper.Map<IEnumerable<ToDoItem>>(todos);
            return Ok(todoDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoItem> getToDoById(int id) 
        {
            var todo = _repository.getToDoById(id);
            if (todo == null)
            {
                return NotFound();
            }
            var todosDto = _mapper.Map<ToDoItem>(todo);
            return Ok(todosDto);
        }

        [HttpPost]
        public ActionResult<ToDoItem> addTodoItem([FromBody]ToDoItem todoDto)
        {
            var todo = _mapper.Map<ToDoItem>(todoDto);
            _repository.addTodo(todo);
            
            var CreatedTodoDto = _mapper.Map<ToDoItem>(todo);
            return CreatedAtAction("Get", new { id = todo.Id }, CreatedTodoDto);
        }

        [HttpPut("{id}")]
        public ActionResult<ToDoItem> updateTodo(int id, [FromBody] ToDoItem todo)
        {
            var existingTodo = _repository.getToDoById(id);
            if (existingTodo == null)
            {
                return NotFound();
            }
            _mapper.Map(todo, existingTodo);
            _repository.updateTodo(id, existingTodo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult deleteTodoItem(int id)
        {
            var todo = _repository.getToDoById(id);
            
            if (todo == null)
            {
                return NotFound();
            }
            
            _repository.deleteTodo(id);
            
            return NoContent();
        }
    }
}
