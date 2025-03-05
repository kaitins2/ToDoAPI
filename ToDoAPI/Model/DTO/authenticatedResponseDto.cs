namespace ToDoAPI.Model.DTO
{
    public class authenticatedResponseDto
    {
        public string token { get; set; }
        public string userId { get; set; }
        public string username { get; set; }
        public DateTime expiresAt { get; set; }
    }
}
