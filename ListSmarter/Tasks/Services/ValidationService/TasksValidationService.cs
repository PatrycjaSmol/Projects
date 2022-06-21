using ToDoList.Buckets.Services;
using ToDoList.Common.Exceptions;
using System;
using System.Threading.Tasks;

namespace ToDoList.Tasks.Services.ValidationService
{
    public class TasksValidationService : ITasksValidationService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IBucketService _bucketService;

        public TasksValidationService(ITaskRepository taskRepository, IBucketService bucketService)
        {
            _bucketService = bucketService;
            _taskRepository = taskRepository;
        }

        public async Task<TaskSv> GetValidation(int id)
        {
            var task = await _taskRepository.GetAsync(id);
            if (task == null)
            {
                throw new DataNotFoundException($"There is no task with id {id}.");
            }
            return task;
        }


        public async Task GetAllByBucket(int bucketId)
        {
            var bucket = await _bucketService.GetAsync(bucketId);
            if (bucket == null)
            {
                throw new DataNotFoundException($"There is no bucket with id {bucketId}.");
            }
        }

        public async Task CreateValidation( TaskSv taskSv)
        {
            if (taskSv == null)
            {
                throw new DataNotFoundException($"Couldn't create a task - {nameof(taskSv)} can't be null.");
            }

            var bucketForTask = await _bucketService.GetAsync(taskSv.BucketId);

            if (bucketForTask == null)
            {
                throw new DataNotFoundException(
                    $"Couldn't create a new task assigned to bucket with id {taskSv.BucketId} - bucket doesn't exist.");
            }

            if (bucketForTask.Tasks.Count >= bucketForTask.MaxNumberOfTask)
            {
                throw new DataLimitException($"Maximum number of tasks has been reached. Couldn't add task to bucket.");
            }
        }

        public async Task UpdateValidation(TaskSv taskSv)
        {
            if (taskSv == null)
            {
                throw new DataNotFoundException($"Couldn't update a task - {nameof(taskSv)} can't be null.");
            }

            var taskToUpdate = await _taskRepository.GetAsync(taskSv.Id);
            if (taskToUpdate == null)
            {
                throw new InvalidOperationException($"There is no task with id {taskSv.Id} - nothing to update.");
            }
        }
        public async Task DeleteValidation(int id)
        {
            var task = await _taskRepository.GetAsync(id);
            if (task == null)
            {
                throw new DataNotFoundException($"Couldn't delete task with id: {id} - it doesn't exist.");
            }
        }
    }
}
