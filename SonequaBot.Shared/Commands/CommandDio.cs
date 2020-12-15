using System;
using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandDio : CommandBase, IResponseImage, IResponseAudio
    {
        protected override string ActivationCommand => "!dio";

        public string GetImageEvent(CommandSource source)
        {
            return "https://media1.tenor.com/images/c0945841a8cea8da4351a9f892288507/tenor.gif";
        }

        public string GetAudioEvent(CommandSource source)
        {
            var rand = (new Random()).Next(1, 3);
            return $"/spfx/dio_{rand}.mp3";
        }
    }
}