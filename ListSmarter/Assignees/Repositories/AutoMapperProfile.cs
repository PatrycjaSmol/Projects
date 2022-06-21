using AutoMapper;
using ToDoList.Assignees.Services;

namespace ToDoList.Assignees.Repositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AssigneeDao, AssigneeSv>().ReverseMap();
        }
    }
}
