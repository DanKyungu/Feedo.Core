using Feedo.Application.Services.Interfaces;
using Feedo.Core.Models;
using Feedo.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Feedo.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISentimentAnalyzer _sentimentAnalyzer;
        private readonly ISocialNetwork _socialNetwork;
        private readonly ICommentService _twitterCommentService;

        public HomeController(ILogger<HomeController> logger, ISentimentAnalyzer sentimentAnalyzer,
            ISocialNetwork socialNetwork)
        {
            _logger = logger;
            _sentimentAnalyzer = sentimentAnalyzer;
            _socialNetwork = socialNetwork;
            _twitterCommentService = new TwitterCommentService(_sentimentAnalyzer, _socialNetwork);
        }

        public async Task<IActionResult> Index()
        {
            AverageComment averageComment = new AverageComment();
            averageComment.TwitterAverage = await _twitterCommentService.GetCommentsAverage();

            return View(averageComment);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}