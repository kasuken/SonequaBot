using SonequaBot.Shared.Commands.Interfaces;
using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandJava : CommandBase, IResponseMessage
    {
        protected override CommandActivationComparison ActivationComparison => CommandActivationComparison.Contains;
        protected override string ActivationCommand => " java ";

        public string GetMessageEvent(CommandSource source)
        {
            return $"Hey {source.User}! Dillo ancora se hai coraggio!!!";
        }
    }
}