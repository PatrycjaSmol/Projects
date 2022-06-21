using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Assignees.Services
{
    public interface IAssigneeRepository
    {
        Task<List<AssigneeSv>> GetAllAsync();
        Task<AssigneeSv> GetAsync(int id);
        Task<List<AssigneeSv>> GetAllByTaskAsync(int taskId);
        Task<AssigneeSv> CreateAsync(AssigneeSv assigneeSv);
        Task<AssigneeSv> AddToTaskAsync(AssigneeSv assigneeSv, int taskId);
        Task<AssigneeSv> UpdateAsync(AssigneeSv assigneeSv);
        Task RemoveFromTaskAsync(int id, int taskId);
        Task DeleteAsync(int id);

    }
}
