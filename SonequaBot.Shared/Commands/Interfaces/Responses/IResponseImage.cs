namespace SonequaBot.Shared.Commands.Interfaces.Responses
{
    public interface IResponseImage
    {
        /**
         * Get visual event to answer via SignalR
         */
        string GetImageEvent(CommandSource source);
    }
}