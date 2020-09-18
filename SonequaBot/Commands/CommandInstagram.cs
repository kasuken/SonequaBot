using TwitchLib.Api;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandInstagram : AbstractCommand
    {
        protected new string Command = "!instagram";

        public override string GetResponseMessage(TwitchAPI twitchApi, object sender, OnMessageReceivedArgs e)
        {
            return $"Follow me on Instagram: {TwitchInfo.Instagram}";
        }
    }
}