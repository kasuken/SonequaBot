using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandPhp : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!php";

        public string GetImageEvent(CommandSource source)
        {
            return "https://i.imgur.com/O4CgHJG.gif";
        }
    }
}