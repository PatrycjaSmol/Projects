using AutoMapper;
using ToDoList.Assignees.Services;

namespace ToDoList.Assignees.Controllers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AssigneeDto, AssigneeSv>().ReverseMap();
        }
    }

}
