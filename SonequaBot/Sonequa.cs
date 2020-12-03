using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SonequaBot.Commands;
using SonequaBot.Commands.Interfaces;
using SonequaBot.Commands.Interfaces.Responses;
using SonequaBot.Models;
using SonequaBot.Services;
using SonequaBot.Shared;
using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace SonequaBot
{
    public class Sonequa : BackgroundService
    {
        private readonly ILogger<Sonequa> _logger;

        private readonly ConnectionCredentials _connectionCredentials;
        private readonly TwitchClient _client = new TwitchClient();
        private readonly TwitchAPI _twitchApi = new TwitchAPI();

        private readonly string[] BotUsers = { "sonequabot", "streamelements" };

        private readonly Dictionary<string, ConnectedUser> _connectedUsers = new Dictionary<string, ConnectedUser>();

        private readonly List<ICommand> _botCommands = new List<ICommand>();
        private readonly HubConnection _connection;

        private readonly SonequaSettings _options;

        private readonly SentimentAnalysisService _sentimentAnalysisService;
        private readonly List<SentimentScores> _sentimentScores = new List<SentimentScores>();
        private readonly SentimentScores _currentChatSentiment = new SentimentScores();

        public Sonequa(ILogger<Sonequa> logger, SonequaSettings options, SentimentAnalysisService sentimentAnalysisService)
        {
            _logger = logger;
            _options = options;
            _sentimentAnalysisService = sentimentAnalysisService;

            _connectionCredentials = new ConnectionCredentials(_options.BotUsername, _options.BotToken);

            _connection = new HubConnectionBuilder()
                .WithUrl(_options.SonequaWebUrl)
                .Build();

            _connection.Closed += async error =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Connect();

            try
            {
                await _connection.StartAsync(stoppingToken);
                _logger.LogInformation("Connection started");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private void Connect()
        {
            _logger.LogInformation("Connecting...");

            _twitchApi.Settings.ClientId = _options.ClientId;

            InitializeBotCommands();
            InitializeBot();
        }

        private void InitializeBotCommands()
        {
            //BotCommands.Add(new CommandHi()); Not needed in twitch
            _botCommands.Add(new CommandJava());
            _botCommands.Add(new CommandPhp());
            _botCommands.Add(new CommandDevastante());
            _botCommands.Add(new CommandSlap());
            _botCommands.Add(new CommandDiceRoll());
            _botCommands.Add(new CommandFriday());
            _botCommands.Add(new CommandDisagio());
            _botCommands.Add(new CommandGren());
            _botCommands.Add(new CommandDebug());
            _botCommands.Add(new CommandDio());
            _botCommands.Add(new CommandPaura());
            _botCommands.Add(new CommandKasu());
            _botCommands.Add(new CommandMerda());
            _botCommands.Add(new CommandAnsia());
            _botCommands.Add(new CommandAccompagnare());
            _botCommands.Add(new CommandZinghero());
        }

        private void InitializeBot()
        {
            _client.Initialize(_connectionCredentials, _options.ChannelName);
            _client.Connect();

            _client.OnUserJoined += Client_OnUserJoined;
            _client.OnUserLeft += Client_OnUserLeft;

            _client.OnConnected += Client_OnConnected;
            _client.OnMessageReceived += Client_OnMessageReceived;
        }

        private void Client_OnUserLeft(object sender, OnUserLeftArgs e)
        {
            _connectedUsers.Remove(e.Username);

            _logger.LogWarning($"The user left: {e.Username}");
            _logger.LogWarning($"Total user on channel: {_connectedUsers.Count}");
        }

        private async void Client_OnUserJoined(object sender, OnUserJoinedArgs e)
        {
            _connectedUsers.Add(e.Username, new ConnectedUser
            {
                Username = e.Username
            });

            await _connection.SendAsync("SendTask", "SendUserAppear", e.Username);

            _logger.LogWarning($"New user on channel: {e.Username}");
            _logger.LogWarning($"Total user on channel: {_connectedUsers.Count}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            _client.SendMessage(_options.ChannelName, "Hi to everyone. I am Sonequabot and I am alive. Again.");
        }


        private async void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            String invoker = e.ChatMessage.Username;

            try
            {
                foreach (var command in _botCommands.Where(command => command.IsActivated(e.ChatMessage.Message)))
                {
                    switch (true)
                    {
                        case true when command is IResponseMessage commandMessage:
                            _client.SendMessage(_options.ChannelName, commandMessage.GetMessage(e));
                            break;

                        case true when command is IResponseVisual commandVisual:
                            await _connection.SendAsync("SendTask", commandVisual.GetVisualEvent(e), "");
                            break;
                    }

                    return; // if activated exit, if not multiple sentiment of !devastante will be UBER negative 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _client.SendWhisper(invoker, ex.Message);
            }

            await ProcessSentiment(e);
        }

        private async Task ProcessSentiment(OnMessageReceivedArgs e)
        {
            // to remove noise 
            if (e.ChatMessage.Message.Length < 10)
            {
                return;
            }

            // limit number of stored SentimentScores to react quickly to changes 
            if (_sentimentScores.Count > 10)
            {
                _sentimentScores.RemoveAt(0);
            }

            var currentScore = _sentimentAnalysisService.ElaborateSentence(e.ChatMessage.Message);

            await _connection.SendAsync("SendTask", "SendSentiment", currentScore.GetSentiment().ToString().ToLower());

            var currentUnrankedSentiment = new Dictionary<SentimentScores.TextSentiment, double>
            {
                {SentimentScores.TextSentiment.Positive, currentScore.Positive},
                {SentimentScores.TextSentiment.Neutral, currentScore.Neutral},
                {SentimentScores.TextSentiment.Negative, currentScore.Negative},
            };

            var currentRankedSentiment = currentUnrankedSentiment.OrderBy(item => item.Value);

            _logger.LogInformation(string.Concat(
                    "currentScore:",
                    Environment.NewLine,
                    string.Join(
                        Environment.NewLine,
                        currentRankedSentiment.Select(a => $"{a.Key}: {a.Value}")
                    )
                )
            );

            var processedSentiment = new SentimentScores();
            var currentRankedSentimentLast = currentRankedSentiment.Last();
            switch (currentRankedSentimentLast.Key)
            {
                case SentimentScores.TextSentiment.Positive:
                    processedSentiment.Positive = currentRankedSentimentLast.Value;
                    break;
                case SentimentScores.TextSentiment.Neutral:
                    processedSentiment.Neutral = currentRankedSentimentLast.Value;
                    break;
                case SentimentScores.TextSentiment.Negative:
                    processedSentiment.Negative = currentRankedSentimentLast.Value;
                    break;
                case SentimentScores.TextSentiment.Mixed:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _sentimentScores.Add(processedSentiment);

            var chatUnrankedSentiment = new Dictionary<SentimentScores.TextSentiment, double>
            {
                {SentimentScores.TextSentiment.Positive, _sentimentScores.Average(c => c.Positive)},
                {SentimentScores.TextSentiment.Neutral, _sentimentScores.Average(c => c.Neutral)},
                {SentimentScores.TextSentiment.Negative, _sentimentScores.Average(c => c.Negative)},
            };

            var chatRankedSentiment = chatUnrankedSentiment.OrderBy(item => item.Value);

            // set current chat sentiment with values
            _currentChatSentiment.SetSentiment(chatRankedSentiment.Last().Key);
            _currentChatSentiment.Positive = chatUnrankedSentiment[SentimentScores.TextSentiment.Positive];
            _currentChatSentiment.Neutral = chatUnrankedSentiment[SentimentScores.TextSentiment.Neutral];
            _currentChatSentiment.Negative = chatUnrankedSentiment[SentimentScores.TextSentiment.Negative];

            _logger.LogInformation(string.Concat(
                    "Chat sentiment:",
                    Environment.NewLine,
                    string.Join(
                        Environment.NewLine,
                        chatRankedSentiment.Select(a => $"{a.Key}: {a.Value}")
                    )
                )
            );

            // interesting can be used with a gauge or a vertical meter that can go from -1 to 1
            double absoluteSentiment = (_currentChatSentiment.Positive - _currentChatSentiment.Neutral) -
                                       (_currentChatSentiment.Negative - _currentChatSentiment.Neutral);
            _logger.LogInformation(string.Concat("(", _sentimentScores.Count.ToString(), ")", " - Absolute sentiment:", absoluteSentiment));

            await _connection.SendAsync("SendTask", "SendGaugeSentiment", absoluteSentiment);
        }
    }
}
