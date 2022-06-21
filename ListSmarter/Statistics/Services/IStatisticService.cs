using ToDoList.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Statistics.Services
{
    public interface IStatisticService
    {
        Task<Dictionary<TaskState, int>> GetAllStatistic();
        Task<Dictionary<TaskState, int>> GetStatisticByBucket(int bucketId);
    }
}
