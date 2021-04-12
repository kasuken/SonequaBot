using SonequaBot.Shared.Commands.Interfaces;
using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandSoLillo : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!solillo";

        protected override CommandActivationComparison ActivationComparison => CommandActivationComparison.Contains;

        public string GetImageEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/solillo.gif";
        }
    }
}