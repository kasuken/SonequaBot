using TwitchLib.Client.Events;

namespace SonequaBot.Commands.Interfaces.Responses
{
    public interface IResponseVisual
    {
        /**
         * Get visual event to answer via SignalR
         */
        string GetVisualEvent(OnMessageReceivedArgs e);
    }
}