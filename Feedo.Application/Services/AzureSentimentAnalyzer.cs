using Azure;
using Feedo.Domain.Models;
using Azure.AI.TextAnalytics;
using Feedo.Application.Services.Interfaces;

namespace Feedo.Application.Services
{
    public class AzureSentimentAnalyzer : ISentimentAnalyzer
    {
        private readonly Uri _endpoint;
        private readonly AzureKeyCredential _credentials;
        private readonly TextAnalyticsClient _textAnalyticsClient;

        public AzureSentimentAnalyzer()
        {
            _credentials = new AzureKeyCredential("0ecccca5761741fd9297dbe6f90b6ce8");
            _endpoint = new Uri("https://feedotextanalytics.cognitiveservices.azure.com/");

            _textAnalyticsClient = new TextAnalyticsClient(_endpoint, _credentials);

        }

        public async Task<IEnumerable<SentimentResult>> GetSentimentScore(Dictionary<string,string> content)
        {
            var sentimentResults = new List<SentimentResult>();

            AnalyzeSentimentResultCollection reviews = await _textAnalyticsClient.AnalyzeSentimentBatchAsync(content.Keys, options: new AnalyzeSentimentOptions()
            {
                IncludeOpinionMining = true
            });

            foreach (AnalyzeSentimentResult review in reviews)
            {
                var scores = review.DocumentSentiment.ConfidenceScores;

                var finalScore = 0.0;

                if (review.DocumentSentiment.Sentiment.ToString() == "Positive")
                    finalScore = scores.Positive;

                if (review.DocumentSentiment.Sentiment.ToString() == "Negative")
                    finalScore = scores.Negative;

                if (review.DocumentSentiment.Sentiment.ToString() == "Neutral")
                    finalScore = scores.Neutral;

                sentimentResults.Add(new SentimentResult()
                {
                    Sentiment = review.DocumentSentiment.Sentiment.ToString(),
                    PositiveScore = scores.Positive,
                    NegativeScore = scores.Negative,
                    NeutralScore = scores.Neutral,
                    Score = finalScore
                });
            }

            return sentimentResults;
        }
    }
}
