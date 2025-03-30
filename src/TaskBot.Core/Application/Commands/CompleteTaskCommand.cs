using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using TaskBot.Core.Infrastructure;
using Telegram.Bot.Types;

namespace TaskBot.Core.Application.Commands
{
    internal class CompleteTaskCommand : ICommand
    {
        private readonly ITaskRepository _taskRepository;
        public string CommandName => "/completetask";
        public CompleteTaskCommand(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task ExecuteAsync(ITelegramBotClient botClient, long chatId, string text, CancellationToken cts)
        {
            // Parse the task ID from the command text
            var taskId = int.Parse(text.Split(' ')[1]);

            // Find the task in the database (this is a placeholder, replace with actual database call)
            await _taskRepository.CompleteTask(taskId);

            await botClient.SendMessage(chatId, $"Task {taskId} has been marked as completed.", cancellationToken: cts);
        }
    }
}
