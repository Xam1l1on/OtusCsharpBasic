using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TaskBot.Core.Application
{
    internal interface ICommand
    {
        string CommandName { get; }
        Task ExecuteAsync(ITelegramBotClient botClient, long chatId, string text, CancellationToken cts);
    }
}
