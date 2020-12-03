using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandMerda : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand => "!merda";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendMerda";
        }
    }
}