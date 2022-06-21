using FluentValidation;

namespace ToDoList.Tasks.Controllers
{
    public class TaskDtoValidator : AbstractValidator<TaskDto>
    {
        public TaskDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.Priority).NotNull().NotEmpty().IsInEnum();
            RuleFor(x => x.State).NotEmpty().NotEmpty().IsInEnum();
            RuleFor(x => x.BucketId).NotNull().NotEmpty().GreaterThan(0);

            When(x => x.Assignees != null, () =>
            {
                RuleFor(x => x.Assignees.Count).LessThanOrEqualTo(5);

            });

        }
    }
}
