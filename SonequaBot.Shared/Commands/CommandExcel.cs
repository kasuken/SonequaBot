using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandExcel : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!excel";

        public string GetImageEvent(CommandSource source)
        {
            return
                "https://media.discordapp.net/attachments/488341298445549568/763654797429047306/121089450_1476976829171640_9052994754153725829_o.png?width=695&height=685";
        }
    }
}