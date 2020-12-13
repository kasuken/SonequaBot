namespace SonequaBot.Shared.Commands.Interfaces.Responses
{
    public interface IResponseMessage
    {
        /**
         * Get response message to answer via chat
         */
        string GetMessageEvent(CommandSource source);
    }
}