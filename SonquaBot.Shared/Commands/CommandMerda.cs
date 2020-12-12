using SonequaBot.Shared.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Shared.Commands
{
    public class CommandMerda : CommandBase, IResponseVideo
    {
        protected override string ActivationCommand => "!merda";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendMerda";
        }

        public string GetVideoEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/merda.mp4";
        }
    }
}