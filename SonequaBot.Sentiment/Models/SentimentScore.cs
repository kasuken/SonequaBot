using System;

namespace SonequaBot.Sentiment.Models
{
    public class SentimentScore : ICloneable
    {
        public double Positive { get; set; }
        public double Negative { get; set; }
        public double Neutral { get; set; }
        
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}