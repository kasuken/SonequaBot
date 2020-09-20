using System;
using System.Text.RegularExpressions;
using SonequaBot.Commands.Interfaces;
using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandSlap : CommandBase, IResponseMessage
    {
        protected string Command = "!slap {target}";

        private string _target;
        
        public override bool IsActivated(string message)
        {   
            var activated = message.StartsWith("!slap", StringComparison.InvariantCultureIgnoreCase);
            if (!activated) return false;

            var match = Regex.Match(message, @"^!slap\s*(\S+)");
            if (match.Success)
            {
                _target = match.Groups[1].Value;
                return true;
            }
            
            throw new Exception("!slap command need a target (ex. !slap Valentino");
        }

        public string GetMessage(OnMessageReceivedArgs e)
        {
            return $"{e.ChatMessage.DisplayName} slap {_target}.";
        }
    }
}