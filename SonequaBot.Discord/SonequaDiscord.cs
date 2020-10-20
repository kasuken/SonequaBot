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

namespace SonequaBot.Discord
{
    public class SonequaDiscord : BackgroundService
    {
        private DiscordClient _discordClient;
        private CommandsNextModule _commands;

        private readonly ILogger<SonequaDiscord> _logger;

        public SonequaDiscord(ILogger<SonequaDiscord> logger)
        {
            _logger = logger;

            _discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = "NzYxMjI0Njg4MDcyMjYxNjQz.X3XfvA.nQHHtTztcze0wKP2lZQedRHcvOo",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = DSharpPlus.LogLevel.Debug
            });

            _commands = _discordClient.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "!"
            });

            _commands.RegisterCommands<SlapCommand>();

            _discordClient.ConnectAsync().GetAwaiter().GetResult();
            Task.Delay(-1).GetAwaiter().GetResult();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

        }
    }
}
