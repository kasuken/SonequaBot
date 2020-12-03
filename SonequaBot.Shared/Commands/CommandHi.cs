using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandHi : CommandBase, IResponseMessage, IResponseDiscord
    {
        protected override string ActivationCommand => "hi";

        public string GetMessage(OnMessageReceivedArgs e)
        {
            return $"Hey there {e.ChatMessage.DisplayName}.";
        }

        public async Task DiscordCommand(CommandContext ctx, string name)
        {
            await ctx.RespondAsync($"👋 Hi, {ctx.User.Mention}!");
        }
    }
}