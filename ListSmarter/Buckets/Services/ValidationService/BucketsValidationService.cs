using ToDoList.Common.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Buckets.Services.ValidationService
{
    public class BucketsValidationService : IBucketsValidationService
    {
        private readonly IBucketRepository _bucketRepository;

        public BucketsValidationService(IBucketRepository bucketRepository)
        {
            _bucketRepository = bucketRepository;
        }

        public async Task<BucketSv> GetValidation(int id)
        {
            var bucket = await _bucketRepository.GetAsync(id);

            if (bucket == null)
            {
                throw new DataNotFoundException($"There is no bucket with id {id}.");
            }

            return bucket;
        }

        public async Task CreateValidation(BucketSv bucketSv)
        {
            if (bucketSv == null)
            {
                throw new DataNotFoundException($"Bucket can't be null.");
            }

            
            if (!(await CheckUniqueNameForAdd(bucketSv)))
            {
                throw new NotUniqueException(
                    $"The name of bucket must be unique. The name {bucketSv.Name} already exist.");
            }
        }

        public async Task UpdateValidation(BucketSv bucketSv)
        {
            if (bucketSv == null)
            {
                throw new DataNotFoundException($"The bucket with id {bucketSv.Id} doesn't exist.");
            }

            var bucketToUpdate = await _bucketRepository.GetAsync(bucketSv.Id);
            if (bucketToUpdate == null)
            {
                throw new DataNotFoundException($"The bucket with id {bucketSv.Id} doesn't exist in data base.");
            }

            if (await CheckUniqueNameForUpdate(bucketSv, bucketToUpdate))
            {
                throw new DataNotFoundException("Name of bucket should be unique. Couldn't update bucket.");
            }
        }


        public async Task DeleteValidation(int id)
        {
            var bucketToDelete = await _bucketRepository.GetAsync(id);

            if (bucketToDelete == null)
            {
                throw new DataNotFoundException($"Couldn't delete bucket with id {id} - it doesn't exist.");
            }
        }

        private async Task<bool> CheckUniqueNameForAdd(BucketSv bucketSv)
        {
            var buckets = await _bucketRepository.GetAllAsync();
            return buckets.All(b => b.Name != bucketSv.Name);
        }

        private async Task<bool> CheckUniqueNameForUpdate(BucketSv bucketSv, BucketSv bucketToUpdate)
        {
            var buckets = await _bucketRepository.GetAllAsync();
            return bucketSv.Name != bucketToUpdate.Name && buckets.Any(b => b.Name == bucketSv.Name);
        }
    }
}
