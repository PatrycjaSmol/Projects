using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Tasks.Services
{
    public interface ITaskRepository
    {
        Task<List<TaskSv>> GetAllAsync();
        Task<List<TaskSv>> GetAllByBucketAsync(int id);
        Task<TaskSv> GetAsync(int id);
        Task<TaskSv> CreateAsync(TaskSv task);
        Task<TaskSv> UpdateAsync(TaskSv task);
        Task DeleteAsync(int id);
    }
}