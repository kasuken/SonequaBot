using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandChoc : CommandBase, IResponseImage, IResponseAudio
    {
        protected override string ActivationCommand => "!choc";

        public string GetImageEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/choc.mp4";
        }

        public string GetAudioEvent(CommandSource source)
        {
            return null;
        }
    }
}