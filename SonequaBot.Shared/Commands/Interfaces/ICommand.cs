namespace SonequaBot.Shared.Commands.Interfaces
{
    public enum CommandActivationComparison
    {
        StartsWith = 0,
        Contains = 1,
        Exactly = 2
    }

    public interface ICommand
    {
        /**
         * Return true if command is triggered.
         */
        bool IsActivated(CommandSource source);

        /**
         * Get activation string.
         */
        CommandActivationComparison GetComparisonMethod();

        /**
         * Get activation string.
         */
        string GetActivationCommand();
    }
}