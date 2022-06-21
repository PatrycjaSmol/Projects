using AutoMapper;
using ToDoList.Tasks.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Tasks.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMapper _mapper;
        private readonly ToDoListContext _context;

        public TaskRepository(IMapper mapper, ToDoListContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<TaskSv>> GetAllByBucketAsync(int id)
        {
            var tasks = _context.Tasks
                .Where(x => x.BucketId == id)
                .Include(x => x.Assignees)
                .Include(x => x.Comments);

            return await Task.FromResult(_mapper.Map<List<TaskSv>>(tasks));
        }

        public async Task<List<TaskSv>> GetAllAsync()
        {
            var result = _mapper.Map<List<TaskSv>>(_context.Tasks);

            return await Task.FromResult(result);
        }

        public async Task<TaskSv> GetAsync(int id)
        {
            var task = _context.Tasks.Include(a => a.Assignees)
                .Include(c => c.Comments)
                .FirstOrDefault(x => x.Id == id);

            var result = _mapper.Map<TaskSv>(task);
            return await Task.FromResult(result);
        }

        public async Task DeleteAsync(int id)
        {
            var taskToRemove = _mapper.Map<TaskDao>(GetAsync(id));

            _context.Tasks.Remove(taskToRemove);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task<TaskSv> UpdateAsync(TaskSv task)
        {
            var taskAfter = _context.Tasks.FirstOrDefault(x => x.Id == task.Id);

            var taskBefore = _mapper.Map<TaskDao>(task);

            taskAfter.Title = taskBefore.Title;
            taskAfter.Description = taskBefore.Description;
            taskAfter.Priority = taskBefore.Priority;
            taskAfter.State = taskBefore.State;

            var taskUpdated = _mapper.Map<TaskSv>(taskAfter);

            await _context.SaveChangesAsync();
            return await Task.FromResult(taskUpdated);
        }

        public async Task<TaskSv> CreateAsync(TaskSv task)
        {
            var taskToAdd = _mapper.Map<TaskDao>(task);
            await _context.Tasks.AddAsync(taskToAdd);
           
            var result = _mapper.Map<TaskSv>(taskToAdd);
            await _context.SaveChangesAsync();
            return await Task.FromResult(result);
        }
    }
}
