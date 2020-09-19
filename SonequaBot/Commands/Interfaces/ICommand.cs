using System;

namespace SonequaBot.Commands.Interfaces
{
    public enum CommandActivationComparison
    {
        StartsWith = 0,
        Contains = 1
    };
    
    public interface ICommand
    {
        /**
         * Get activation string.
         */
        CommandActivationComparison GetComparisonMethod();
        
        /**
         * Get activation string.
         */
        string GetCommand();
    }
}