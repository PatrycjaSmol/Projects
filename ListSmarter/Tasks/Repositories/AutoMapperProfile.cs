using AutoMapper;
using ToDoList.Tasks.Services;

namespace ToDoList.Tasks.Repositories
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TaskDao, TaskSv>().ReverseMap();
            //CreateMap<Task0, TaskDao>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
