using System;
using SonequaBot.Commands.Interfaces;

namespace SonequaBot.Commands
{
    public abstract class CommandBase : ICommand
    {
        /**
         * The Command string for activation.
         */
        protected abstract string ActivationCommand
        {
            get;
        }

        /**
         * The Comparison type for activation.
         */
        protected virtual CommandActivationComparison ActivationComparison => CommandActivationComparison.StartsWith;

        public virtual bool IsActivated(string message)
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

        public virtual CommandActivationComparison GetComparisonMethod()
        {
            return ActivationComparison;
        }

        public virtual string GetActivationCommand()
        {
            return ActivationCommand;
        }
    }
}