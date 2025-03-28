using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBot.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskBot.Core.Infrastructure
{
    internal class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }
        public DbSet<TaskItem> Tasks { get; set; }
    }
}
