using System.Xml.Serialization;
using ToDoAPI.Model;

namespace ToDoAPI.RepoLayer
{
    public interface IToDoRepository
    {
       List<ToDoItem> getAllTodos(int userId);
       ToDoItem getToDoById(int id, int userId);
       void addTodo(ToDoItem todo);
       void updateTodo(int id, ToDoItem todo);
       void deleteTodo(int id);
    }
}
