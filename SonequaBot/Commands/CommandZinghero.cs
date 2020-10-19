using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandZinghero : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand => "!zinghero";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendZinghero";
        }
    }
}