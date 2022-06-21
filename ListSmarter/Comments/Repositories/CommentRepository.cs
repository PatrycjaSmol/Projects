using AutoMapper;
using ToDoList.Comments.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Comments.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IMapper _mapper;
        private readonly ToDoListContext _context;

        public CommentRepository(IMapper mapper, ToDoListContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<CommentSv>> GetAllAsync()
        {
            var tasks = _context.Buckets.SelectMany(x => x.Tasks);

            var all = tasks.Where(x => x.Comments != null).SelectMany(x => x.Comments);
            var result = _mapper.Map<List<CommentSv>>(all);
            return await Task.FromResult(result);
        }

        public async Task<List<CommentSv>> GetAllByTaskAsync(int taskId)
        {
            var comments =  _context.Comments
                .Where(x => x.TaskId == taskId)
                .ToList();

            var result = _mapper.Map<List<CommentSv>>(comments);
            return await Task.FromResult(result);
        }

        public async Task <CommentSv> GetAsync(int commentId)
        {
            var commentToGet = _context.Comments
                .FirstOrDefault(x => x.Id == commentId);

            var result = _mapper.Map<CommentSv>(commentToGet);
            return await Task.FromResult(result);
        }

        public async Task<CommentSv> CreateAsync(CommentSv commentSv)
        {
            var newComment = _mapper.Map<CommentDao>(commentSv);

            await _context.Comments.AddAsync(newComment); 
            await _context.SaveChangesAsync();

            var result = _mapper.Map<CommentSv>(newComment);

            return await Task.FromResult(result);
        }


        public async Task<CommentSv> UpdateAsync(CommentSv commentSv)
        {
            var commentBefore = _mapper.Map<CommentDao>(GetAsync(commentSv.Id));

            commentBefore.Content = commentSv.Content;

            await _context.SaveChangesAsync();
            var commentAfter = _mapper.Map<CommentSv>(commentBefore);

            return await Task.FromResult(commentAfter);
        }

        public async Task DeleteAsync(int id)
        {
            var comment = _context.Comments.FirstOrDefault(d => d.Id == id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
