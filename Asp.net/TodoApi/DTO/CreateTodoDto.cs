using TodoApi.Models;

namespace TodoApi.DTO
{
    public class CreateTodoDto
    {
        public string Title { get; set; } = "";
        public Priority Priority { get; set; }

        public DateTime DueDate { get; set; }
    }
}
