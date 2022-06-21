using ToDoList.Tasks.Repositories;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Comments.Repositories
{
    public class CommentDao
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        [Required]
        public int TaskId { get; set; }

        public TaskDao Task { get; set; }
    }
}
