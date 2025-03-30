using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using TaskBot.Core.Infrastructure;

namespace TaskBot.Core.Application.Commands
{
    internal class ListTaskCommand : ICommand
    {
        private readonly ITaskRepository _taskRepository;
        public string CommandName => "/listtask";
        public ListTaskCommand(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public Task ExecuteAsync(ITelegramBotClient botClient, long chatId, string text, CancellationToken cts)
        {
            throw new NotImplementedException();
        }
    }
}
