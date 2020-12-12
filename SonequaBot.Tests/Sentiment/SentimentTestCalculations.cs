using Bogus.DataSets;
using SonequaBot.Sentiment.Models;
using Xunit;

namespace SonequaBot.Tests.Sentiment
{
    public class SentimentTestCalculations : SentimentTestBase
    {
        // Strict theoretical case when all scores are equals and ranking of it is impossible
        [Fact]
        public void TestSentimentAbsoluteAllScoreEqualsBug()
        {
            var sentiment = getSentimentFixed(1, 10, 10);

            for (var x = 1; x <= 10; x++) sentiment.AddMessage(new Lorem("it").Sentence(3));

            Assert.Equal(0, sentiment.GetSentimentAbsolute());
        }

        [Fact]
        public void TestSentimentAbsoluteLabel()
        {
            var processor = new MockSentimentProcessorPredefinedScore
            {
                scores = new[]
                {
                    new SentimentScore {Positive = 0, Negative = 0, Neutral = 1},
                    new SentimentScore {Positive = 0, Negative = 0, Neutral = 1},
                    new SentimentScore {Positive = 0, Negative = 0, Neutral = 1},
                    new SentimentScore {Positive = 0, Negative = 1, Neutral = 0},
                    new SentimentScore {Positive = 1, Negative = 0, Neutral = 0}
                }
            };

            var sentiment = new SonequaBot.Sentiment.Sentiment(processor);

            for (var x = 0; x < 5; x++) sentiment.AddMessage(new Lorem("it").Sentence(3));

            Assert.Equal("Normal", sentiment.GetSentimentAbsoluteLabel());
        }

        [Fact]
        public void TestSentimentAbsoluteLabelAllScoreEqualsBug()
        {
            var sentiment = getSentimentFixed(1, 10, 10);

            for (var x = 1; x <= 10; x++) sentiment.AddMessage(new Lorem("it").Sentence(3));

            Assert.Equal(sentiment.TextSentimentAbsolute[sentiment.TextSentimentAbsolute.Count / 2],
                sentiment.GetSentimentAbsoluteLabel());
        }

        [Fact]
        public void TestSentimentAbsoluteLabelMaxNegative()
        {
            var processor = new MockSentimentProcessorPredefinedScore
            {
                scores = new[]
                {
                    new SentimentScore {Positive = 0, Negative = 1, Neutral = 0},
                    new SentimentScore {Positive = 0, Negative = 1, Neutral = 0},
                    new SentimentScore {Positive = 0, Negative = 1, Neutral = 0},
                    new SentimentScore {Positive = 0, Negative = 1, Neutral = 0},
                    new SentimentScore {Positive = 0, Negative = 1, Neutral = 0}
                }
            };

            var sentiment = new SonequaBot.Sentiment.Sentiment(processor);

            for (var x = 0; x < 5; x++) sentiment.AddMessage(new Lorem("it").Sentence(3));

            Assert.Equal("Abysmal", sentiment.GetSentimentAbsoluteLabel());
        }

        [Fact]
        public void TestSentimentAbsoluteLabelMaxPositive()
        {
            var processor = new MockSentimentProcessorPredefinedScore
            {
                scores = new[]
                {
                    new SentimentScore {Positive = 1, Negative = 0, Neutral = 0},
                    new SentimentScore {Positive = 1, Negative = 0, Neutral = 0},
                    new SentimentScore {Positive = 1, Negative = 0, Neutral = 0},
                    new SentimentScore {Positive = 1, Negative = 0, Neutral = 0},
                    new SentimentScore {Positive = 1, Negative = 0, Neutral = 0}
                }
            };

            var sentiment = new SonequaBot.Sentiment.Sentiment(processor);

            for (var x = 0; x < 5; x++) sentiment.AddMessage(new Lorem("it").Sentence(3));

            Assert.Equal("Perfect", sentiment.GetSentimentAbsoluteLabel());
        }

        [Fact]
        public void TestSentimentAverage()
        {
            var sentiment = getSentimentFixed(1, 10, 10);

            for (var x = 1; x <= 10; x++) sentiment.AddMessage(new Lorem("it").Sentence(3));

            Assert.Equal(1, sentiment.GetSentimentAverage().Positive);
            Assert.Equal(1, sentiment.GetSentimentAverage().Negative);
            Assert.Equal(1, sentiment.GetSentimentAverage().Neutral);
        }

        [Fact]
        public void TestSentimentAverageLabel()
        {
            var sentiment = getSentimentFixed(1, 10, 10);

            for (var x = 1; x <= 10; x++) sentiment.AddMessage(new Lorem("it").Sentence(3));

            Assert.Equal("Neutral", sentiment.GetSentimentAverageLabel().ToString());
        }

        [Fact]
        public void TestSentimentAverageValue()
        {
            var processor = new MockSentimentProcessorPredefinedScore
            {
                scores = new[]
                {
                    new SentimentScore
                    {
                        Positive = 1,
                        Negative = 0,
                        Neutral = 0
                    },
                    new SentimentScore
                    {
                        Positive = 0,
                        Negative = 0,
                        Neutral = 0
                    },
                    new SentimentScore
                    {
                        Positive = 0,
                        Negative = 0,
                        Neutral = 0
                    },
                    new SentimentScore
                    {
                        Positive = 1,
                        Negative = 0,
                        Neutral = 0
                    }
                }
            };

            var sentiment = new SonequaBot.Sentiment.Sentiment(processor);

            for (var x = 0; x < 4; x++) sentiment.AddMessage(new Lorem("it").Sentence(3));

            Assert.Equal(.5, sentiment.GetSentimentAverage().Positive);
            Assert.Equal(0, sentiment.GetSentimentAverage().Negative);
            Assert.Equal(0, sentiment.GetSentimentAverage().Neutral);
        }

        [Fact]
        public void TestSentimentLast()
        {
            var sentiment = getSentimentFixed(1, 10, 10);

            sentiment.AddMessage(new Lorem("it").Sentence(3));

            Assert.Equal(1, sentiment.GetSentimentLast().GetScore().Positive);
            Assert.Equal(1, sentiment.GetSentimentLast().GetScore().Negative);
            Assert.Equal(1, sentiment.GetSentimentLast().GetScore().Neutral);
        }
    }
}