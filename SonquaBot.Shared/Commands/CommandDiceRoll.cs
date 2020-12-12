using System;
using System.Text.RegularExpressions;
using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandDiceRoll : CommandBase, IResponseMessage
    {
        private const int DefaultDiceFaces = 6;
        private readonly Random _rnd = new Random();

        protected override string ActivationCommand => "!roll{@int dice faces}";

        public override bool IsActivated(CommandSource source)
        {
            return source.Message.Length >= 5 && source.Message.Substring(0, 5) == "!roll";
        }

        public string GetMessageEvent(CommandSource source)
        {
            var match = Regex.Match(source.Message, "^!roll([0-9]+)");
            var diceFaces = !match.Success ? DefaultDiceFaces : int.Parse(match.Groups[1].Value);

            return $"{source.User} roll a {_rnd.Next(1, diceFaces)} (1d{diceFaces})";
        }
    }
}