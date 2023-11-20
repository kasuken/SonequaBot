using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SonequaBot.Shared;
using SonequaBot.Shared.Commands;
using SonequaBot.Shared.Commands.Interfaces;
using SonequaBot.Shared.Commands.Interfaces.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SonequaBot.Discord
{
	public class SonequaDiscord : BackgroundService
	{
		private DiscordClient _discordClient;

		public CommandsNextExtension _commands { get; set; }

		private readonly ILogger<SonequaDiscord> _logger;

		private readonly List<ICommand> BotCommands = new List<ICommand>();

		private readonly string[] BotUsers = { "sonequabot", "streamelements" };

		public SonequaDiscord(ILogger<SonequaDiscord> logger, SonequaSettings options)
		{
			_logger = logger;

			_discordClient = new DiscordClient(new DiscordConfiguration
			{
				Token = options.BotToken,
				TokenType = TokenType.Bot,
				MinimumLogLevel = LogLevel.Debug
			});

			_commands = _discordClient.UseCommandsNext(new CommandsNextConfiguration()
			{
				UseDefaultCommandHandler = false
			});

			InitializeBotCommands();

			_discordClient.MessageCreated += CommandHandler;

			_discordClient.ConnectAsync().GetAwaiter().GetResult();
			Task.Delay(-1).GetAwaiter().GetResult();
		}

		private async Task CommandHandler(DiscordClient client, MessageCreateEventArgs e)
		{
			var source = new CommandSource
			{
				Channel = e.Channel.Name,
				Message = e.Message.Content,
				User = e.Author.Username
			};

			if (Array.Exists(BotUsers, element => element.ToLowerInvariant() == source.User.ToLowerInvariant())) return;

			try
			{
				foreach (var command in BotCommands)
				{
					if (command.IsActivated(source))
					{
						if (command is IResponseMessage messageText)
						{
							await e.Channel.SendMessageAsync(messageText.GetMessageEvent(source));
						}

						if (command is IResponseImage messageImage)
						{
							var embed = new DiscordEmbedBuilder
							{
								ImageUrl = messageImage.GetImageEvent(source)
							};
							await e.Channel.SendMessageAsync("", embed);
						}

						if (command is IResponseVideo messageVideo)
						{
							var embed = new DiscordEmbedBuilder
							{
								Url = messageVideo.GetVideoEvent(source)
							};
							await e.Channel.SendMessageAsync("", embed);
						}

						if (command is IResponseAudio messageAudio)
						{
							var embed = new DiscordEmbedBuilder
							{
								Url = messageAudio.GetAudioEvent(source)
							};
							await e.Channel.SendMessageAsync("", embed);
						}

						if (command is IResponseImageCard messageCard)
						{
							var cardData = messageCard.GetImageCardEvent(source);

							var embed = new DiscordEmbedBuilder
							{
								Title = cardData.Title,
								Description = cardData.Description,
								ImageUrl = cardData.ImageUrl,
								Url = cardData.Url
							};
							await e.Channel.SendMessageAsync("", embed);
						}

						return;
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				await e.Channel.SendMessageAsync(ex.Message);
			}
		}

		private void InitializeBotCommands()
		{
			List<Type> types = Assembly.Load("SonequaBot.Shared").GetTypes()
				.Where(t => t.Namespace == "SonequaBot.Shared.Commands")
				.ToList();

			foreach (Type fqcnType in types)
			{
				if (fqcnType.BaseType != null && fqcnType.BaseType == typeof(CommandBase))
				{
					_logger.LogInformation("Load BotCommand : " + fqcnType.ToString());
					BotCommands.Add((ICommand)Activator.CreateInstance(fqcnType));
				}
			}
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("SonequaBot is starting.");

			stoppingToken.Register(() => _logger.LogInformation("SonequaBot is stopping."));

			while (!stoppingToken.IsCancellationRequested)
			{
				_logger.LogInformation("SonequaBot is doing background work.");

				await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
			}

			_logger.LogInformation("SonequaBot background task is stopping.");
		}
	}
}
