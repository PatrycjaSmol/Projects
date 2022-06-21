using System.Threading.Tasks;

namespace ToDoList.Tasks.Services.ValidationService
{
    public interface ITasksValidationService
    {
        Task<TaskSv> GetValidation(int id);
        Task GetAllByBucket(int bucketId);
        Task CreateValidation(TaskSv taskSv);
        Task UpdateValidation(TaskSv taskSv);
        Task DeleteValidation(int id);
    }
}
