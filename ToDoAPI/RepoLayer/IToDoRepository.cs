using System.Xml.Serialization;
using ToDoAPI.Model;

namespace ToDoAPI.RepoLayer
{
    public interface IToDoRepository
    {
       List<ToDoItem> getAllTodos();
       ToDoItem getToDoById(int id);
       void addTodo(ToDoItem todo);
       void updateTodo(int id, ToDoItem todo);
       void deleteTodo(int id);
    }
}
