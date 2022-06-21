using ToDoList.Common.Exceptions;
using ToDoList.Tasks.Services;
using System.Threading.Tasks;

namespace ToDoList.Comments.Services.ValidationService
{
    public class CommentsValidationService :ICommentsValidationService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITaskService _taskService;

        public CommentsValidationService(ICommentRepository commentRepository, ITaskService taskService)
        {
            _taskService = taskService;
            _commentRepository = commentRepository;
        }

        public async Task GetAllByTaskValidation(int taskId)
        {
            var task = await _taskService.GetAsync(taskId);
            if (task == null)
            {
                throw new DataNotFoundException($"Task with id: {taskId} doesn't exist.");
            }
        }


        public async Task<CommentSv> GetValidation(int id)
        {
            var comment = await _commentRepository.GetAsync(id);

            if (comment == null)
            {
                throw new DataNotFoundException($"Couldn't delete comment with id {id} - it doesn't exist");
            }
            return comment;
        }

        public async Task CreateValidation(CommentSv commentSv)
        {
            if (commentSv == null)
            {
                throw new DataNotFoundException("Comment can't be null.");
            }

            var taskForComment = await _taskService.GetAsync(commentSv.TaskId);
            if (taskForComment == null)
            {
                throw new DataNotFoundException(
                    $"There is no task with id: {commentSv.TaskId} so comment can't be created.");
            }

        }

        public async Task UpdateValidation(CommentSv commentSv)
        {
            if (commentSv == null)
            {
                throw new DataNotFoundException($"Couldn't update, comment can't be null. ");
            }

            var commentToUpdate = await _commentRepository.GetAsync(commentSv.Id);
            if (commentToUpdate == null)
            {
                throw new DataNotFoundException(
                    $"Comment with id: {commentToUpdate.Id} doesn't exist.");
            }
        }

        public async Task DeleteValidation(int id)
        {
            var comment = await _commentRepository.GetAsync(id);

            if (comment == null)
            {
                throw new DataNotFoundException($"Couldn't delete comment with id {id} - it doesn't exist");
            }
        }
    }
}
