using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandDebug : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand => "!debug";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendDebug";
        }
    }
}