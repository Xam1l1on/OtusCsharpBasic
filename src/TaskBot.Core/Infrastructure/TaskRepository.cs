using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBot.Core.Models;

namespace TaskBot.Core.Infrastructure
{
    internal class TaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task AddTask(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task EditTask(int id, string newTitle, string newDescription)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                task.Title = newTitle;
                task.Description = newDescription;
                await _context.SaveChangesAsync();
            }
        }

        public async Task CompleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
