using ToDoList.Comments.Services.ValidationService;
using ToDoList.Common.Exceptions;
using ToDoList.Tasks.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Comments.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITaskService _taskService;
        private readonly ICommentsValidationService _commentsValidationService;

        public CommentService(ICommentRepository commentRepository, ITaskService taskService, ICommentsValidationService commentsValidationService)
        {
            _commentsValidationService = commentsValidationService;
            _commentRepository = commentRepository;
            _taskService = taskService;
        }

        public async Task<List<CommentSv>> GetAllByTaskAsync(int taskId)
        {
            try
            {
                await _taskService.GetAsync(taskId);
            }
            catch (DataNotFoundException)
            {
                throw new DataNotFoundException($"There is no task with id {taskId}");
            }

            return await _commentRepository.GetAllByTaskAsync(taskId);
        }

        public async Task<List<CommentSv>> GetAllAsync()
        {
            try
            {
                return await _commentRepository.GetAllAsync();
            }
            catch (DataNotFoundException)
            {
                throw new DataNotFoundException($"There is no comments.");
            }

        }

        public async Task<CommentSv> GetAsync(int id)
        { 
            await _commentsValidationService.DeleteValidation(id);
            return await _commentRepository.GetAsync(id);

        }

        public async Task DeleteAsync(int id)
        {
            await _commentsValidationService.DeleteValidation(id);
            await _commentRepository.DeleteAsync(id);
        }

        public async Task<CommentSv> UpdateAsync(CommentSv commentSv)
        {
            await _commentsValidationService.UpdateValidation(commentSv);
            return await _commentRepository.UpdateAsync(commentSv);

        }

        public async Task<CommentSv> CreateAsync(CommentSv commentSv)
        {
            await _commentsValidationService.CreateValidation(commentSv);
            return await _commentRepository.CreateAsync(commentSv);

        }
    }
}
