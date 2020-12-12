using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandDisagio : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!disagio";

        public string GetImageEvent(CommandSource source)
        {
            return "https://i.imgur.com/lmPrHzoh.jpg";
        }
    }
}