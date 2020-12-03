using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandExcel : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand => "!excel";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendExcel";
        }
    }
}