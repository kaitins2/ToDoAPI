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
        public ToDoController(IToDoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> Get()
        {
            return Ok(_repository.getAllTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoItem> getToDoById(int id) 
        {
            var todo = _repository.getToDoById(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public ActionResult<ToDoItem> addTodoItem([FromBody]ToDoItem todo)
        {
            _repository.addTodo(todo);
            return CreatedAtAction("Get", new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public ActionResult<ToDoItem> updateTodo(int id, [FromBody] ToDoItem todo)
        {
            var existingTodo = _repository.getToDoById(id);
            if (existingTodo == null)
            {
                return NotFound();
            }
            _repository.updateTodo(id, todo);
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
