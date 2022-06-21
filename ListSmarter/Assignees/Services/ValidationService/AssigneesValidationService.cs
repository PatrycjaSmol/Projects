using ToDoList.Common.Exceptions;
using ToDoList.Tasks.Services;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace ToDoList.Assignees.Services.ValidationService
{
    public class AssigneesValidationService : IAssigneesValidationService
    {
        private readonly IAssigneeRepository _assigneeRepository;
        private readonly ITaskService _taskService;

        public AssigneesValidationService(IAssigneeRepository assigneeRepository, ITaskService taskService)
        {
            _taskService = taskService;
            _assigneeRepository = assigneeRepository;
        }

        public async Task<AssigneeSv> GetValidation(int id)
        {
            var assignee = await _assigneeRepository.GetAsync(id);
            if (assignee == null)
            {
                throw new DataNotFoundException($"Assignee with id: {id} doesn't exist.");
            }

            return assignee;
        }

        public async Task GetAllByTaskValidation(int taskId)
        {
            var task = await _taskService.GetAsync(taskId);
            if (task == null)
            {
                throw new DataNotFoundException($"Task with id: {taskId} doesn't exist.");
            }
        }

        public async Task AddToTaskValidation(AssigneeSv assigneeSv, int taskId)
        {
            if (assigneeSv == null)
            {
                throw new DataNotFoundException($"Create is not possible - new assignee can't be null.");
            }

            var task = await _taskService.GetAsync(taskId);
            if (task == null)
            {
                throw new DataNotFoundException($"Couldn't create assignee for the task with id: {taskId} - it doesn't exist.");
            }

            if (task.Assignees.Count >= 5) // magic number to delete
            {
                throw new DataLimitException(
                    $"Couldn't add assignee to task {taskId}. Maximum number of assignees has been reached.");
            }

            if (await CheckIfAssigneeAlreadyExist(assigneeSv))
            {
                throw new DataNotFoundException($"Assignee with id: {assigneeSv.Id} is already assigned to this task.");
            }
        }

        public async Task CreateValidation(AssigneeSv assigneeSv)
        {
            if (assigneeSv == null)
            {
                throw new DataNotFoundException("Couldn't create a new assignee - it can not be null.");
            }
            if (await CheckIfAssigneeAlreadyExist(assigneeSv))
            {
                throw new DataNotFoundException(
                    $"Assignee '{nameof(assigneeSv.Name)}' already exist in data base. Name must to be unique.");
            }
        }


        public async Task UpdateValidation(AssigneeSv assigneeSv)
        {
            if (assigneeSv == null)
            {
                throw new DataNotFoundException("Name of update assignee can't be null.");
            }
            var assigneeToUpdate = await _assigneeRepository.GetAsync(assigneeSv.Id);
            if (assigneeToUpdate == null)
            {
                throw new DataNotFoundException(
                    $"Couldn't update assignee with id :{assigneeSv.Id} doesn't exist in data base.");
            }

            if (CheckUniqueNameForUpdate(assigneeSv, assigneeToUpdate))
            {
                throw new DataNotFoundException(
                    $"Couldn't update the Assignee '{nameof(assigneeSv.Name)}'. Name must to be unique.");
            }
        }

        public async Task RemoveFromTaskValidation(int id, int taskId)
        {
            var assignee = await _assigneeRepository.GetAsync(id);
            if (assignee == null)
            {
                throw new DataNotFoundException($"Couldn't delete assignee with id: {id} - it doesn't exist.");
            }
        }

        public async Task DeleteValidation(int id)
        {
            var assignee = await _assigneeRepository.GetAsync(id);
            if (assignee == null)
            {
                throw new DataNotFoundException(
                    $"Couldn't delete assignee with id {id} - it doesn't exist.");
            }
        }

        private bool CheckUniqueNameForUpdate(AssigneeSv assigneeSv, AssigneeSv assigneeToUpdate)
        {
            return assigneeSv.Name.ToLower() != assigneeToUpdate.Name.ToLower();
        }

        private async Task<bool> CheckIfAssigneeAlreadyExist(AssigneeSv assigneeSv)
        {
            var result = await _assigneeRepository.GetAllAsync();
            return result.Any(x => x.Name.ToLower() == assigneeSv.Name.ToLower());
        }
    }
}
