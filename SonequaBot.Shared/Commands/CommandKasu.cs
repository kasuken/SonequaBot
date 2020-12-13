using SonequaBot.Shared.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Shared.Commands
{
    public class CommandKasu : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!kasu";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendKasu";
        }

        public string GetImageEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/kasu.gif";
        }
    }
}