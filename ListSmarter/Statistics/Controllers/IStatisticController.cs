using ToDoList.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Statistics.Controllers
{
    public interface IStatisticController
    {
        Task<Dictionary<TaskState, int>> GetAllStatistics();
        Task<Dictionary<TaskState, int>> GetStatisticByBucket(int bucketId);
    }
}
