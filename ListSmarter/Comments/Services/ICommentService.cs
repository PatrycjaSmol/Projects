using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Comments.Services
{
    public interface ICommentService
    {
        Task<List<CommentSv>> GetAllAsync();
        Task<CommentSv> GetAsync(int id);
        Task<List<CommentSv>> GetAllByTaskAsync(int taskId);
        Task<CommentSv> CreateAsync(CommentSv commentSv);
        Task<CommentSv> UpdateAsync(CommentSv commentSv);
        Task DeleteAsync(int id);
       
       
    }
}
