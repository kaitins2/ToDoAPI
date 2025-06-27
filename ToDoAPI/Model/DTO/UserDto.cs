using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Model.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Email { get; set; }
    }
}
