namespace SonequaBot.Shared.Commands.Interfaces.Responses
{
    public interface IResponseAudio
    {
        /**
         * Get visual event to answer via SignalR
         */
        string GetAudioEvent(CommandSource source);
    }
}