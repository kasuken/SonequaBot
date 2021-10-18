using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandFacepalm : CommandBase, IResponseAudio, IResponseImage
    {
        protected override string ActivationCommand => "!facepalm";

        public string GetAudioEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/spfx/facepalm.mp3";
        }

        public string GetImageEvent(CommandSource source)
        {
            return  "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/facepalm.gif";
        }
    }
}