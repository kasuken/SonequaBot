using System.Threading.Tasks;
using DSharpPlus.CommandsNext;

namespace SonequaBot.Commands.Interfaces.Responses
{
    public interface IResponseDiscord
    {
        Task DiscordCommand(CommandContext ctx, string name);
    }
}