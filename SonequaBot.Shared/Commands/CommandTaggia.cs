using SonequaBot.Shared.Commands.Interfaces;
using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandTaggia : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!taggia";

        public string GetImageEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/taggia.gif";
        }
    }
}