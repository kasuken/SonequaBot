using SonequaBot.Commands.Interfaces;

namespace SonequaBot.Commands
{
    public abstract class CommandBase :ICommand
    {
        protected string Command;
        protected CommandActivationComparison ActivationComparison;

        public CommandActivationComparison GetComparisonMethod()
        {
            return ActivationComparison;
        }

        public string GetCommand()
        {
            return Command;
        }
    }
}