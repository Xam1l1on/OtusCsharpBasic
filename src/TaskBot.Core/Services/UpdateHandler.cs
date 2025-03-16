using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBot.Core.Models;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TaskBot.Core.Models;

namespace TaskBot.Core.Services
{
    public class UpdateHandler: IUpdateHandler
    {
        private List<TaskFK> tasks = new List<TaskFK>();
        private readonly TelegramBotClient _botClient;
        public UpdateHandler(TelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var handlers = new UpdateHandlers(botClient);
            await (update switch
            {
                { Message: { } message } => OnMessage(message),
                { CallbackQuery: { } callbackQuery } => OnCallbackQuery(callbackQuery),
                { InlineQuery: { } inlineQuery } => OnInlineQuery(inlineQuery),
                _ => UnknownUpdateHandlerAsync(update)
            });         
        }

        private async Task OnBotCall(Message message)
        {
            throw new NotImplementedException();
        }

        private async Task UnknownUpdateHandlerAsync(Update update)
        {
            throw new NotImplementedException();
        }

        private async Task OnPollAnswer(PollAnswer pollAnswer)
        {
            throw new NotImplementedException();
        }

        private async Task OnPoll(Poll poll)
        {
            throw new NotImplementedException();
        }

        private async Task OnChosenInlineResult(ChosenInlineResult chosenInlineResult)
        {
            throw new NotImplementedException();
        }

        private async Task OnInlineQuery(InlineQuery inlineQuery)
        {
            throw new NotImplementedException();
        }

        private async Task OnCallbackQuery(CallbackQuery callbackQuery)
        {
            await _botClient.AnswerCallbackQuery(callbackQuery.Id, $"Received {callbackQuery.Data}");
            await _botClient.SendMessage(callbackQuery.Message!.Chat, $"Received {callbackQuery.Data}");
        }

        private async Task OnMessage(Message message)
        {
            if (message.Text is not { } messageText)
                return;
            var chatId = message.Chat.Id;
            var chatType = message.Chat.Type;
            var messageId = message.MessageId;
            await (messageText.Split(' ', '@')[0].ToLower() switch
            {
                "/start" => HandleStartCommand(message),
                "/end" => HandleEndCommand(message),
                "/listtask" => ListTask(message),
                "/addtask" => AddTask(message),
                "/edittask" => EditTask(message),
                "/deletetask" => DeleteTask(message),
                _ => Usage(message)
            });
        }
        private async Task Usage(Message msg)
        {
            const string usage = """
                <b><u>Bot menu</u></b>:
                /start - Запуск бота
            """;
            await _botClient.SendMessage(msg.Chat, usage, parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
        }
        private async Task HandleStartCommand(Message message)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId; // ID сообщения пользователя

            // Создаем ReplyKeyboardMarkup для команды /start
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "Список задач", "Добавить задачу", "Изменить задачу", "Удалить задачу" } // Один ряд кнопок
            })
            {
                ResizeKeyboard = true // Автоматически уменьшать размер клавиатуры
            };

            // Удаляем сообщение пользователя (/start)
            await _botClient.DeleteMessage(chatId: chatId, messageId: messageId);
            // Отправляем сообщение с клавиатурой
            await _botClient.SendMessage(
                chatId: chatId,
                text: "Keyboard",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: CancellationToken.None);
        }
        private async Task HandleEndCommand(Message message)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "Список задач", "Добавить задачу", "Изменить задачу", "Удалить задачу" } // Один ряд кнопок
            })
            {
                ResizeKeyboard = true // Автоматически уменьшать размер клавиатуры
            };
        }
        private async Task ListTask(Message message)
        {
            var chatId = message.Chat.Id;

            if (!tasks.Any())
            {
                await _botClient.SendMessage(chatId, "Список задач пуст");
                return;
            }

            var tasksList = tasks.Select(t =>
                $"ID: {t.TaskId}\n" +
                $"Описание: {t.Description}\n" +
                $"Ответственный: {t.UserResponsible}\n" +
                $"Тип: {t.TaskType}\n" +
                new string('-', 20))
                .Aggregate((a, b) => a + "\n" + b);

            await _botClient.SendMessage(chatId, tasksList);
        }
        private async Task AddTask(Message message)
        {
            var chatId = message.Chat.Id;
            var parts = message.Text.Split(' ', '@').Skip(1).ToArray();

            if (parts.Length < 3)
            {
                await _botClient.SendMessage(chatId,
                    "Формат команды: /addtask [Описание] @[Ответственный] [Тип]\n" +
                    "Доступные типы: Basic, Project, Other");
                return;
            }

            if (!Enum.TryParse(parts[2], true, out TaskFKType taskType))
            {
                await _botClient.SendMessage(chatId, "Неверный тип задачи");
                return;
            }

            var newTask = new TaskFK
            {
                TaskId = _nextTaskId++,
                Description = parts[0],
                UserResponsible = parts[1],
                TaskType = taskType
            };
            tasks.Add(newTask);
            await _botClient.SendMessage(chatId, $"Задача #{newTask.TaskId} добавлена");
        }
        private async Task EditTask(Message message,int taskId)
        {
            TaskFK taskModify = tasks.FirstOrDefault(t => t.TaskId == taskId);
            
        }
        private async Task DeleteTask(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
