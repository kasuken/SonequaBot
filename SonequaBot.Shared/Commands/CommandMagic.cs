using SonequaBot.Shared.Commands.Interfaces;
using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandMagic : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!magic";

        protected override CommandActivationComparison ActivationComparison => CommandActivationComparison.Contains;

        public string GetImageEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/magic.gif";
        }
    }
}