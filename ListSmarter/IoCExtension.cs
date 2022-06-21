using FluentValidation;
using ToDoList.Assignees.Controllers;
using ToDoList.Assignees.Repositories;
using ToDoList.Assignees.Services;
using ToDoList.Assignees.Services.ValidationService;
using ToDoList.Buckets.Controllers;
using ToDoList.Buckets.Repositories;
using ToDoList.Buckets.Services;
using ToDoList.Buckets.Services.ValidationService;
using ToDoList.Comments.Controllers;
using ToDoList.Comments.Repositories;
using ToDoList.Comments.Services;
using ToDoList.Comments.Services.ValidationService;
using ToDoList.Statistics.Services;
using ToDoList.Tasks.Controllers;
using ToDoList.Tasks.Repositories;
using ToDoList.Tasks.Services;
using ToDoList.Tasks.Services.ValidationService;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList
{
    public static class IoCExtension
    {
        public static void RegisterControllers(this IServiceCollection services)
        {
            //services.AddScoped<IBucketController, BucketController>();
            //services.AddScoped<ITaskController, TaskController>();
            //services.AddScoped<IAssigneeController, AssigneController>();
            //services.AddScoped<ICommentController, CommentController>();
        }
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IBucketService, BucketService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IAssigneeService, AssigneeService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IStatisticService, StatisticService>();
            services.AddScoped<IAssigneesValidationService, AssigneesValidationService>();
            services.AddScoped<IBucketsValidationService, BucketsValidationService>();
            services.AddScoped<ICommentsValidationService, CommentsValidationService>();
            services.AddScoped<ITasksValidationService, TasksValidationService>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBucketRepository, BucketRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IAssigneeRepository, AssigneeRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
        }

        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<BucketDto>, BucketDtoValidator>();
            services.AddScoped<IValidator<TaskDto>, TaskDtoValidator>();
            services.AddScoped<IValidator<AssigneeDto>, AssigneeDtoValidator>();
            services.AddScoped<IValidator<CommentDto>, CommentDtoValidator>();
        }
    }
}
