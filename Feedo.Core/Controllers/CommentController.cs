using Feedo.Application.Services.Interfaces;
using Feedo.Core.Models;
using Feedo.Persistance.Context;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Feedo.Core.Controllers
{
    public class CommentController : Controller
    {
        private readonly ISentimentAnalyzer _sentimentAnalyzer;
        private readonly ISocialNetwork _socialNetwork;

        private readonly ILogger<CommentController> _logger;
        private FeedoContext _feedoContext;

        public CommentController(ILogger<CommentController> logger,FeedoContext feedoContext, ISentimentAnalyzer sentimentAnalyzer,
            ISocialNetwork socialNetwork)
        {
            _logger = logger;
            _sentimentAnalyzer = sentimentAnalyzer;
            _socialNetwork = socialNetwork;
            _feedoContext = feedoContext;
        }

        public IActionResult GetComments()
        {
            var comments = _feedoContext.Comments.ToList();
            return Json(comments);
        }

        public async Task<IActionResult?> SearchPersitingComments()
        {
            var comments = (await _socialNetwork.GetSocialCommentByKeywork("brasimba")).ToArray();
            Dictionary<string, string> data = new Dictionary<string, string>();

            var twitter = _feedoContext.SocialNetworks.FirstOrDefault(x => x.Name == "Twitter");

            foreach (var item in comments)
            {
                var getCurrentComment = _feedoContext.Comments.FirstOrDefault(x => x.SocialCommentId == item.Id);
                if (getCurrentComment != null) continue;

                if (data.ContainsKey(item.Comment)) continue;
                data.Add(item.Comment,item.Id);
            }

            var analyzedContents = (await _sentimentAnalyzer.GetSentimentScore(data)).ToArray();

            if (analyzedContents == null) return null;

            for (int i = 0; i < analyzedContents.Count(); i++)
            {
                if (analyzedContents[i] != null)
                {
                    _feedoContext.Comments.Add(new Domain.Comment()
                    {
                        SocialCommentId = comments[i].Id,
                        OriginalComment = comments[i].Comment,
                        Sentiment = analyzedContents[i].Sentiment,
                        SocialNetworkId = twitter.Id,
                        UserFullName = comments[i].FullUsername,
                        Username = comments[i].Username,
                        SentimentRate = analyzedContents[i].Score
                    });

                    _feedoContext.SaveChanges();
                }
            }

            var analyzedComments = _feedoContext.Comments.ToList();
            return Json(analyzedComments);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}