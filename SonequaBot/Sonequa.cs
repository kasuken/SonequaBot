using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SonequaBot.Sentiment.Processors;
using SonequaBot.Shared;
using SonequaBot.Shared.Commands;
using SonequaBot.Shared.Commands.Interfaces;
using SonequaBot.Shared.Commands.Interfaces.Responses;
using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace SonequaBot
{
    public class Sonequa : BackgroundService
    {
        private readonly ILogger<Sonequa> _logger;

        private readonly SonequaSettings _options;

        private readonly Sentiment.Sentiment _sentiment;

        private readonly string[] BotUsers = {"sonequabot", "streamelements"};
        
        private readonly TwitchClient client = new TwitchClient();
        private readonly HubConnection connection;

        private readonly ConnectionCredentials connectionCredentials;
        private readonly TwitchAPI twitchAPI = new TwitchAPI();

        private readonly List<ICommand> BotCommands = new List<ICommand>();

        private readonly Dictionary<string, ConnectedUser> ConnectedUsers = new Dictionary<string, ConnectedUser>();

        public Sonequa(ILogger<Sonequa> logger, SonequaSettings options)
        {
            _logger = logger;
            _options = options;

            connectionCredentials = new ConnectionCredentials(_options.BotUsername, _options.BotToken);

            connection = new HubConnectionBuilder()
                .WithUrl(_options.SonequaWebUrl)
                .Build();

            connection.Closed += async error =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            var sentimentProcessor = new ProcessorCognitive(
                new AzureKeyCredential(options.CognitiveAzureKey),
                new Uri(options.CognitiveEndPoint)
            );

            _sentiment = new Sentiment.Sentiment(
                sentimentProcessor
            );
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Connect();

            try
            {
                await connection.StartAsync();
                _logger.LogInformation("Connection started");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        internal void Connect()
        {
            _logger.LogInformation("Connecting...");

            twitchAPI.Settings.ClientId = _options.ClientId;

            InitializeBotCommands();
            InitializeBot();
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
                    _logger.LogInformation("Load BotCommand : " +  fqcnType.ToString());
                    BotCommands.Add((ICommand) Activator.CreateInstance(fqcnType));
                }
            }
        }

        private void InitializeBot()
        {
            client.Initialize(connectionCredentials, _options.ChannelName);
            client.Connect();

            client.OnUserJoined += Client_OnUserJoined;
            client.OnUserLeft += Client_OnUserLeft;

            client.OnConnected += Client_OnConnected;
            client.OnMessageReceived += Client_OnMessageReceived;
        }

        private void Client_OnUserLeft(object sender, OnUserLeftArgs e)
        {
            ConnectedUsers.Remove(e.Username);

            _logger.LogWarning($"The user left: {e.Username}");
            _logger.LogWarning($"Total user on channel: {ConnectedUsers.Count}");
        }

        private async void Client_OnUserJoined(object sender, OnUserJoinedArgs e)
        {
            ConnectedUsers.Add(e.Username, new ConnectedUser(e.Username));

            await connection.SendAsync("SendTask", "SendUserAppear", e.Username);

            _logger.LogWarning($"New user on channel: {e.Username}");
            _logger.LogWarning($"Total user on channel: {ConnectedUsers.Count}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            client.SendMessage(_options.ChannelName, "Hi to everyone. I am Sonequabot and I am alive. Again.");
        }


        private async void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            var source = new CommandSource
            {
                Channel = e.ChatMessage.Channel,
                Message = e.ChatMessage.Message,
                User = e.ChatMessage.Username
            };

            if (Array.Exists(BotUsers, element => element == source.User)) return;

            try
            {
                foreach (var command in BotCommands)
                    if (command.IsActivated(source))
                    {
                        if (command is IResponseMessage messageText)
                            client.SendMessage(_options.ChannelName, messageText.GetMessageEvent(source));

                        if (command is IResponseImage messageImage)
                            await connection.SendAsync(
                                "SendTask", "SendCreateImage", messageImage.GetImageEvent(source));

                        if (command is IResponseVideo messageVideo)
                            await connection.SendAsync(
                                "SendTask", "SendCreateVideo", messageVideo.GetVideoEvent(source));

                        if (command is IResponseAudio messageAudio)
                            await connection.SendAsync(
                                "SendTask", "SendCreateAudio", messageAudio.GetAudioEvent(source));

                        return; // if activated exit, if not multiple sentiment of !devastante will be UBER negative 
                    }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                client.SendWhisper(source.User, ex.Message);
            }

            await ProcessSentiment(e);
        }

        private async Task ProcessSentiment(OnMessageReceivedArgs e)
        {
            _sentiment.AddMessage(e.ChatMessage.Message);

            var report = "LAST MESSAGE" + Environment.NewLine;
            report += "Label : " + _sentiment.GetSentimentLastLabel() + Environment.NewLine;
            report += "POS : " + _sentiment.GetSentimentLast().GetScore().Positive;
            report += "|";
            report += "NEG : " + _sentiment.GetSentimentLast().GetScore().Negative;
            report += "|";
            report += "NEU : " + _sentiment.GetSentimentLast().GetScore().Negative + Environment.NewLine;

            report += "AVERAGE SENTIMENT" + Environment.NewLine;
            report += "Avg POS : " + _sentiment.GetSentimentAverage().Positive;
            report += "|";
            report += "Avg NEG : " + _sentiment.GetSentimentAverage().Negative;
            report += "|";
            report += "Avg NEU : " + _sentiment.GetSentimentAverage().Neutral + Environment.NewLine;

            report += "Avg Label : " + _sentiment.GetSentimentAverageLabel() + Environment.NewLine;
            report += "Absolute Index : " + _sentiment.GetSentimentAbsolute();
            report += "|";
            report += "Label : " + _sentiment.GetSentimentAbsoluteLabel() + Environment.NewLine;
                   
            _logger.LogInformation(report);

            await connection.SendAsync("SendTask", "SendSentiment",
                _sentiment.GetSentimentLastLabel().ToString().ToLower());
            await connection.SendAsync("SendTask", "SendGaugeSentiment", _sentiment.GetSentimentAbsolute());
        }
    }
}