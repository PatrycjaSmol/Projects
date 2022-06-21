using System.Threading.Tasks;

namespace ToDoList.Assignees.Services.ValidationService
{
    public interface IAssigneesValidationService
    {
        Task<AssigneeSv> GetValidation(int id);
        Task GetAllByTaskValidation(int taskId);
        Task AddToTaskValidation(AssigneeSv assigneeSv, int taskId);
        Task CreateValidation(AssigneeSv assigneeSv);
        Task UpdateValidation(AssigneeSv assigneeSv);
        Task DeleteValidation(int id);
        Task RemoveFromTaskValidation(int id, int taskId);
    }
}
