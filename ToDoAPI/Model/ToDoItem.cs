using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ToDoAPI.Model
{
    public class ToDoItem
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

    }
    
    
}
