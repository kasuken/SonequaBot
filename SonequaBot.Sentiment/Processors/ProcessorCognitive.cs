using System;
using Azure;
using Azure.AI.TextAnalytics;
using SonequaBot.Sentiment.Interfaces;
using SonequaBot.Sentiment.Models;

namespace SonequaBot.Sentiment.Processors
{
    public class ProcessorCognitive : IProcessor
    {
        private readonly AzureKeyCredential _credentials;
        private readonly Uri _endpoint;

        public ProcessorCognitive(AzureKeyCredential azureKey, Uri endPoint)
        {
            _credentials = azureKey;
            _endpoint = endPoint;
        }

        public SentimentScore Process(string sentence)
        {
            var client = new TextAnalyticsClient(_endpoint, _credentials);

            DocumentSentiment documentSentiment = client.AnalyzeSentiment(sentence, "it");

            return new SentimentScore
            {
                Positive = documentSentiment.ConfidenceScores.Positive,
                Negative = documentSentiment.ConfidenceScores.Negative,
                Neutral = documentSentiment.ConfidenceScores.Neutral
            };
        }
    }
}