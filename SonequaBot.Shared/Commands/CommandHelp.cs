using SonequaBot.Shared.Commands.Interfaces;
using SonequaBot.Shared.Commands.Interfaces.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SonequaBot.Shared.Commands
{
    public class CommandHelp : CommandBase, IResponseMessage
    {
        protected override string ActivationCommand => "!help";

        public string GetMessageEvent(CommandSource source)
        {
            var builder = new System.Text.StringBuilder();
            builder.AppendLine($"Salve, ~~Professor Falken~~, ehm, {source.User}. A che gioco vuoi giocare?");
            builder.AppendLine(string.Empty);

            List<Type> types = Assembly.Load("SonequaBot.Shared").GetTypes()
                .Where(t => t.Namespace == "SonequaBot.Shared.Commands")
                .ToList();

            foreach (Type t in types)
            {
                if (t.BaseType != null && t.BaseType == typeof(CommandBase))
                {
                    var command = (ICommand)Activator.CreateInstance(t);

                    builder.AppendLine(command.GetActivationCommand());
                }
            }

            return builder.ToString();
        }
    }
}