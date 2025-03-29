using Telegram.Bot.Types;
using Telegram.Bot;
using TaskBot.Core.Infrastructure;
using TaskBot.Core.Application.Commands;
using TaskBot.Core.Helper;

namespace TaskBot.Core.Application
{
    public class BotHandler : IBotHandler
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public BotHandler(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is null) return;
            var chatId = update.Message.Chat.Id;
            var text = update.Message.Text;

            var command = text.Split(' ')[0]; // Берем только команду, остальное — аргументы
            await _commandDispatcher.DispatchAsync(command, botClient, chatId, text, cancellationToken);
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }
    }
}
