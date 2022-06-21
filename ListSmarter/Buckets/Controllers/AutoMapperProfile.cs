using AutoMapper;
using ToDoList.Buckets.Services;

namespace ToDoList.Buckets.Controllers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BucketSv, BucketDto>().ReverseMap();
        }
    }
}
