using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SonequaBot.Discord.Commands
{
    public class BucoCommand
    {
        [Command("buco")]
        public async Task Buco(CommandContext ctx)
        {
            await ctx.RespondAsync("https://media.discordapp.net/attachments/488341298445549568/767791907446849536/116879423_10157719508528425_4476752716855431460_o.png?width=1178&height=685");
        }
    }
}
