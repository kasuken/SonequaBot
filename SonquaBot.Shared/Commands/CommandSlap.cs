using System;
using System.Text.RegularExpressions;
using SonequaBot.Commands.Exceptions;
using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandSlap : CommandBase, IResponseMessage
    {
        private string _target;
        protected override string ActivationCommand => "!slap {@string target}";

        public string GetMessageEvent(CommandSource source)
        {
            switch (_target.ToLower())
            {
                case "@kasuken":
                case "kasuken":
                    return $"{source.User} sorry, i can't is my creator!";

                case "@sonequabot":
                case "sonequabot":
                case "myself":
                case "me":

                    return $"{source.User} hey dude, did you think i slap myself? FY! I slap YOU MF!";
            }

            var enrich = "";
            switch (new Random().Next(0, 5))
            {
                case 0:
                    enrich = " with violence";
                    break;
                case 1:
                    enrich = " with a beautiful Microsoft whip";
                    break;
                case 2:
                    enrich = " with a mouse";
                    break;
                case 3:
                    enrich = " with a keyboard";
                    break;
                case 4:
                    enrich = " yelling : be my MSDOS!!!! and bless Bill Gates! Amen.";
                    break;
            }

            return $"{source.User} at your service! I slap {_target}{enrich}.";
        }

        public override bool IsActivated(CommandSource source)
        {
            var message = source.Message;

            if (message.Length < 5) return false;
            var activated = message.StartsWith(ActivationCommand.Substring(0, 5),
                StringComparison.InvariantCultureIgnoreCase);
            if (!activated) return false;

            if (message == "!slap" || message == "!slap ")
                throw new CommandException(CommandException.CommandNeedsStringArgument, message, "!slap");

            var match = Regex.Match(message, @"^!slap \s*(\S+)");
            if (match.Success)
            {
                _target = match.Groups[1].Value;
                return true;
            }

            throw new CommandException(CommandException.CommandNotValidSuggest, message, "!slap");
        }
    }
}