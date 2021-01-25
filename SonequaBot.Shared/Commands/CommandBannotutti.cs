using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandBannotutti : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!bannotutti";

        public string GetImageEvent(CommandSource source)
        {
            return
                "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/bannotutti.jpg";
        }
    }
}