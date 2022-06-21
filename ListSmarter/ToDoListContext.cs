using ToDoList.Assignees.Repositories;
using ToDoList.Buckets.Controllers;
using ToDoList.Buckets.Repositories;
using ToDoList.Comments.Repositories;
using ToDoList.Tasks.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ToDoList
{
    public class ToDoListContext : DbContext
    {
        public static readonly Microsoft.Extensions.Logging.LoggerFactory _myLoggerFactory =
            new LoggerFactory(new[] {
                new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
            });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myLoggerFactory);
        }
        public DbSet<BucketDao> Buckets { get; set; }
        public DbSet<TaskDao> Tasks { get; set; }
        public DbSet<AssigneeDao> Assignees { get; set; }
        public DbSet<CommentDao> Comments { get; set; }


        public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssigneeDao>()
                .HasMany(left => left.Tasks)
                .WithMany(right => right.Assignees)
                .UsingEntity<Dictionary<string, object>>("TaskAssignees",
                    x => x.HasOne<TaskDao>().WithMany().HasForeignKey("TaskId"),
                    x => x.HasOne<AssigneeDao>().WithMany().HasForeignKey("AssigneeId"),
                    x => x.ToTable("TaskAssignees"));
        }
    }
}


