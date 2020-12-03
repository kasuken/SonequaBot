using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandKasu : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand => "!kasu";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendKasu";
        }
    }
}
