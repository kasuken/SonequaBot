using SonequaBot.Sentiment.Models;

namespace SonequaBot.Sentiment.Interfaces
{
    public interface IProcessor
    {
        SentimentScore Process(string sentence);
    }
}