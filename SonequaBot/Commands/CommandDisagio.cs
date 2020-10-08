using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandDisagio : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand => "!disagio";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendDisagio";
        }
    }
}
