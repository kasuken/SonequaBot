using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandFrullino : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!frullino";

        public string GetImageEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/frullino.gif";
        }
    }
}
