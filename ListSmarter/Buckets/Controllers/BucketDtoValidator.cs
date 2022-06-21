using FluentValidation;

namespace ToDoList.Buckets.Controllers
{
    public class BucketDtoValidator : AbstractValidator<BucketDto>
    {
        public BucketDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(00, 10).WithMessage($"The name of buckets can't be null");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description has only 500 characters.");
            RuleFor(x => x.MaxNumberOfTask).LessThanOrEqualTo(40).WithMessage("You can add 40 tasks for one bucket");
        }
    }
}
