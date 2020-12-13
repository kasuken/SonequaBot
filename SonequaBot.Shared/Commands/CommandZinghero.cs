using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandZinghero : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!zinghero";

        public string GetImageEvent(CommandSource source)
        {
            return
                "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/zinghero.jpg";
        }
    }
}