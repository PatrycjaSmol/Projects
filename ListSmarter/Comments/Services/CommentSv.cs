using ToDoList.Tasks.Services;

namespace ToDoList.Comments.Services
{
    public class CommentSv
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int TaskId { get; set; }

    }
}
