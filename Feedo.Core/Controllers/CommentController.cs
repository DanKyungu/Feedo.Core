using Feedo.Application.Services.Interfaces;
using Feedo.Core.Models;
using Feedo.Core.Services;
using Feedo.Persistance.Context;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Feedo.Core.Controllers
{
    public class CommentController : Controller
    {
        private readonly ISentimentAnalyzer _sentimentAnalyzer;
        private readonly ISocialNetwork _socialNetwork;
        private readonly ICommentService _twitterCommentService;

        private readonly ILogger<CommentController> _logger;
        private FeedoContext _feedoContext;

        public CommentController(ILogger<CommentController> logger,FeedoContext feedoContext, ISentimentAnalyzer sentimentAnalyzer,
            ISocialNetwork socialNetwork)
        {
            _logger = logger;
            _sentimentAnalyzer = sentimentAnalyzer;
            _socialNetwork = socialNetwork;
            _feedoContext = feedoContext;
            _twitterCommentService = new TwitterCommentService(_sentimentAnalyzer, _socialNetwork);
        }

        public IActionResult GetComments()
        {
            var comments = _feedoContext.Comments.ToList();
            return Json(comments);
        }

        public async Task<IActionResult?> Index()
        {
            var analyzedComments = await _twitterCommentService.GetComments();
            return View(analyzedComments);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}