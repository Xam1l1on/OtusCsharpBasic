using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TaskBot.Core.Helper
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync(string command, ITelegramBotClient botClient, long chatId, string text, CancellationToken cancellationToken);
    }
}
