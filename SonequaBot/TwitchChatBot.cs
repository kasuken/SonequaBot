using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SonequaBot.Commands;
using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace SonequaBot
{
    internal class TwitchChatBot
    {
        private readonly ConnectionCredentials connectionCredentials =
            new ConnectionCredentials(TwitchInfo.BotUsername, TwitchInfo.BotToken);

        private readonly TwitchAPI twitchAPI = new TwitchAPI();

        private readonly string[] BotUsers = {"sonequabot", "streamelements"};

        private TwitchClient client;

        private readonly List<string> UsersOnline = new List<string>();

        private List<AbstractCommand> BotCommands = new List<AbstractCommand>();

        internal void Connect()
        {
            Console.WriteLine("Connecting...");

            twitchAPI.Settings.ClientId = TwitchInfo.ClientId;

            InitializeCommands();
            InitializeBot();
        }

        private void InitializeCommands()
        {
            BotCommands.Add(new CommandHi());
            BotCommands.Add(new CommandUptime());
            BotCommands.Add(new CommandProject());
            BotCommands.Add(new CommandPlaylist());
            BotCommands.Add(new CommandDiscord());
            BotCommands.Add(new CommandBlog());
            BotCommands.Add(new CommandInstagram());
            BotCommands.Add(new CommandTwitter());
        }

        private void InitializeBot()
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

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            client.SendMessage(TwitchInfo.ChannelName, "Hi to everyone. I am Sonequabot and I am alive. Again.");
        }

        private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            client.SendMessage(TwitchInfo.ChannelName,
                $"Thank you for the subscription {e.Subscriber.DisplayName}!!! I really appreciate it!");
        }

        private void Client_OnUserTimedout(object sender, OnUserTimedoutArgs e)
        {
            client.SendMessage(TwitchInfo.ChannelName, $"User {e.UserTimeout.Username} timed out.");
        }

        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            //client.SendWhisper(e.WhisperMessage.Username, $"your said: { e.WhisperMessage.Message}");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (!(e.ChatMessage.Message.StartsWith('!')))
            {
                return;
            }
            
            List<string> info = new List<string>();
            info.Add("Unknown command.");
            info.Add("List command :");
            
            foreach (var command in BotCommands)
            {
                info.Add(command.GetCommand());
                if (command.IsActivated(e.ChatMessage.Message))
                {
                    client.SendMessage(TwitchInfo.ChannelName,command.GetResponseMessage(twitchAPI, sender, e));
                    return;
                }
            }
            
            client.SendWhisper(e.ChatMessage.DisplayName, String.Join("\r\n", info.ToArray()));
        }

        private void Client_OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            Console.WriteLine(e.Error.Message);
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private void Client_OnUserJoined(object sender, OnUserJoinedArgs e)
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

        private void Client_OnUserLeft(object sender, OnUserLeftArgs e)
        {
            UsersOnline.Remove(e.Username);
        }

        internal void Disconnect()
        {
            Console.WriteLine("Disconnecting...");
        }
    }
}