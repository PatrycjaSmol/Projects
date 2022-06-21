using System.Collections.Generic;
using ToDoList.Assignees.Controllers;
using ToDoList.Buckets.Controllers;
using ToDoList.Comments.Controllers;
using ToDoList.Common.Enum;

namespace ToDoList.Tasks.Controllers
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }
        public Priority Priority { get; set; }
        public int BucketId { get; set; }
        public List<CommentDto> Comments { get; set; }
        public List<AssigneeDto> Assignees { get; set; }
        

        public TaskDto()
        {
            Priority = Priority.Normal;
            State = TaskState.ToDo;
            Assignees = new List<AssigneeDto>();
            Comments = new List<CommentDto>();
        }

        //dodatkowy obiekt taskforcreate - ebz assigness, tworze taska, i pozniej na serv + dod assignes. + mapowanie 
    }
}
