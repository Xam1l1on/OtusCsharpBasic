using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBot.Core.Models;

namespace TaskBot.Core.Infrastructure
{
    internal interface ITaskRepository
    {
        Task AddTask(TaskItem task);
        Task EditTask(int id, string newTitle, string newDescription);
        Task CompleteTask(int id);
        Task DeleteTask(int id);
    }
}
