using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandZingari : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!zingari";

        public string GetImageEvent(CommandSource source)
        {
            return
                "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/zingari.jpg";
        }
    }
}