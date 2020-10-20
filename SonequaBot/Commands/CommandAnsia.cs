using SonequaBot.Commands.Interfaces.Responses;
using SonequaBot.Commands.Interfaces;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandAnsia : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand => "!ansia";

        protected override CommandActivationComparison ActivationComparison => CommandActivationComparison.Contains;

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendAnsia";
        }
    }
}