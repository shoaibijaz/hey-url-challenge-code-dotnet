using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.Repositories.Interfaces;
using hey_url_challenge_code_dotnet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;

namespace HeyUrlChallengeCodeDotnet.Controllers
{
    [Route("/")]
    public class UrlsController : Controller
    {
        private readonly ILogger<UrlsController> _logger;
        private static readonly Random getrandom = new Random();
        private readonly IBrowserDetector browserDetector;
        private readonly IUrlRepository urlRepository;

        public UrlsController(ILogger<UrlsController> logger, IBrowserDetector browserDetector, IUrlRepository urlRepository)
        {
            this.browserDetector = browserDetector;
            this.urlRepository = urlRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel();
            model.Urls =  await urlRepository.GetUrls();
            model.NewUrl = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(a => a.Errors).Select(a => a.ErrorMessage));
                TempData["Notice"] = errors;
            }
            else
            {
                await urlRepository.CreateUrl(viewModel.NewUrl);
                TempData["Notice"] = "Short Url has been created";
            }

           
            return RedirectToAction("Index");
        }

        [Route("/{url}")]
        public async Task<IActionResult> Visit(string url)
        {
            var urlObject = await urlRepository.FindByShortUrl(url);

            if(urlObject == null)
            {
                return View("404");
            }

            await urlRepository.AddClick(url, this.browserDetector.Browser.OS, this.browserDetector.Browser.Name);

            return Redirect(urlObject.OriginalURL);
        }


        [Route("urls/{url}")]
        public async Task<IActionResult> Show(string url)
        {
            var model = new ShowViewModel();
            model.Url = await urlRepository.FindByShortUrl(url);

            if(model.Url == null)
            {
                return View("404");
            }

            model.DailyClicks = model.Url.Clicks.GroupBy(a => a.CreatedOn.Day).ToDictionary(a=>a.Key.ToString(), a=>a.Count());
            model.BrowseClicks = model.Url.Clicks.GroupBy(a => a.Browser).ToDictionary(a=>a.Key.ToString(), a=>a.Count());
            model.PlatformClicks = model.Url.Clicks.GroupBy(a => a.OS).ToDictionary(a=>a.Key.ToString(), a=>a.Count());

            return View(model);
        }

        //[Route("urls/{url}")]
        //public IActionResult Show(string url) => View(new ShowViewModel
        //{
        //    Url = new Url {ShortUrl = url, Count = getrandom.Next(1, 10)},
        //    DailyClicks = new Dictionary<string, int>
        //    {
        //        {"1", 13},
        //        {"2", 2},
        //        {"3", 1},
        //        {"4", 7},
        //        {"5", 20},
        //        {"6", 18},
        //        {"7", 10},
        //        {"8", 20},
        //        {"9", 15},
        //        {"10", 5}
        //    },
        //    BrowseClicks = new Dictionary<string, int>
        //    {
        //        { "IE", 13 },
        //        { "Firefox", 22 },
        //        { "Chrome", 17 },
        //        { "Safari", 7 },
        //    },
        //    PlatformClicks = new Dictionary<string, int>
        //    {
        //        { "Windows", 13 },
        //        { "macOS", 22 },
        //        { "Ubuntu", 17 },
        //        { "Other", 7 },
        //    }
        //});
    }
}