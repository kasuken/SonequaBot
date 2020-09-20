using System;
using System.Text.RegularExpressions;
using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandDiceRoll : CommandBase, IResponseMessage
    {
        private readonly Random _rnd = new Random();

        private const int DefaultDiceFaces = 6;
        protected string Command = "!roll{@int dice faces}";

        public string GetMessage(OnMessageReceivedArgs e)
        {
            var match = Regex.Match(e.ChatMessage.Message, "^!roll([0-9]+)");
            var diceFaces = !match.Success ? DefaultDiceFaces : int.Parse(match.Groups[1].Value);

            return $"{e.ChatMessage.DisplayName} roll a {_rnd.Next(1, diceFaces)} (1d{diceFaces})";
        }

        public override bool IsActivated(string message)
        {
            return base.IsActivated(message.Substring(0, 5));
        }
    }
}