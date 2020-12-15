using System;
using System.Collections.Generic;
using System.Linq;
using SonequaBot.Sentiment.Interfaces;
using SonequaBot.Sentiment.Models;

namespace SonequaBot.Sentiment
{
    public class Sentiment
    {
        public enum TextSentiment
        {
            Positive = 0,
            Neutral = 1,
            Negative = 2
        }

        /// <summary>
        /// Ignore message shorter than this length.
        /// </summary>
        private readonly int _discardLowerThanChar;

        /// <summary>
        /// Message history
        /// </summary>
        private readonly SentimentHistory _history;

        /// <summary>
        /// Message processor
        /// </summary>
        private readonly IProcessor _processor;

        /// <summary>
        /// Customizable list of TextSentiment for Absolute calculations.
        /// </summary>
        public List<string> TextSentimentAbsolute = new List<string>
        {
            "Perfect",
            "Excellent",
            "Incredible",
            "Superb",
            "Fantastic",
            "Awesome",
            "Very good",
            "Really good",
            "Great",
            "Good",
            "Pretty good",
            "Above average",
            "Decent",
            "Quite good",
            "Somewhat good",
            "Fine",

            "Normal",

            "Mediocre",
            "Somewhat bad",
            "Pretty bad",
            "Quite bad",
            "Poor",
            "Below Average",
            "Bad",
            "Unsatisfactory",
            "Rubbish",
            "Really bad",
            "Very bad",
            "Terrible",
            "Awful",
            "Dreadful",
            "Appalling",
            "Abysmal"
        };

        public Sentiment(IProcessor processor, int historyLength = 10, int discardLowerThanChar = 10)
        {
            _processor = processor;
            _discardLowerThanChar = discardLowerThanChar;
            _history = new SentimentHistory(historyLength);
        }

        /// <summary>
        /// Add a new message to history.
        /// </summary>
        public bool AddMessage(string message)
        {
            if (message.Length < _discardLowerThanChar) return false;

            var processedMessage = new SentimentMessage(message);
            processedMessage.Process(_processor);

            _history.AddMessage(processedMessage);

            return true;
        }

        /// <summary>
        /// Return a clone of the last add message.
        /// </summary>
        public SentimentMessage GetSentimentLast()
        {
            return _history.IsEmpty()
                ? (new SentimentMessage("")).Process(_processor)
                : _history.GetLast()
            ;
        }

        /// <summary>
        /// Return TextSentiment representation of the last add message.
        /// </summary>
        public TextSentiment GetSentimentLastLabel()
        {
            return _history.IsEmpty()
                ? TextSentiment.Neutral
                : GetSentimentLast().GetSentimentLabel();
        }

        /// <summary>
        /// Return a Averaged SentimentScore calculated against Current History.
        /// </summary>
        public SentimentScore GetSentimentAverage()
        {
            if (_history.IsEmpty()) return new SentimentScore();
            
            var historyItems = _history.GetAll();

            return new SentimentScore
            {
                Positive = historyItems.Average(message => message.GetScore().Positive),
                Negative = historyItems.Average(message => message.GetScore().Negative),
                Neutral = historyItems.Average(message => message.GetScore().Neutral)
            };
        }

        /// <summary>
        /// Return TextSentiment representation of averaged SentimentScore calculated against Current History.
        /// </summary>
        public TextSentiment GetSentimentAverageLabel()
        {
            return SentimentMessage.GetRankedLabel(GetSentimentAverage());
        }
        
        /// <summary>
        /// Return an Index in range -1 and -1 representing the sentiment of averaged
        /// SentimentScore calculated against Current History.
        /// </summary>
        public double GetSentimentAbsolute()
        {
            var sentimentAverage = GetSentimentAverage();

            var normalizedPositive = sentimentAverage.Positive + sentimentAverage.Neutral / 2;
            var normalizedNegative = -sentimentAverage.Negative - sentimentAverage.Neutral / 2;

            return normalizedPositive + normalizedNegative;
        }

        /// <summary>
        /// Return a Label from the TextSentimentAbsolute List representing the sentiment of averaged
        /// SentimentScore calculated against Current History.
        /// </summary>
        public string GetSentimentAbsoluteLabel()
        {
            var absolute = GetSentimentAbsolute();

            if (absolute.Equals(0.0)) return TextSentimentAbsolute[TextSentimentAbsolute.Count / 2];

            var indexMax = TextSentimentAbsolute.Count - 1.0;
            var factor = indexMax / 2.0;
            var index = indexMax - factor * (absolute + 1);

            return TextSentimentAbsolute[(int) Math.Floor(index)];
        }
    }
}