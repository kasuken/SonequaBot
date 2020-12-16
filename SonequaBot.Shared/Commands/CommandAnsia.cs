using SonequaBot.Shared.Commands.Interfaces;
using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandAnsia : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!ansia";

        protected override CommandActivationComparison ActivationComparison => CommandActivationComparison.Contains;

        public string GetImageEvent(CommandSource source)
        {
            return "https://i.imgur.com/5Zt13Ilh.jpg";
        }
    }
}