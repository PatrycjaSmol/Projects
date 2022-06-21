using System.Threading.Tasks;

namespace ToDoList.Buckets.Services.ValidationService
{
    public interface IBucketsValidationService
    {
        Task<BucketSv> GetValidation(int id);
        Task CreateValidation(BucketSv bucketSv);
        Task UpdateValidation(BucketSv bucketSv);
        Task DeleteValidation(int id);
       
       
    }
}
