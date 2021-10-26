using SonequaBot.Shared.Commands.DTO;

namespace SonequaBot.Shared.Commands.Interfaces.Responses
{
    public interface IResponseImageCard
    {
        ImageCardData GetImageCardEvent(CommandSource source);
    }
}