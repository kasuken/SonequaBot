using System;
using System.Text.RegularExpressions;
using SonequaBot.Commands.Interfaces;
using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandDiceRoll : CommandBase, IResponseMessage
    {
        protected string Command = "!roll{dice-faces}";

        private int _diceFaces = 6;
        private readonly Random _rnd = new Random();
        
        public override bool IsActivated(string message)
        {
            _diceFaces = 6; // reset dice faces after a roll
            
            var activated = message.StartsWith("!roll", StringComparison.InvariantCultureIgnoreCase);
            if (!activated) return false;

            var match = Regex.Match(message, "^!roll([0-9]+)");
            if (!match.Success) return true;

            _diceFaces = int.Parse(match.Groups[1].Value);

            return true;
        }

        public string GetMessage(OnMessageReceivedArgs e)
        {
            return $"{e.ChatMessage.DisplayName} roll a {_rnd.Next(1, _diceFaces)} (1d{_diceFaces})";
        }
    }
}