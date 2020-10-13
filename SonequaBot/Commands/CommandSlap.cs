using System;
using System.Text.RegularExpressions;
using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandSlap : CommandBase, IResponseMessage
    {
        private string _target;
        protected override string ActivationCommand => "!slap {@string target}";

        public string GetMessage(OnMessageReceivedArgs e)
        {
            switch (_target.ToLower())
            {
                case "kasuken":
                    return $"{e.ChatMessage.DisplayName} sorry, i can't is my creator!";
                
                case "sonequabot":
                    return $"{e.ChatMessage.DisplayName} hey dude, did you think i slap myself? FY! I slap YOU MF!";
            }

            string enrich = "";
            switch (new Random().Next(0, 5))
            {
                case 0: enrich = " with violence"; break;
                case 1: enrich = " with a beautiful Microsoft whip"; break;
                case 2: enrich = " with a mouse"; break;
                case 3: enrich = " with a keyboard"; break;
                case 4: enrich = " yelling : be my MSDOS!!!! and bless Bill Gates! Amen."; break;
            }
            
            return $"{e.ChatMessage.DisplayName} at your service! I slap {_target}{enrich}.";
        }

        public override bool IsActivated(string message)
        {
            if (message.Length < 5)
            {
                return false;
            }

            var activated = message.StartsWith(ActivationCommand.Substring(0, 5), StringComparison.InvariantCultureIgnoreCase);
            if (!activated) return false;

            var match = Regex.Match(message, @"^!slap\s*(\S+)");
            if (match.Success)
            {
                _target = match.Groups[1].Value;
                return true;
            }

            throw new Exception("!slap command need a target (ex. !slap Valentino");
        }
    }
}
