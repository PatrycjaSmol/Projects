using ToDoList.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Tasks.Services
{
    public interface ITaskService
    {
        Task<List<TaskSv>> GetAll();
        Task<List<TaskSv>> GetAllByBucketAsync(int bucketId);
        Task<TaskSv> GetAsync(int id);
        Task<TaskSv> Create(TaskSv task);
        Task<TaskSv> Update(TaskSv task);
        Task Delete(int id);

    }
}
