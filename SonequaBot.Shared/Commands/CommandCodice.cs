using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandCodice : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!codice";

        public string GetImageEvent(CommandSource command)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/Codice_perfetto.png";
        }
    }
}