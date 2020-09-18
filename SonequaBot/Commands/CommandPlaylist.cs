using TwitchLib.Api;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandPlaylist : AbstractCommand
    {
        protected new string Command = "!playlist";

        public override string GetResponseMessage(TwitchAPI twitchApi, object sender, OnMessageReceivedArgs e)
        {
            return $"Playlist for my live on Twitch: {TwitchInfo.Playlist}";
        }
    }
}