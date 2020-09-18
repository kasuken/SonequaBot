using TwitchLib.Api;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandTwitter : AbstractCommand
    {
        protected new string Command = "!twitter";

        public override string GetResponseMessage(TwitchAPI twitchApi, object sender, OnMessageReceivedArgs e)
        {
            return $"Follow me on Twitter: {TwitchInfo.Twitter}";
        }
    }
}