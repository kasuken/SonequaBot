namespace SonequaBot.Commands.Interfaces
{
    public enum CommandActivationComparison
    {
        StartsWith = 0,
        Contains = 1
    }

    public interface ICommand
    {
        /**
         * Return true if command is triggered.
         */
        bool IsActivated(string message);

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