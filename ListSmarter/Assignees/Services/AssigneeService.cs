using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Assignees.Services.ValidationService;

namespace ToDoList.Assignees.Services
{
    public class AssigneeService : IAssigneeService
    {
        private readonly IAssigneeRepository _assigneeRepository;
        private readonly IAssigneesValidationService _validationAssigneeService;

        public AssigneeService(IAssigneeRepository assigneeRepository, IAssigneesValidationService validationAssigneeService)
        {
            _assigneeRepository = assigneeRepository;
            _validationAssigneeService = validationAssigneeService;
        }

        public async Task<List<AssigneeSv>> GetAllAsync()
        {
            return await _assigneeRepository.GetAllAsync();
        }

        public async Task<AssigneeSv> GetAsync(int id)
        {
            await _validationAssigneeService.GetValidation(id);
            return await _assigneeRepository.GetAsync(id);
        }

        public async Task<List<AssigneeSv>> GetAllByTaskAsync(int taskId)
        {
            await _validationAssigneeService.GetAllByTaskValidation(taskId);
            return await _assigneeRepository.GetAllByTaskAsync(taskId);
        }

        public async Task<AssigneeSv> CreateAsync(AssigneeSv assigneeSv)
        {
            await _validationAssigneeService.CreateValidation(assigneeSv);
            return await _assigneeRepository.CreateAsync(assigneeSv);
        }

        public async Task AddToTaskAsync(AssigneeSv assigneeSv, int taskId)
        {
            await _validationAssigneeService.AddToTaskValidation(assigneeSv, taskId);
            await _assigneeRepository.AddToTaskAsync(assigneeSv, taskId);
        }

        public async Task<AssigneeSv> UpdateAsync(AssigneeSv assigneeSv)
        {
            await _validationAssigneeService.UpdateValidation(assigneeSv);
            return await _assigneeRepository.UpdateAsync(assigneeSv);
        }

        public async Task RemoveFromTaskAsync(int id, int taskId)
        {
            await _validationAssigneeService.RemoveFromTaskValidation(id, taskId);
            await _assigneeRepository.RemoveFromTaskAsync(id, taskId);
        }

        public async Task DeleteAsync(int id)
        {
            await _validationAssigneeService.DeleteValidation(id);
            //spr id 

            //var assigneeTask = await _assigneeRepository.GetAllByTaskAsync(id);

            //foreach (var item in assigneeTask)
            //{
            //    await RemoveFromTaskAsync(id, item.Id);
            //}

            await _assigneeRepository.DeleteAsync(id);
        }
    }
}
