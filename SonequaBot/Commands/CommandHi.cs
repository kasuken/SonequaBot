using TwitchLib.Api;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandHi : AbstractCommand
    {
        protected new string Command = "Hi";

        public override string GetResponseMessage(TwitchAPI twitchApi, object sender, OnMessageReceivedArgs e)
        {
            return $"Hey there {e.ChatMessage.DisplayName}.";
        }
    }
}