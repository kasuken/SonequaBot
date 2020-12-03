using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandAccompagnare : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand => "!accompagnare";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendAccompagnare";
        }
    }
}