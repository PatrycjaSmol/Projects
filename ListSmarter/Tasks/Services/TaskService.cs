using ToDoList.Tasks.Services.ValidationService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Tasks.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITasksValidationService _validationTasksService;

        public TaskService(ITaskRepository taskRepository, ITasksValidationService validationTasksService)

        {
            _validationTasksService = validationTasksService;
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskSv>> GetAll()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<TaskSv> GetAsync(int id)
        {
            return await _validationTasksService.GetValidation(id);
        }

        public async Task<List<TaskSv>> GetAllByBucketAsync(int bucketId)
        {
            await _validationTasksService.GetAllByBucket(bucketId);
            return await _taskRepository.GetAllByBucketAsync(bucketId);
        }
        public async Task<TaskSv> Create(TaskSv taskSv)
        {
            await _validationTasksService.CreateValidation(taskSv);
            return await _taskRepository.CreateAsync(taskSv);
        }

        public async Task<TaskSv> Update(TaskSv task)
        {
            await _validationTasksService.UpdateValidation(task);
            return await _taskRepository.UpdateAsync(task);
        }

        public async Task Delete(int id)
        {
            await _validationTasksService.DeleteValidation(id);
            await _taskRepository.DeleteAsync(id);
        }
    }
}

