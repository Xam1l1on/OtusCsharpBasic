using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using TaskBot.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TaskBot.Core.Application.Commands
{
    internal class DeleteTaskCommand : ICommand
    {
        private readonly ITaskRepository _taskRepository;
        public string CommandName => "/deletetask";
        public DeleteTaskCommand(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task ExecuteAsync(ITelegramBotClient botClient, long chatId, string text, CancellationToken cts)
        {
            botClient.SendMessage(chatId, "Введиет номер задачи");
            
            var parts = text.Split(' ');
            bool intParts = int.TryParse(parts[1], out int taskId);
            if (!intParts)
            {
                await botClient.SendMessage(chatId, "Invalid task ID. Please provide a valid integer.", cancellationToken: cts);
                _taskRepository.DeleteTask(taskId);
                await botClient.SendMessage(chatId, $"Task {taskId} has been deleted.", cancellationToken: cts);
            }
        }
    }
}
