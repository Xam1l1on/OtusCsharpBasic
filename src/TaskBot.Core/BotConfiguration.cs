using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBot.Core
{
    class BotConfiguration
    {
        public string BotToken { get; }

        public BotConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            BotToken = configuration["BotConfiguration:BotToken"] ?? throw new InvalidOperationException("TelegramBotToken is missing in appsettings.json");
        }
    }
}
