using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToDoList.Tasks.Repositories;

namespace ToDoList.Assignees.Repositories
{
    public class AssigneeDao
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public List<TaskDao> Tasks { get; set; }
    }
}
