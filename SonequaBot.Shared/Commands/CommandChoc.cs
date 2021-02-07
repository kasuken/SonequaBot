using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandChoc : CommandBase, IResponseVideo
    {
        protected override string ActivationCommand => "!choc";

        public string GetVideoEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/choc.mp4";
        }
    }
}