using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TaskBot.Core.Application.Commands
{
    internal class CompleteTaskCommand : ICommand
    {
        public string CommandName => "/completetask";
        public Task ExecuteAsync(ITelegramBotClient botClient, long chatId, string text, CancellationToken cts)
        {
            throw new NotImplementedException();
        }
    }
}
