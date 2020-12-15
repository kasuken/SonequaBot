using System;
using System.Collections.Generic;
using System.Linq;
using SonequaBot.Sentiment.Interfaces;

namespace SonequaBot.Sentiment.Models
{
    public class SentimentMessage : ICloneable
    {
        private readonly string _message;
        private SentimentScore _score;
        private Sentiment.TextSentiment _textSentiment;

        public SentimentMessage(string message)
        {
            _message = message;
        }

        public SentimentMessage Process(IProcessor processor)
        {
            if (null != _score) throw new Exception("Message cannot be processed multiple times");

            _score = processor.Process(GetMessage());

            _textSentiment = GetRankedLabel(GetScore());

            return this;
        }

        public static Sentiment.TextSentiment GetRankedLabel(SentimentScore messageScore)
        {
            var sentimentRank = new Dictionary<Sentiment.TextSentiment, double>
            {
                {Sentiment.TextSentiment.Positive, messageScore.Positive},
                {Sentiment.TextSentiment.Neutral, messageScore.Neutral},
                {Sentiment.TextSentiment.Negative, messageScore.Negative}
            };

            if (messageScore.Neutral.CompareTo(messageScore.Positive) == 0 &&
                messageScore.Neutral.CompareTo(messageScore.Negative) == 0) return Sentiment.TextSentiment.Neutral;

            return sentimentRank.OrderBy(item => item.Value).Last().Key;
        }

        public string GetMessage()
        {
            return _message;
        }

        public SentimentScore GetScore()
        {
            if (null == _score) throw new Exception("Message must be processed to get score");

            return (SentimentScore) _score.Clone();
        }

        public Sentiment.TextSentiment GetSentimentLabel()
        {
            if (null == _score) throw new Exception("Message must be processed to get TextSentiment");

            return _textSentiment;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}