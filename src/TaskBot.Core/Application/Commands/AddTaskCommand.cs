using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBot.Core.Infrastructure;
using TaskBot.Core.Models;
using Telegram.Bot;

namespace TaskBot.Core.Application.Commands
{
    internal class AddTaskCommand : ICommand
    {
        private readonly ITaskRepository _taskRepository;
        public string CommandName => "/add";

        public AddTaskCommand(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task ExecuteAsync(ITelegramBotClient botClient, long chatId, string text, CancellationToken cancellationToken)
        {
            var parts = text.Split('|');
            if (parts.Length < 4)
            {
                await botClient.SendMessage(chatId, "Invalid format. Use: /add | AssignedTo | Title | Description | TaskType", cancellationToken: cancellationToken);
                return;
            }

            var task = new TaskItem
            {
                CreatedAt = DateTime.UtcNow,
                AssignedTo = parts[1].Trim(),
                Title = parts[2].Trim(),
                Description = parts[3].Trim(),
                TaskType = Enum.TryParse(parts[4].Trim(), out TaskItemType type) ? type : TaskItemType.Basic
            };

            await _taskRepository.AddTask(task);
            await botClient.SendMessage(chatId, $"Task added: {task.Title} (ID: {task.Id})", cancellationToken: cancellationToken);
        }
    }
}
