using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Models;

namespace SonequaBot
{
    internal class TwitchChatBot
    {
        readonly ConnectionCredentials connectionCredentials = new ConnectionCredentials(TwitchInfo.BotUsername, TwitchInfo.BotToken);
        TwitchClient client;
        readonly TwitchAPI twitchAPI = new TwitchAPI();

        string[] BotUsers = new string[] { "sonequabot", "streamelements" };

        List<string> UsersOnline = new List<string>();

        public TwitchChatBot()
        {
        }

        internal void Connect()
        {
            Console.WriteLine("Connecting...");

            twitchAPI.Settings.ClientId = TwitchInfo.ClientId;

            InizializeBot();
        }

        private void InizializeBot()
        {
            client = new TwitchClient();

            client.OnLog += Client_OnLog;
            client.OnConnectionError += Client_OnConnectionError;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnUserTimedout += Client_OnUserTimedout;
            client.OnNewSubscriber += Client_OnNewSubscriber;
            client.OnUserJoined += Client_OnUserJoined;
            client.OnUserLeft += Client_OnUserLeft;

            client.Initialize(connectionCredentials, TwitchInfo.ChannelName);
            client.Connect();

            client.OnConnected += Client_OnConnected;
        }

        private void Client_OnConnected(object sender, TwitchLib.Client.Events.OnConnectedArgs e)
        {
            client.SendMessage(TwitchInfo.ChannelName, $"Hi to everyone. I am Sonequabot and I am alive. Again.");
        }

        private void Client_OnNewSubscriber(object sender, TwitchLib.Client.Events.OnNewSubscriberArgs e)
        {
            client.SendMessage(TwitchInfo.ChannelName, $"Thank you for the subscription {e.Subscriber.DisplayName}!!! I really appreciate it!");
        }

        private void Client_OnUserTimedout(object sender, TwitchLib.Client.Events.OnUserTimedoutArgs e)
        {
            client.SendMessage(TwitchInfo.ChannelName, $"User {e.UserTimeout.Username} timed out.");
        }

        private void Client_OnWhisperReceived(object sender, TwitchLib.Client.Events.OnWhisperReceivedArgs e)
        {
            //client.SendWhisper(e.WhisperMessage.Username, $"your said: { e.WhisperMessage.Message}");
        }

        private void Client_OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.StartsWith("hi", StringComparison.InvariantCultureIgnoreCase))
            {
                client.SendMessage(TwitchInfo.ChannelName, $"Hey there { e.ChatMessage.DisplayName }.");
            }
            else if (e.ChatMessage.Message.StartsWith("!uptime", StringComparison.InvariantCultureIgnoreCase))
            {
                var upTime = GetUpTime().Result;

                client.SendMessage(TwitchInfo.ChannelName, upTime?.ToString() ?? "Offline");
            }
            else if (e.ChatMessage.Message.StartsWith("!project", StringComparison.InvariantCultureIgnoreCase))
            {
                client.SendMessage(TwitchInfo.ChannelName, $"I'm working on {TwitchInfo.ProjectDescription}.");
            }
            else if (e.ChatMessage.Message.StartsWith("!instagram", StringComparison.InvariantCultureIgnoreCase))
            {
                client.SendMessage(TwitchInfo.ChannelName, $"Follow me on Instagram: {TwitchInfo.Instagram}");
            }
            else if (e.ChatMessage.Message.StartsWith("!twitter", StringComparison.InvariantCultureIgnoreCase))
            {
                client.SendMessage(TwitchInfo.ChannelName, $"Follow me on Twitter: {TwitchInfo.Twitter}");
            }
            else if (e.ChatMessage.Message.StartsWith("!blog", StringComparison.InvariantCultureIgnoreCase))
            {
                client.SendMessage(TwitchInfo.ChannelName, $"My blog: {TwitchInfo.Blog}");
            }
            else if (e.ChatMessage.Message.StartsWith("!playlist", StringComparison.InvariantCultureIgnoreCase))
            {
                client.SendMessage(TwitchInfo.ChannelName, $"Playlist for my live on Twitch: {TwitchInfo.Playlist}");
            }
            else if (e.ChatMessage.Message.StartsWith("!discord", StringComparison.InvariantCultureIgnoreCase))
            {
                client.SendMessage(TwitchInfo.ChannelName, $"Vieni sul mio canale Discord: {TwitchInfo.Discord}");
            }
        }

        private async Task<TimeSpan?> GetUpTime()
        {
            var userId = await GetUserId(TwitchInfo.ChannelName);

            return await twitchAPI.V5.Streams.GetUptimeAsync(userId);
        }

        async Task<string> GetUserId(string username)
        {
            var userList = await twitchAPI.V5.Users.GetUserByNameAsync(username);

            return userList.Matches[0].Id;
        }

        private void Client_OnConnectionError(object sender, TwitchLib.Client.Events.OnConnectionErrorArgs e)
        {
            Console.WriteLine(e.Error.Message);
        }

        private void Client_OnLog(object sender, TwitchLib.Client.Events.OnLogArgs e)
        {
            Console.WriteLine(e.Data);
        }

        void Client_OnUserJoined(object sender, TwitchLib.Client.Events.OnUserJoinedArgs e)
        {
            if (BotUsers.Contains(e.Username)) return;

            try
            {
                //client.SendMessage(TwitchInfo.ChannelName, $"Welcome on my channel, { e.Username }.");

                UsersOnline.Add(e.Username);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void Client_OnUserLeft(object sender, TwitchLib.Client.Events.OnUserLeftArgs e)
        {
            UsersOnline.Remove(e.Username);
        }

        internal void Disconnect()
        {
            Console.WriteLine("Disconnecting...");
        }
    }
}