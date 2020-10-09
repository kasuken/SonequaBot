using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandGren : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand => "!gren";

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendGren";
        }
    }
}