using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandPaura : CommandBase, IResponseImage, IResponseAudio
    {
        protected override string ActivationCommand => "!paura";

        public string GetImageEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/paura.png";
        }

        public string GetAudioEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/spfx/spfx/paura.mp3";
        }
    }
}