using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandPaura : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand => "!paura";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendPaura";
        }
    }
}