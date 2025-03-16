using Microsoft.Extensions.Configuration;
using TaskBot.Core.Services;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace TaskBot.Core
{
    internal class Program
    {
        private static TelegramBotClient? _botClient;
        private static UpdateHandler _updateHandler;
        static void Main(string[] args)
        {
            BotConfiguration botConfig = new BotConfiguration();
            _botClient = new TelegramBotClient(botConfig.BotToken);
            _updateHandler = new UpdateHandler(_botClient);
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<Telegram.Bot.Types.Enums.UpdateType>()
            };
            using var cts = new CancellationTokenSource();

            _botClient.StartReceiving(
                updateHandler: _updateHandler,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
                );
            Console.WriteLine("Введите 'stop' в консоль для остановки бота.\n");
            while (true)
            {
                string? command = Console.ReadLine();
                if (command != null && command.ToLower() == "stop")
                {
                    Console.WriteLine("Получена команда остановки. Завершение работы бота...");
                    cts.Cancel(); // Отмена запроса на получение обновлений и остановка бота
                    break; // Выход из бесконечного цикла
                }
                else
                {
                    Console.WriteLine($"Неизвестная команда: '{command}'. Введите 'stop' для остановки."); // Сообщение о неизвестной команде
                }
            }
        }
    }
}
