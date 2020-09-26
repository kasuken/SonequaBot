using SonequaBot.Commands.Interfaces;
using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandJava : CommandBase, IResponseMessage
    {
        protected override CommandActivationComparison ActivationComparison => CommandActivationComparison.Contains;
        protected override string ActivationCommand => " java ";

        public string GetMessage(OnMessageReceivedArgs e)
        {
            return $"Hey {e.ChatMessage.DisplayName}! Dillo ancora se hai coraggio!!!";
        }
    }
}