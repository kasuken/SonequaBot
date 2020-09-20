using System;
using System.Runtime.CompilerServices;
using SonequaBot.Commands.Interfaces;

namespace SonequaBot.Commands
{
    public abstract class CommandBase :ICommand
    {
        /**
         * The Command string for activation. 
         */
        protected string ActivationCommand;
        
        /**
         * The Comparison type for activation. 
         */
        protected CommandActivationComparison ActivationComparison = CommandActivationComparison.StartsWith;

        public bool IsActivated(string message)
        {
            switch (GetComparisonMethod())
            {
                case CommandActivationComparison.StartsWith:
                    return message.StartsWith(GetActivationCommand(), StringComparison.InvariantCultureIgnoreCase);
                
                case CommandActivationComparison.Contains:
                    return message.Contains(GetActivationCommand(), StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        public CommandActivationComparison GetComparisonMethod()
        {
            return ActivationComparison;
        }

        public string GetActivationCommand()
        {
            return ActivationCommand;
        }
    }
}