using System;
using SonequaBot.Commands.Interfaces;
using SonequaBot.Commands.Exceptions;

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
            if(message.Contains(GetActivationCommand(), StringComparison.InvariantCultureIgnoreCase)){
                switch (GetComparisonMethod()){

                    case CommandActivationComparison.Contains:
                        return true;

                    case CommandActivationComparison.StartsWith:
                        if(message.StartsWith(GetActivationCommand(), StringComparison.InvariantCultureIgnoreCase)) 
                            return true;
                        else
                            throw new CommandException(CommandException.CommandNotValidSuggest,message,ActivationCommand);
                    case CommandActivationComparison.Exactly:
                        if(message == ActivationCommand)
                            return true;
                        else 
                            throw new CommandException(CommandException.CommandNotValidSuggest,message,ActivationCommand);
                    default:
                        return false;
                }
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