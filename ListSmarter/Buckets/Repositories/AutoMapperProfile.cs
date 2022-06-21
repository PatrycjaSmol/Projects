using AutoMapper;
using ToDoList.Buckets.Services;

namespace ToDoList.Buckets.Repositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BucketSv, BucketDao>().ReverseMap();
        }
    }
}
