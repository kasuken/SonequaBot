using System.Linq;
using System.Reflection;
using SonequaBot.Sentiment;
using SonequaBot.Sentiment.Interfaces;
using SonequaBot.Sentiment.Models;

namespace SonequaBot.Tests.Sentiment
{
    public class MockSentimentProcessorFixed : IProcessor
    {
        public SentimentScore FixedScore { get; set; }

        public SentimentScore Process(string sentence)
        {
            return FixedScore;
        }
    }

    public class MockSentimentProcessorPredefinedScore : IProcessor
    {
        public int index = -1;
        public SentimentScore[] scores { get; set; }

        public SentimentScore Process(string sentence)
        {
            index++;
            return scores.ElementAt(index);
        }
    }


    public class SentimentTestBase
    {
        protected SonequaBot.Sentiment.Sentiment getSentimentFixed(int scoreFixedValue, int limitHistory,
            int ignoreMessageShorterThan)
        {
            var fixedProcessor = new MockSentimentProcessorFixed();
            fixedProcessor.FixedScore = new SentimentScore
            {
                Positive = scoreFixedValue,
                Negative = scoreFixedValue,
                Neutral = scoreFixedValue
            };

            return new SonequaBot.Sentiment.Sentiment(fixedProcessor, limitHistory, ignoreMessageShorterThan);
        }

        protected SentimentHistory GetReflectionHistory(SonequaBot.Sentiment.Sentiment sentiment)
        {
            return (SentimentHistory) typeof(SonequaBot.Sentiment.Sentiment)
                .GetField("_history", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(sentiment);
        }

        protected int GetReflectionHistoryLength(SonequaBot.Sentiment.Sentiment sentiment)
        {
            return GetReflectionHistory(sentiment).GetAll().Count;
        }
    }
}