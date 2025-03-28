using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TaskBot.Core.Application;

namespace TaskBot.Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var serviceProvider = ConfigureServices(configuration);
            var bot = serviceProvider.GetRequiredService<ITelegramBotClient>();
            var handler = serviceProvider.GetRequiredService<IBotHandler>();

            using var cts = new CancellationTokenSource();
            bot.StartReceiving(
                handler.HandleUpdateAsync,
                handler.HandleErrorAsync,
                new ReceiverOptions(),
                cts.Token
            );
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
        static ServiceProvider ConfigureServices(IConfiguration configuration)
        {
            var botToken = configuration["BotToken"];
            var connectionString = configuration["ConnectionStrings:PostgreSQL"];

            return new ServiceCollection()
                .AddSingleton<ITelegramBotClient>(new TelegramBotClient(botToken))
                .AddSingleton<IBotHandler, BotHandler>()
                .AddDbContext<TaskDbContext>(options => options.UseNpgsql(connectionString))
                .AddScoped<ITaskRepository, TaskRepository>()
                .AddSingleton(configuration)
                .BuildServiceProvider();
        }
    }
}
