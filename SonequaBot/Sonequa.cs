using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SonequaBot.Commands;
using SonequaBot.Commands.Interfaces;
using SonequaBot.Commands.Interfaces.Responses;
using SonequaBot.Models;
using SonequaBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Models;

namespace SonequaBot
{
    public class Sonequa : BackgroundService
    {
        private readonly ILogger<Sonequa> _logger;

        readonly ConnectionCredentials connectionCredentials;
        private readonly TwitchClient client = new TwitchClient();
        readonly TwitchAPI twitchAPI = new TwitchAPI();

        private readonly string[] BotUsers = new string[] { "sonequabot", "streamelements" };

        Dictionary<string, ConnectedUser> ConnectedUsers = new Dictionary<string, ConnectedUser>();

        List<ICommand> BotCommands = new List<ICommand>();
        private readonly HubConnection connection;

        private readonly SonequaSettings _options;

        private readonly SentimentAnalysisService _sentimentAnalysisService;
        private List<SentimentScores> sentimentScores = new List<SentimentScores>();
        private SentimentScores currentChatSentiment = new SentimentScores();

        private double positiveAverage = 0.0;
        private double neutralAverage = 0.0;
        private double negativeAverage = 0.0;

        public Sonequa(ILogger<Sonequa> logger, SonequaSettings options, SentimentAnalysisService sentimentAnalysisService)
        {
            _logger = logger;
            _options = options;
            _sentimentAnalysisService = sentimentAnalysisService;

            connectionCredentials = new ConnectionCredentials(_options.BotUsername, _options.BotToken);

            connection = new HubConnectionBuilder()
                .WithUrl(_options.SonequaWebUrl)
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Connect();

            await connection.StartAsync();
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
            //BotCommands.Add(new CommandHi()); Not needed in twitch
            BotCommands.Add(new CommandJava()); ;
            BotCommands.Add(new CommandPhp());
            BotCommands.Add(new CommandDevastante());
            BotCommands.Add(new CommandSlap());
            BotCommands.Add(new CommandDiceRoll());
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

        private void Client_OnUserLeft(object sender, TwitchLib.Client.Events.OnUserLeftArgs e)
        {
            ConnectedUsers.Remove(e.Username);

            _logger.LogWarning($"The user left: {e.Username}");
            _logger.LogWarning($"Total user on channel: {ConnectedUsers.Count}");
        }

        private void Client_OnUserJoined(object sender, TwitchLib.Client.Events.OnUserJoinedArgs e)
        {
            ConnectedUsers.Add(e.Username, new ConnectedUser(e.Username));

            _logger.LogWarning($"New user on channel: {e.Username}");
            _logger.LogWarning($"Total user on channel: {ConnectedUsers.Count}");
        }

        private void Client_OnConnected(object sender, TwitchLib.Client.Events.OnConnectedArgs e)
        {
            client.SendMessage(_options.ChannelName, $"Hi to everyone. I am Sonequabot and I am alive. Again.");
        }

        private async void Client_OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
        {
            try
            {
                foreach (var command in BotCommands)
                {
                    if (command.IsActivated(e.ChatMessage.Message))
                    {
                        switch (true)
                        {
                            case true when command is IResponseMessage commandMessage:
                                client.SendMessage(_options.ChannelName, commandMessage.GetMessage(e));
                                break;

                            case true when command is IResponseVisual commandVisual:
                                await connection.SendAsync(commandVisual.GetVisualEvent(e));
                                break;
                        }
                        
                        return; // if activated exit, if not multiple sentiment of !devastante will be UBER negative 
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                //client.SendWhisper(e.ChatMessage.Username, ex.Message);
            }

            if (e.ChatMessage.Message.Length > 10)
            {
                var currentScore = _sentimentAnalysisService.ElaborateSentence(e.ChatMessage.Message);

                sentimentScores.Add(currentScore);

                await connection.SendAsync("SentimentRealTime", currentScore.GetSentiment().ToString().ToLower());

                if (currentScore.Neutral > currentScore.Negative && currentScore.Neutral > currentScore.Negative)
                {
                    neutralAverage = sentimentScores.Average(c => c.Neutral);
                    currentChatSentiment.Neutral = neutralAverage;

                    currentChatSentiment.SetSentiment(SentimentScores.TextSentiment.Neutral);
                }

                if (currentScore.Positive > currentScore.Negative && currentScore.Positive > currentScore.Neutral)
                {
                    positiveAverage = sentimentScores.Average(c => c.Positive);
                    currentChatSentiment.Positive = positiveAverage;

                    currentChatSentiment.SetSentiment(SentimentScores.TextSentiment.Positive);
                }

                if (currentScore.Negative > currentScore.Positive && currentScore.Negative > currentScore.Neutral)
                {
                    negativeAverage = sentimentScores.Average(c => c.Negative);
                    currentChatSentiment.Negative = negativeAverage;

                    currentChatSentiment.SetSentiment(SentimentScores.TextSentiment.Negative);
                }

                if (neutralAverage > positiveAverage && neutralAverage > negativeAverage)
                {
                    currentChatSentiment.Neutral = neutralAverage;
                    currentChatSentiment.SetSentiment(SentimentScores.TextSentiment.Neutral);
                }

                if (positiveAverage > negativeAverage && positiveAverage > neutralAverage)
                {
                    currentChatSentiment.Positive = positiveAverage;
                    currentChatSentiment.SetSentiment(SentimentScores.TextSentiment.Positive);
                }

                if (negativeAverage > positiveAverage && negativeAverage > neutralAverage)
                {
                    currentChatSentiment.Negative = negativeAverage;
                    currentChatSentiment.SetSentiment(SentimentScores.TextSentiment.Negative);
                }

                await connection.SendAsync("Sentiment", currentChatSentiment.GetSentiment().ToString().ToLower());
            }
        }
    }
}
