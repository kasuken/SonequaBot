using System;
using System.Collections.Generic;
using System.Text;

using Azure;
using System.Globalization;
using Azure.AI.TextAnalytics;
using SonequaBot.Models;

namespace SonequaBot.Services
{
    public class SentimentAnalysisService
    {
        private static readonly AzureKeyCredential Credentials = new AzureKeyCredential("693c2c2d8c5b4e02b26bc0e8db644700");
        private static readonly Uri Endpoint = new Uri("https://sonequabot.cognitiveservices.azure.com/");

        public SentimentScores ElaborateSentence(string sentence)
        {
            var client = new TextAnalyticsClient(Endpoint, Credentials);

            DocumentSentiment documentSentiment = client.AnalyzeSentiment(sentence, "it");
            Console.WriteLine($"Sentence sentiment: {documentSentiment.Sentiment}\n");

            var score = new SentimentScores();
            score.SetSentiment((SentimentScores.TextSentiment)documentSentiment.Sentiment);
            score.Positive = documentSentiment.ConfidenceScores.Positive;
            score.Negative = documentSentiment.ConfidenceScores.Negative;
            score.Neutral = documentSentiment.ConfidenceScores.Neutral;

            return score;
        }
    }
}
