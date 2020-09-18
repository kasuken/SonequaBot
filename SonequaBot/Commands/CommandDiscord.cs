using TwitchLib.Api;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandDiscord : AbstractCommand
    {
        protected new string Command = "!discord";

        public override string GetResponseMessage(TwitchAPI twitchApi, object sender, OnMessageReceivedArgs e)
        {
            return $"Vieni sul mio canale Discord: {TwitchInfo.Discord}";
        }
    }
}