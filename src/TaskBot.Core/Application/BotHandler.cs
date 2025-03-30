using Telegram.Bot.Types;
using Telegram.Bot;
using TaskBot.Core.Infrastructure;
using TaskBot.Core.Application.Commands;
using TaskBot.Core.Helper;
using Telegram.Bot.Types.ReplyMarkups;

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
            if (text == "/tasks")
            {
                var keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton("Добавить"),
                    new KeyboardButton("Изменить"),
                    new KeyboardButton("Список"),
                    new KeyboardButton("Назад")
                }
                )
                { ResizeKeyboard = true };
                await botClient.SendMessage(chatId, "Выберите действие:", replyMarkup: keyboard, cancellationToken: cancellationToken);
                return;
            }
            else if (text == "Добавить" || text == "/addtask")
            {
                await _commandDispatcher.DispatchAsync("/addtask", botClient, chatId, text, cancellationToken);
                return;
            }
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
