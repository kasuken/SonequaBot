using System;
using SonequaBot.Commands.Interfaces;
using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandHi : CommandBase, IResponseMessage
    {
        protected new string Command = "hi";
        
        public string GetMessage(OnMessageReceivedArgs e)
        {
            return $"Hey there {e.ChatMessage.DisplayName}.";
        }
    }
}