﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TaskBot.Core.Application.Commands
{
    internal class DeleteTaskCommand : ICommand
    {
        public string CommandName => "/delete";
        public Task ExecuteAsync(ITelegramBotClient botClient, long chatId, string text, CancellationToken cts)
        {
            throw new NotImplementedException();
        }
    }
}
