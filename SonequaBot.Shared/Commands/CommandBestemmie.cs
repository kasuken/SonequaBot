using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandBestemmie : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!bestemmie";

        public string GetImageEvent(CommandSource source)
        {
            return  "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/bestemmie.jpg";
        }
    }
}