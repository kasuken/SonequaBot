using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandFriday : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!friday";

        public string GetImageEvent(CommandSource source)
        {
            return "https://media.giphy.com/media/26gsv0tbVbQqCZwAg/giphy.gif";
        }
    }
}