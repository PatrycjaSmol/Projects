using ToDoList.Tasks.Services;
using System.Collections.Generic;

namespace ToDoList.Buckets.Services
{
    public class BucketSv
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxNumberOfTask { get; set; } 
        public string Color { get; set; }
        public List<TaskSv> Tasks { get; set; }

    }
}
