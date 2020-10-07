using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandFriday : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand => "!friday";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendFriday";
        }
    }
}