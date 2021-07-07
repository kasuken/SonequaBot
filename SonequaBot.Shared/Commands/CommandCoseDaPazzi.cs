using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandCoseDaPazzi : CommandBase, IResponseAudio, IResponseImage
    {
        protected override string ActivationCommand => "!cosedapazzi";

        public string GetAudioEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/spfx/cosedapazzi.mp3";
        }

        public string GetImageEvent(CommandSource source)
        {
            return  "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/cosedapazzi.gif";
        }
    }
}