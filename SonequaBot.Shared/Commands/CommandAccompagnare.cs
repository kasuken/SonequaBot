using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandAccompagnare : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!accompagnare";

        public string GetImageEvent(CommandSource source)
        {
            return
                "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/accompagnare.jpg";
        }
    }
}