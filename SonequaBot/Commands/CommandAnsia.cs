using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandAnsia : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand => "!ansia";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendAnsia";
        }
    }
}