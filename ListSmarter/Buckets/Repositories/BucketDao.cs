using ToDoList.Tasks.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Buckets.Repositories
{
    public class BucketDao
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0, 40)]
        public int MaxNumberOfTask { get; set; }

        [Required]
        public string Color { get; set; }

        public List<TaskDao> Tasks { get; set; }
    }
}
