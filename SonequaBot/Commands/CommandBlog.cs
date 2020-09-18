using TwitchLib.Api;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandBlog : AbstractCommand
    {
        protected new string Command = "!twitter";

        public override string GetResponseMessage(TwitchAPI twitchApi, object sender, OnMessageReceivedArgs e)
        {
            return $"My blog: {TwitchInfo.Blog}";
        }
    }
}