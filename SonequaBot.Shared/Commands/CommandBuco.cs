using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandBuco : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand => "!buco";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendBuco";
        }
    }
}