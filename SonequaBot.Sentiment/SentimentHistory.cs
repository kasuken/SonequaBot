using System.Collections.Generic;
using System.Linq;
using SonequaBot.Sentiment.Models;

namespace SonequaBot.Sentiment
{
    public class SentimentHistory
    {
        private readonly int _historyLimit;
        private readonly List<SentimentMessage> _historyMessage = new List<SentimentMessage>();

        public SentimentHistory(int historyLimit)
        {
            _historyLimit = historyLimit;
        }

        public void AddMessage(SentimentMessage message)
        {
            _historyMessage.Add(message);
            if (_historyMessage.Count > _historyLimit) _historyMessage.RemoveAt(0);
        }

        public SentimentMessage GetLast()
        {
            return (SentimentMessage) _historyMessage.Last().Clone();
        }

        public List<SentimentMessage> GetAll()
        {
            return new List<SentimentMessage>(_historyMessage);
        }
    }
}