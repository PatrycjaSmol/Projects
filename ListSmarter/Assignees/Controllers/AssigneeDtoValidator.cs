using FluentValidation;

namespace ToDoList.Assignees.Controllers
{
    public class AssigneeDtoValidator : AbstractValidator<AssigneeDto>
    {
        public AssigneeDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(100);
        }
    }
}
