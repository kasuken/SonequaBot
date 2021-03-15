using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandBergarambo : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!bergarambo";

        public string GetImageEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/bergarambo.gif";
        }
    }
}
