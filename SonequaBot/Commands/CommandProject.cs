using TwitchLib.Api;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandProject : AbstractCommand
    {
        protected new string Command = "!project";

        public override string GetResponseMessage(TwitchAPI twitchApi, object sender, OnMessageReceivedArgs e)
        {
            return $"I'm working on {TwitchInfo.ProjectDescription}.";
        }
    }
}