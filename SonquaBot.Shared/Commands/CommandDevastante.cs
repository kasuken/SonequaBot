using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandDevastante : CommandBase, IResponseImage, IResponseAudio
    {
        protected override string ActivationCommand => "!devastante";

        public string GetImageEvent(CommandSource source)
        {
            return "https://media.giphy.com/media/fVDWqUcVscRxCkQVAR/200w_d.gif";
        }

        public string GetAudioEvent(CommandSource source)
        {
            return "~/spfx/devastante.mp3";
        }
    }
}