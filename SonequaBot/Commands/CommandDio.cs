using SonequaBot.Commands.Interfaces.Responses;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandDio : CommandBase, IResponseVisual
    {
        protected override string ActivationCommand
        {
            get { return "!dio"; }
        }

        public string GetVisualEvent(OnMessageReceivedArgs e)
        {
            return "SendDio";
        }
    }
}