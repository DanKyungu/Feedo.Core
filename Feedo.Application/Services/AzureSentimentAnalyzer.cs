using Azure;
using Azure.AI.TextAnalytics;
using Feedo.Application.Services.Interfaces;
using Feedo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedo.Application.Services
{
    public class AzureSentimentAnalyzer : ISentimentAnalyzer
    {
        private readonly Uri _endpoint;
        private readonly AzureKeyCredential _credentials;
        private readonly TextAnalyticsClient _textAnalyticsClient;

        public AzureSentimentAnalyzer(string key, string endpoint)
        {
            _credentials = new AzureKeyCredential(key);
            _endpoint = new Uri(endpoint);

            _textAnalyticsClient = new TextAnalyticsClient(_endpoint, _credentials);

        }

        public async Task<IEnumerable<SentimentResult>> GetSentimentScore(Dictionary<string,string> content)
        {
            var sentimentResults = new List<SentimentResult>();

            AnalyzeSentimentResultCollection reviews = await _textAnalyticsClient.AnalyzeSentimentBatchAsync(content.Values, options: new AnalyzeSentimentOptions()
            {
                IncludeOpinionMining = true
            });

            foreach (AnalyzeSentimentResult review in reviews)
            {
                var scores = review.DocumentSentiment.ConfidenceScores;

                sentimentResults.Add(new SentimentResult()
                {
                    Sentiment = review.DocumentSentiment.Sentiment.ToString(),
                    PositiveScore = scores.Positive,
                    NegativeScore = scores.Negative,
                    NeutralScore = scores.Neutral,
                    Score = (scores.Positive + scores.Negative + scores.Neutral) / 3
                });
            }

            return sentimentResults;
        }
    }
}
