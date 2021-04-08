namespace SonequaBot.Shared.Commands.Interfaces.Responses
{
    public interface IResponseVideo
    {
        /**
         * Get visual event to answer via SignalR
         */
        string GetVideoEvent(CommandSource source);
    }
}