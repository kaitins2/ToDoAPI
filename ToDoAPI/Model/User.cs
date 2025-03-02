using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>(); 
    }
}
