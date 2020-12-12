using System;
using Bogus.DataSets;
using SonequaBot.Sentiment.Models;
using Xunit;

namespace SonequaBot.Tests.Sentiment
{
    public class SentimentTestFunctional : SentimentTestBase
    {
        [Fact]
        public void TestSentimentAbsoluteHistoryEmpty()
        {
            var sentiment = getSentimentFixed(1, 10, 10);

            // history is empty return 0
            Assert.Equal(0, sentiment.GetSentimentAbsolute());
        }

        [Fact]
        public void TestSentimentAddMessage()
        {
            var sentiment = getSentimentFixed(1, 10, 10);

            sentiment.AddMessage(new Lorem("it").Sentence(3));

            Assert.Equal(1, GetReflectionHistoryLength(sentiment));
        }

        [Fact]
        public void TestSentimentAddMessageGetMessage()
        {
            var sentiment = getSentimentFixed(1, 10, 10);
            var message = new Lorem("it").Sentence(3);

            sentiment.AddMessage(message);

            Assert.Equal(message, GetReflectionHistory(sentiment).GetLast().GetMessage());
            Assert.Equal(1, GetReflectionHistory(sentiment).GetLast().GetScore().Positive);
            Assert.Equal(1, GetReflectionHistory(sentiment).GetLast().GetScore().Negative);
            Assert.Equal(1, GetReflectionHistory(sentiment).GetLast().GetScore().Neutral);

            Assert.Equal("Neutral", GetReflectionHistory(sentiment).GetLast().GetSentimentLabel().ToString());
        }

        [Fact]
        public void TestSentimentWhenEmpty()
        {
            var sentiment = getSentimentFixed(0, 10, 10);

            Assert.Equal(0, sentiment.GetSentimentLast().GetScore().Positive);
            Assert.Equal(0, sentiment.GetSentimentLast().GetScore().Negative);
            Assert.Equal(0, sentiment.GetSentimentLast().GetScore().Neutral);
            
            Assert.Equal(SonequaBot.Sentiment.Sentiment.TextSentiment.Neutral, sentiment.GetSentimentLastLabel());
        }
        
        [Fact]
        public void TestSentimentExceptionCallProcessTwice()
        {
            var sentiment = getSentimentFixed(1, 10, 10);

            sentiment.AddMessage(new Lorem("it").Sentence(3));

            Assert.Throws<Exception>(() =>
            {
                GetReflectionHistory(sentiment).GetLast().Process(new MockSentimentProcessorFixed());
            });
        }

        [Fact]
        public void TestSentimentExceptionGetScoreOrLabelOnNotProcessed()
        {
            var message = new SentimentMessage(new Lorem("it").Sentence(3));

            Assert.Throws<Exception>(() => { message.GetScore(); });
            Assert.Throws<Exception>(() => { message.GetSentimentLabel(); });
        }

        [Fact]
        public void TestSentimentHistoryLength()
        {
            var sentiment = getSentimentFixed(1, 10, 10);

            for (var x = 1; x <= 10; x++)
            {
                sentiment.AddMessage(new Lorem("it").Sentence(3));
                Assert.Equal(x, GetReflectionHistoryLength(sentiment));
            }
        }
        
        [Fact]
        public void TestSentimentHistoryLengthFixed()
        {
            var sentiment = getSentimentFixed(1, 10, 10);

            for (var x = 1; x <= 10; x++)
            {
                sentiment.AddMessage(new Lorem("it").Sentence(3));
                Assert.Equal(x, GetReflectionHistoryLength(sentiment));
            }

            // extra message 
            sentiment.AddMessage(new Lorem("it").Sentence(3));
            
            // check if hit the limit
            Assert.Equal(10, GetReflectionHistoryLength(sentiment));
        }

        [Fact]
        public void TestSentimentIgnoreShortMessage()
        {
            var sentiment = getSentimentFixed(1, 10, 10);

            sentiment.AddMessage(new Lorem("it").Sentence(3));
            Assert.Equal(1, GetReflectionHistoryLength(sentiment));

            // add short message to be ignored
            sentiment.AddMessage("hello");

            // check if hit the limit
            Assert.Equal(1, GetReflectionHistoryLength(sentiment));
        }

        [Fact]
        public void TestSentimentProcessing()
        {
            var sentiment = getSentimentFixed(1, 10, 10);

            sentiment.AddMessage(new Lorem("it").Sentence(3));

            Assert.Equal(1, sentiment.GetSentimentLast().GetScore().Positive);
            Assert.Equal(1, sentiment.GetSentimentLast().GetScore().Negative);
            Assert.Equal(1, sentiment.GetSentimentLast().GetScore().Neutral);
        }
        
        [Fact]
        public void TestSentimentLastLabel()
        {
            var sentiment = getSentimentFixed(1, 10, 10);

            sentiment.AddMessage(new Lorem("it").Sentence(3));

            Assert.Equal("Neutral", sentiment.GetSentimentLastLabel().ToString());
        }
    }
}