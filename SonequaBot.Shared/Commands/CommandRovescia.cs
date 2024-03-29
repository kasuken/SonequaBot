﻿using SonequaBot.Shared.Commands.Interfaces.Responses;

namespace SonequaBot.Shared.Commands
{
    public class CommandRovescia : CommandBase, IResponseImage
    {
        protected override string ActivationCommand => "!rovescia";

        public string GetImageEvent(CommandSource source)
        {
            return "https://raw.githubusercontent.com/kasuken/SonequaBot/master/SonequaBot.Web/wwwroot/img/rovescia.jpg";
        }
    }
}