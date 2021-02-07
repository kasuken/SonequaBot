using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandChoc : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!choc";

        public string GetImageEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/choc.gif";
        }
    }
}
