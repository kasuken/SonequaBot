using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandPhp : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand => "!php";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendPhp";
        }
    }
}