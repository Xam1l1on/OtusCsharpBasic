using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskBot.Core.Services
{
    class UpdateHandlers
    {
        private readonly ITelegramBotClient _botClient;

        public UpdateHandlers(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task OnMessage(Message message)
        {
            // Логика обработки сообщения
            if (message.Text is not { } messageText)
                return;

            Message sentMessage = await (messageText.Split(' ')[0] switch
            {
                "/addtask" => StartAddTaskFlow(message),
                //"/keyboard" => SendReplyKeyboard(message),
                //"/remove" => RemoveKeyboard(message),
                //"/request" => RequestContactAndLocation(message),
                //"/inline_mode" => StartInlineQuery(message),
                //"/poll" => SendPoll(message),
                //"/poll_anonymous" => SendAnonymousPoll(message),
                //"/throw" => FailingHandler(message),
                _ => Usage(message)
            });
        }

        public async Task OnEditedMessage(Message message)
        {
            // Логика обработки отредактированного сообщения
        }

        public async Task OnCallbackQuery(CallbackQuery callbackQuery)
        {
            // Логика обработки запроса от встроенных кнопок
        }

        public async Task OnInlineQuery(InlineQuery inlineQuery)
        {
            // Логика обработки inline запроса
        }

        public async Task OnChosenInlineResult(ChosenInlineResult chosenInlineResult)
        {
            // Логика обработки результата inline запроса
        }

        public async Task OnPoll(Poll poll)
        {
            // Логика обработки опроса
        }

        public async Task OnPollAnswer(PollAnswer pollAnswer)
        {
            // Логика обработки ответа на опрос
        }

        public async Task UnknownUpdateHandlerAsync(Update update)
        {
            // Логика обработки неизвестных типов обновлений
        }

        private async Task<Message> StartAddTaskFlow(Message message)
        {
            InlineKeyboardMarkup taskGroupKeyboard = new(new[]
            {
                new[] { InlineKeyboardButton.WithCallbackData("Основное", "Основное"), InlineKeyboardButton.WithCallbackData("Проектная", "Проектная"), InlineKeyboardButton.WithCallbackData("Другое", "Другое") }
            });
            return await _botClient.SendMessage(message.Chat, "Keyboard buttons:"); ;
        }
        private async Task<Message> Usage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
