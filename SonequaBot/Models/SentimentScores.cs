using System;
using System.Collections.Generic;
using System.Text;

namespace SonequaBot.Models
{
    public class SentimentScores
    {
        public enum TextSentiment
        {
            Positive = 0,
            Neutral = 1,
            Negative = 2,
            Mixed = 3
        }

        private TextSentiment textSentiment;

        public void SetSentiment(TextSentiment value)
        {
            textSentiment = value;
        }

        public TextSentiment GetSentiment()
        {
            return textSentiment;
        }

        public double Positive { get; set; }

        public double Negative { get; set; }

        public double Neutral { get; set; }
    }
}
