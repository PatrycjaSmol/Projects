using AutoMapper;
using ToDoList.Comments.Services;

namespace ToDoList.Comments.Controllers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CommentDto, CommentSv>().ReverseMap();
        }
    }
}
