using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SonequaBot.Discord.Commands;
using SonequaBot.Shared;

namespace SonequaBot.Discord
{
    public class SonequaDiscord : BackgroundService
    {
        private DiscordClient _discordClient;
        private CommandsNextModule _commands;

        private readonly ILogger<SonequaDiscord> _logger;

        public SonequaDiscord(ILogger<SonequaDiscord> logger, SonequaSettings options)
        {
            _logger = logger;

            _discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = options.BotToken,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = DSharpPlus.LogLevel.Debug
            });

            _commands = _discordClient.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "!"
            });

            _commands.RegisterCommands<HiCommand>();
            _commands.RegisterCommands<BucoCommand>();

            _discordClient.ConnectAsync().GetAwaiter().GetResult();
            Task.Delay(-1).GetAwaiter().GetResult();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

        }
    }
}
