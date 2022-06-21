using System.Collections.Generic;
using ToDoList.Assignees.Services;
using ToDoList.Buckets.Repositories;
using ToDoList.Comments.Services;
using ToDoList.Common.Enum;

namespace ToDoList.Tasks.Services
{
    public class TaskSv
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }
        public Priority Priority { get; set; }
        public int BucketId { get; set; }
        public List<CommentSv> Comments { get; set; }
        public List<AssigneeSv> Assignees { get; set; }

        public TaskSv()
        {
            Priority = Priority.Normal;
            State = TaskState.ToDo;
            Assignees = new List<AssigneeSv>();
            Comments = new List<CommentSv>();
        }
    }
}
