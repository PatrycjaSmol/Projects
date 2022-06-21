using ToDoList.Assignees.Repositories;
using ToDoList.Buckets.Repositories;
using ToDoList.Buckets.Services;
using ToDoList.Comments.Repositories;
using ToDoList.Common.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Tasks.Repositories
{
    public class TaskDao
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public TaskState State { get; set; }

        [Required]
        public Priority Priority { get; set; }

        public int BucketId { get; set; }

        public BucketDao Bucket { get; set; }

        public List<CommentDao> Comments { get; set; }

        [Range(0, 5)]
        public List<AssigneeDao> Assignees { get; set; }

        //public TaskDao()
        //{
        //    Priority = Priority.Normal;
        //    State = TaskState.ToDo;
        //    Assignees = new List<AssigneeDao>();
        //    Comments = new List<CommentDao>();
        //}
    }
}
