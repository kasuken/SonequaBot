using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandBuco : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!buco";

        public string GetImageEvent(CommandSource source)
        {
            return "https://media.discordapp.net/attachments/488341298445549568/767791907446849536/116879423_10157719508528425_4476752716855431460_o.png?width=1178&height=685";
        }
    }
}