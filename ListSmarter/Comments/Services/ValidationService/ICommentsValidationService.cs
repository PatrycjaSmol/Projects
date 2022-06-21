using System.Threading.Tasks;

namespace ToDoList.Comments.Services.ValidationService
{
    public interface ICommentsValidationService
    {
        Task GetAllByTaskValidation(int taskId);
        Task CreateValidation(CommentSv commentSv);
        Task UpdateValidation(CommentSv commentSv);
        Task<CommentSv> GetValidation(int id);
        Task DeleteValidation(int id);
    }
}
