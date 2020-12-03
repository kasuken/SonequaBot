using TwitchLib.Client.Events;

namespace SonequaBot.Commands.Interfaces.Responses
{
    public interface IResponseMessage
    {
        /**
         * Get response message to answer via chat
         */
        string GetMessage(OnMessageReceivedArgs e);
    }
}