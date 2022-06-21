using ToDoList.Buckets.Services.ValidationService;
using ToDoList.Common.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Buckets.Services
{
    public class BucketService : IBucketService
    {
        private readonly IBucketRepository _bucketRepository;
        private readonly IBucketsValidationService _bucketsValidationService;

        public BucketService(IBucketRepository bucketRepository, IBucketsValidationService bucketsValidationService)
        {
            _bucketsValidationService = bucketsValidationService;
            _bucketRepository = bucketRepository;
        }

        public async Task <List<BucketSv>> GetAllAsync()
        {
            var buckets = await _bucketRepository.GetAllAsync();

            if (buckets == null)
            {
                throw new DataNotFoundException($"There is no buckets");
            }

            return buckets;
        }

        public async Task<BucketSv> GetAsync(int id)
        {
             await _bucketsValidationService.GetValidation(id);
            return await _bucketRepository.GetAsync(id);
        }

        public async Task<BucketSv> CreateAsync(BucketSv bucketSv)
        {
            await _bucketsValidationService.CreateValidation(bucketSv);
            return await _bucketRepository.CreateAsync(bucketSv);
        }

        public async Task<BucketSv> UpdateAsync(BucketSv bucketSv)
        {
            await _bucketsValidationService.UpdateValidation(bucketSv);
            return await _bucketRepository.UpdateAsync(bucketSv);
        }

        public async Task DeleteAsync(int id)
        {
            await _bucketsValidationService.DeleteValidation(id);
            await _bucketRepository.DeleteAsync(id);
        }

    }
}
