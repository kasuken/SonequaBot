using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandHi : CommandBase, IResponseMessage
    {
        protected override string ActivationCommand => "hi";

        public string GetMessage(OnMessageReceivedArgs e)
        {
            return $"Hey there {e.ChatMessage.DisplayName}.";
        }
    }
}