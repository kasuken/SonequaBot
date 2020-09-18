using System;
using TwitchLib.Api;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public abstract class AbstractCommand
    {
        protected string Command;

        public string GetCommand()
        {
            return Command;
        }

        public virtual bool IsActivated(string message)
        {
            return message.StartsWith(Command, StringComparison.InvariantCultureIgnoreCase);
        }

        public abstract string GetResponseMessage(TwitchAPI twitchApi, object sender, OnMessageReceivedArgs e);
    }
}