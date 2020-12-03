using System;

namespace SonequaBot.Commands.Exceptions
{
    public class CommandException : Exception
        {
        public CommandException(string message, string input,string command = "default"){
            message = message.Replace("%input%",input);
            message = message.Replace("%command%",command);
            Message = message;
        }

        public override string Message{get;}

        // %input% refers to the command sent by the user
        // %command% refers to the command suggested by the bot.

        public const string CommandNotValid = "%input% does not exists.";
        public const string CommandNotValidSuggest = "%input% does not exists. (Did you mean to use \"%command%\" ?)";
        public const string CommandNeedsStringArgument = "%command% command needs a target (ex. %command% Valentino).";
        public const string CommandUnavailable = "%command% is currently unavailable.";
        public const string CommandNeedsNumberArgument = "%command% needs an argument (ex %command% 3).";
    }
}