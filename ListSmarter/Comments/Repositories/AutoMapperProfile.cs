using AutoMapper;
using ToDoList.Comments.Services;

namespace ToDoList.Comments.Repositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CommentDao, CommentSv>().ReverseMap();
        }
    }
}
