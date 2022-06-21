using AutoMapper;
using ToDoList.Buckets.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Buckets.Repositories
{
    public class BucketRepository : IBucketRepository
    {
        private readonly IMapper _mapper;
        private readonly ToDoListContext _context;

        public BucketRepository(IMapper mapper, ToDoListContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BucketSv>> GetAllAsync()
        {
            return _mapper.Map<List<BucketSv>>(await _context.Buckets
                .Include(b => b.Tasks)
                .ThenInclude(x => x.Comments)
                .Include(x => x.Tasks)
                .ThenInclude(x => x.Assignees)
                .ToListAsync());
        }

        public async Task<BucketSv> GetAsync(int id)
        {
            var bucket = await _context.Buckets
                .Include(b => b.Tasks)
                .ThenInclude(t => t.Comments)
                .Include(b => b.Tasks)
                .ThenInclude(t => t.Assignees)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            return await Task.FromResult(_mapper.Map<BucketSv>(bucket));
        }

        public async Task<BucketSv> CreateAsync(BucketSv bucketSv)
        {
            var newBucket = _mapper.Map<BucketDao>(bucketSv);

            await _context.Buckets.AddAsync(newBucket);
            await _context.SaveChangesAsync();
            var bucketAdded = _mapper.Map<BucketSv>(newBucket);
            return bucketAdded;
        }


        public async Task<BucketSv> UpdateAsync(BucketSv bucketSv)
        {
            var bucketBefore = await _context.Buckets.SingleOrDefaultAsync(b => b.Id == bucketSv.Id);

            bucketBefore.Name = bucketSv.Name;
            bucketBefore.Color = bucketSv.Color;
            bucketBefore.Description = bucketSv.Description;

            await _context.SaveChangesAsync();

            var bucketAfter = _mapper.Map<BucketSv>(bucketBefore);
            
            return await Task.FromResult(bucketAfter);
        }

        public async Task DeleteAsync(int id)
        {
            var bucketToDelete = await _context.Buckets
                .FirstOrDefaultAsync(d => d.Id == id);

            _context.Buckets.Remove(bucketToDelete);
            await _context.SaveChangesAsync();

            await Task.CompletedTask;
        }
    }
}
