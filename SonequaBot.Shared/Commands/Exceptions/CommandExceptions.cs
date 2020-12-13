using System;

namespace SonequaBot.Commands.Exceptions
{
    public class CommandException : Exception
    {
        // %input% refers to the command sent by the user
        // %command% refers to the command suggested by the bot.

        public static string CommandNotValid = "%input% does not exists.";
        public static string CommandNotValidSuggest = "%input% does not exists. (Did you mean to use \"%command%\" ?)";
        public static string CommandNeedsStringArgument = "%command% command needs a target (ex. %command% Valentino).";
        public static string CommandUnavailable = "%command% is currently unavailable.";
        public static string CommandNeedsNumberArgument = "%command% needs an argument (ex %command% 3).";

        public CommandException(string message, string input, string command = "default")
        {
            message = message.Replace("%input%", input);
            message = message.Replace("%command%", command);
            Message = message;
        }

        public override string Message { get; }
    }
}