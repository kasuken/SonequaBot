using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandFritz : CommandBase, IResponseAudio, IResponseImage
    {
        protected override string ActivationCommand => "!fritz";

        public string GetAudioEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/spfx/fritz.mp3";
        }

        public string GetImageEvent(CommandSource source)
        {
            return  "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/fritz.jpg";
        }
    }
}