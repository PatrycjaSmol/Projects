using ToDoList.Tasks.Controllers;
using System.Collections.Generic;

namespace ToDoList.Buckets.Controllers
{
    public class BucketDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxNumberOfTask { get; set; }
        public string Color { get; set; }
        public List<TaskDto> Tasks { get; set; }

        public BucketDto()
        {
            MaxNumberOfTask = 15;
            Color = "fff666";
            Tasks = new List<TaskDto>();
        }
    }
}
