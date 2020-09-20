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
using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace SonequaBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        readonly ConnectionCredentials connectionCredentials = new ConnectionCredentials(TwitchInfo.BotUsername, TwitchInfo.BotToken);
        TwitchClient client;
        readonly TwitchAPI twitchAPI = new TwitchAPI();

        string[] BotUsers = new string[] { "sonequabot", "streamelements" };
        
        Dictionary<string,ConnectedUser> ConnectedUsers = new Dictionary<string, ConnectedUser>();
        
        List<ICommand> BotCommands = new List<ICommand>();

        HubConnection connection;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            connection = new HubConnectionBuilder()
                .WithUrl("https://kasukensonequabotweb.azurewebsites.net/sonequaBotHub")
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
            Console.WriteLine("Connecting...");

            twitchAPI.Settings.ClientId = TwitchInfo.ClientId;

            InitializeBot();
        }

        private void InitializeBot()
        {
            client = new TwitchClient();

            client.Initialize(connectionCredentials, TwitchInfo.ChannelName);
            client.Connect();

            client.OnUserJoined += Client_OnUserJoined;
            client.OnUserLeft += Client_OnUserLeft;

            client.OnConnected += Client_OnConnected;
            client.OnMessageReceived += Client_OnMessageReceived;
        }

        private void InitializeBotCommands()
        {
            //BotCommands.Add(new CommandHi()); Not needed in twitch
            BotCommands.Add(new CommandJava());;
            BotCommands.Add(new CommandPhp());
            BotCommands.Add(new CommandDevastante());
        }

        private void Client_OnUserLeft(object sender, TwitchLib.Client.Events.OnUserLeftArgs e)
        {
            ConnectedUsers.Remove(e.Username);
        }

        private void Client_OnUserJoined(object sender, TwitchLib.Client.Events.OnUserJoinedArgs e)
        {
            ConnectedUsers.Add(e.Username, new ConnectedUser(e.Username));
        }

        private void Client_OnConnected(object sender, TwitchLib.Client.Events.OnConnectedArgs e)
        {
            client.SendMessage(TwitchInfo.ChannelName, $"Hi to everyone. I am Sonequabot and I am alive. Again.");
        }

        private async void Client_OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
        {
            foreach (var command in BotCommands)
            {
                if (command.IsActivated(e.ChatMessage.Message))
                {
                    switch (true)
                    {
                        case true when command is IResponseMessage commandMessage:
                            client.SendMessage(TwitchInfo.ChannelName, commandMessage.GetMessage(e));
                            break;
                        
                        case true when command is IResponseVisual commandVisual:
                            await connection.SendAsync( commandVisual.GetVisualEvent(e));
                            break;
                    }
                }
            }
        }
    }
}
