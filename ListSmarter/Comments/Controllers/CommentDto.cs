using ToDoList.Tasks.Controllers;

namespace ToDoList.Comments.Controllers
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int TaskId { get; set; }
    }
}
