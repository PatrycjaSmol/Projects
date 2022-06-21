using ToDoList.Common.Enum;
using ToDoList.Tasks.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Statistics.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly ITaskRepository _taskRepository;

        public StatisticService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<Dictionary<TaskState, int>> GetAllStatistic()
        {
            return (await _taskRepository.GetAllAsync())
                .GroupBy(x => x.State)
                .ToDictionary(x => x.Key, x => x.Count());
        }

        public async Task<Dictionary<TaskState, int>> GetStatisticByBucket(int bucketId)
        {
            return (await _taskRepository.GetAllByBucketAsync(bucketId))
                .GroupBy(x => x.State)
                .ToDictionary(x => x.Key, x => x.Count());
        }
    }
}
