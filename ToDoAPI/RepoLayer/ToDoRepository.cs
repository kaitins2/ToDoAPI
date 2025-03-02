using ToDoAPI.Data;
using ToDoAPI.Model;

namespace ToDoAPI.RepoLayer
{
    public class ToDoRepository: IToDoRepository
    {
        private readonly ToDoDbContext _context;
        public ToDoRepository(ToDoDbContext context)
        {
            _context = context;
        }

        public List<ToDoItem> getAllTodos()
        {
            return _context.ToDoItems.ToList();
        }

        public ToDoItem getToDoById(int Id) 
        {
            return _context.ToDoItems.Find(Id);
        }

        public void addTodo(ToDoItem todo) 
        {
            _context.ToDoItems.Add(todo);
            _context.SaveChanges();
        }

        public void updateTodo(int id, ToDoItem todo)
        {
            _context.ToDoItems.Update(todo);
            _context.SaveChanges();
        }

        public void deleteTodo(int Id) 
        {
            var todo = _context.ToDoItems.Find(Id);
            if (todo != null)
            {
                _context.ToDoItems.Remove(todo);
                _context.SaveChanges();
            }
        }
    }
}
