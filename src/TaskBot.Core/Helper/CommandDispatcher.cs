using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBot.Core.Application;
using Telegram.Bot;

namespace TaskBot.Core.Helper
{
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly Dictionary<string, ICommand> _commands;

        public CommandDispatcher(IEnumerable<ICommand> commands)
        {
            _commands = commands.ToDictionary(cmd => cmd.CommandName, cmd => cmd);
        }

        public async Task DispatchAsync(string command, ITelegramBotClient botClient, long chatId, string text, CancellationToken cancellationToken)
        {
            if (_commands.TryGetValue(command, out var cmd))
            {
                await cmd.ExecuteAsync(botClient, chatId, text, cancellationToken);
            }
            else
            {
                await botClient.SendMessage(chatId, "Неизвестная команда", cancellationToken: cancellationToken);
            }
        }
    }
}
