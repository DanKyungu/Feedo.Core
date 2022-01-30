using Feedo.Application.Services.Interfaces;

namespace Feedo.Core.Services
{
    public class TwitterCommentService : ICommentService
    {
        private readonly ISentimentAnalyzer _sentimentAnalyzer;
        private readonly ISocialNetwork _socialNetwork;

        public TwitterCommentService(ISentimentAnalyzer sentimentAnalyzer, ISocialNetwork socialNetwork)
        {
            _sentimentAnalyzer = sentimentAnalyzer;
            _socialNetwork = socialNetwork;
        }

        public async Task<List<Domain.Comment>> GetComments()
        {
            var comments = (await _socialNetwork.GetSocialCommentByKeywork("brasimba")).ToArray();
            var analyzedComments = new List<Domain.Comment>();

            Dictionary<string, string> data = new Dictionary<string, string>();

            //var twitter = _feedoContext.SocialNetworks.FirstOrDefault(x => x.Name == "Twitter");

            foreach (var item in comments)
            {
                //var getCurrentComment = _feedoContext.Comments.FirstOrDefault(x => x.OriginalComment.Contains(item.Comment));
                //if (getCurrentComment != null) continue;

                if (data.ContainsKey(item.Comment)) continue;
                data.Add(item.Comment, item.Id);
            }

            var analyzedContents = (await _sentimentAnalyzer.GetSentimentScore(data)).ToArray();

            if (analyzedContents == null) return null;

            for (int i = 0; i < analyzedContents.Count(); i++)
            {
                if (analyzedContents[i] != null)
                {
                    analyzedComments.Add(new Domain.Comment()
                    {
                        SocialCommentId = comments[i].Id,
                        OriginalComment = comments[i].Comment,
                        Sentiment = analyzedContents[i].Sentiment,
                        SocialNetworkId = 1,
                        UserFullName = comments[i].FullUsername,
                        Username = comments[i].Username,
                        SentimentRate = analyzedContents[i].Score
                    });

                }
            }

            return analyzedComments;
        }
        public async Task<double> GetCommentsAverage()
        {
            var analyzedComments = await GetComments();

            var average = Math.Round(analyzedComments.Select(x => x.SentimentRate).Average() * 100, 1);

            return average;
        }
    }
}
