using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandHi : CommandBase, IResponseMessage
    {
        protected override string ActivationCommand => "hi";

        public string GetMessageEvent(CommandSource source)
        {
            return $"Hey there {source.User}.";
        }
    }
}