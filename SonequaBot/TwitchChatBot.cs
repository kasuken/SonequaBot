using System;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Models;

namespace SonequaBot
{
    internal class TwitchChatBot
    {
        ConnectionCredentials connectionCredentials = new ConnectionCredentials(TwitchInfo.BotUsername, TwitchInfo.BotToken);
        TwitchClient client;
        TwitchAPI twitchAPI = new TwitchAPI();

        public TwitchChatBot()
        {
        }

        internal void Connect()
        {
            Console.WriteLine("Connecting...");

            client = new TwitchClient();

            client.OnLog += Client_OnLog;
            client.OnConnectionError += Client_OnConnectionError;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnUserTimedout += Client_OnUserTimedout;
            client.OnNewSubscriber += Client_OnNewSubscriber;

            client.Initialize(connectionCredentials, TwitchInfo.ChannelName);
            client.Connect();

            twitchAPI.Settings.ClientId = TwitchInfo.ClientId;
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
            client.SendWhisper(e.WhisperMessage.Username, $"your said: { e.WhisperMessage.Message}");
        }

        private void Client_OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.StartsWith("hi", StringComparison.InvariantCultureIgnoreCase))
            {
                client.SendMessage(TwitchInfo.ChannelName, $"Hey there { e.ChatMessage.DisplayName }");
            }
            else if (e.ChatMessage.Message.StartsWith("!uptime", StringComparison.InvariantCultureIgnoreCase))
            {
                var upTime = GetUpTime().Result;

                client.SendMessage(TwitchInfo.ChannelName, upTime?.ToString() ?? "Offline");
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

        internal void Disconnect()
        {
            Console.WriteLine("Disconnecting...");
        }
    }
}