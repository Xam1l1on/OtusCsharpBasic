using Telegram.Bot.Types;
using Telegram.Bot;
using TaskBot.Core.Infrastructure;

namespace TaskBot.Core.Application
{
    internal interface BotHandler : IBotHandler
    {
        private readonly ITaskRepository _taskRepository;

        public BotHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is null) return;
            var chatId = update.Message.Chat.Id;
            var text = update.Message.Text;

            if (text.StartsWith("/add"))
            {
                // Add task logic
            }
            else if (text.StartsWith("/edit"))
            {
                // Edit task logic
            }
            else if (text.StartsWith("/complete"))
            {
                // Complete task logic
            }
            else if (text.StartsWith("/delete"))
            {
                // Delete task logic
            }
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }
    }
}
