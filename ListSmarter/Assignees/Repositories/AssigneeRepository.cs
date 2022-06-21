using AutoMapper;
using ToDoList.Assignees.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ToDoList.Assignees.Repositories
{
    public class AssigneeRepository : IAssigneeRepository
    {
        private readonly IMapper _mapper;
        private readonly ToDoListContext _context;

        public AssigneeRepository(IMapper mapper, ToDoListContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AssigneeSv> GetAsync(int id)
        {
            var assignee = _context.Assignees.SingleOrDefault(x => x.Id == id);
            return await Task.FromResult(_mapper.Map<AssigneeSv>(assignee));
        }

        public async Task<List<AssigneeSv>> GetAllAsync()
        {
            var assignees = _mapper.Map<List<AssigneeSv>>(_context.Assignees);
            return await Task.FromResult(assignees);
        }

        public async Task<List<AssigneeSv>> GetAllByTaskAsync(int taskId)
        {
            var assigneeTask = await _context.Tasks
                .Include(x => x.Assignees)
                .Where(x => x.Id == taskId)
                .SelectMany(x => x.Assignees)
                .ToListAsync();

            return _mapper.Map<List<AssigneeSv>>(assigneeTask);
        }

        
        public async Task<AssigneeSv> CreateAsync(AssigneeSv assigneeSv)
        {
            var newAssignee = _mapper.Map<AssigneeDao>(assigneeSv);

            await _context.Assignees.AddAsync(newAssignee);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<AssigneeSv>(newAssignee);

            return await Task.FromResult(result);
        }

        public async Task<AssigneeSv> AddToTaskAsync(AssigneeSv assigneeSv, int taskId)
        {

            var assignee = _mapper.Map<AssigneeDao>(assigneeSv);
            var existingAssignee = await _context.Assignees
                .SingleOrDefaultAsync(a => a.Name.ToLower() == assignee.Name.ToLower());

            var task = _context.Tasks
                .Include(t => t.Assignees)
                .SingleOrDefault(x => x.Id == taskId);

            task.Assignees.Add(existingAssignee);
            await _context.SaveChangesAsync();

            return _mapper.Map<AssigneeSv>(existingAssignee);
        }

        public async Task<AssigneeSv> UpdateAsync(AssigneeSv assigneeSv)
        {
            var assigneeToUpdate = _context.Assignees.SingleOrDefault(x => x.Id == assigneeSv.Id);
            assigneeToUpdate.Name = assigneeSv.Name;

            await _context.SaveChangesAsync();

            var assigneeMapped = await _context.Assignees.SingleOrDefaultAsync(x => x.Id == assigneeSv.Id);
            var result = _mapper.Map<AssigneeSv>(assigneeMapped);

            return await Task.FromResult(result);
        }

        public async Task RemoveFromTaskAsync(int id, int taskId)
        {
            var assigneeToRemove = _context.Assignees.Single(x => x.Id == id);

            var task = _context.Tasks.Include(x => x.Assignees)
                .SingleOrDefault(x => x.Id == taskId);

            task.Assignees.Remove(assigneeToRemove);

            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var assigneeToRemove = _context.Assignees.SingleOrDefault(x => x.Id == id);
            _context.Assignees.Remove(assigneeToRemove);

            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

    }
}

