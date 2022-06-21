using AutoMapper;
using ToDoList.Tasks.Services;


namespace ToDoList.Tasks.Controllers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TaskSv, TaskDto>().ReverseMap();
        }
    }
}
