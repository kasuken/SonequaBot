using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            if (e.ChatMessage.Message.Contains(" java ", StringComparison.InvariantCultureIgnoreCase))
            {
                client.SendMessage(TwitchInfo.ChannelName, $"Hey { e.ChatMessage.DisplayName }! Dillo ancora se hai coraggio!!!");
            }

            if (e.ChatMessage.Message.StartsWith("!devastante",StringComparison.InvariantCultureIgnoreCase))
            {
                await connection.SendAsync("SendDevastante");
            }

            if (e.ChatMessage.Message.StartsWith("!php", StringComparison.InvariantCultureIgnoreCase))
            {
                await connection.SendAsync("SendPhp");
            }
        }
    }
}