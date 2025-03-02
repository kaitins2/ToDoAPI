using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Model.DTO
{
    public class CreateToDoDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
    }
}
