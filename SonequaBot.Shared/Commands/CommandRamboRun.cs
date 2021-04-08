using SonequaBot.Shared.Commands.Interfaces;
using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandRamboRun : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!ramborun";

        protected override CommandActivationComparison ActivationComparison => CommandActivationComparison.Contains;

        public string GetImageEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/ramborun.gif";
        }
    }
}