using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Buckets.Services
{
    public interface IBucketService
    {
        Task<List<BucketSv>> GetAllAsync();
        Task<BucketSv> GetAsync(int id);
        Task<BucketSv> CreateAsync (BucketSv bucketSv);
        Task<BucketSv> UpdateAsync(BucketSv bucketSv);
        Task DeleteAsync (int id);
    }
}
