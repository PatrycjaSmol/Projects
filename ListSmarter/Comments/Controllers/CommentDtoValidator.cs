using FluentValidation;

namespace ToDoList.Comments.Controllers
{
    public class CommentDtoValidator : AbstractValidator<CommentDto>
    {
        public CommentDtoValidator()
        {
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.TaskId).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
