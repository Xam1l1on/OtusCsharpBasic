using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaskBot.Core
{
    internal class Program
    {
        private static ITelegramBotClient? _botClient;
        static async Task Main(string[] args)
        {
            BotConfiguration botConfig = new BotConfiguration();
            _botClient = new TelegramBotClient(botConfig.BotToken);
        }
    }
}
