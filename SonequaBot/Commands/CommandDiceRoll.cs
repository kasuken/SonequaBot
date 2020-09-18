using System;
using System.Text.RegularExpressions;
using TwitchLib.Api;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandDiceRoll : AbstractCommand
    {
        protected new string Command = "!roll{dice-faces}";
        protected int DiceFaces = 6;
        private readonly Random _rnd = new Random();

        public override bool IsActivated(string message)
        {
            var activated = message.StartsWith("!roll", StringComparison.InvariantCultureIgnoreCase);
            if (!activated) return false;

            var match = Regex.Match(message, "^!roll([0-9]+)");
            if (!match.Success) return true;

            DiceFaces = int.Parse(match.Groups[1].Value);

            return true;
        }

        public override string GetResponseMessage(TwitchAPI twitchApi, object sender, OnMessageReceivedArgs e)
        {
            return $"{e.ChatMessage.DisplayName} roll a {_rnd.Next(1, DiceFaces)} (1d{DiceFaces})";
        }
    }
}