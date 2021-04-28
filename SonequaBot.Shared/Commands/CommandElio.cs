using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandElio : CommandBase, IResponseAudio, IResponseImage
    {
        protected override string ActivationCommand => "!elio";

        public string GetAudioEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/spfx/elio.mp3";
        }

        public string GetImageEvent(CommandSource source)
        {
            return  "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/elio.gif";
        }
    }
}