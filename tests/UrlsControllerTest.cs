using hey_url_challenge_code_dotnet.Repositories.Interfaces;
using HeyUrlChallengeCodeDotnet.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Shyjus.BrowserDetection;
using System.Threading.Tasks;

namespace tests
{
    public class UrlsControllerTest
    {
        private Mock<ILogger<UrlsController>> _logger;
        private Mock<IBrowserDetector> browserDetector;
        private Mock<IUrlRepository> urlRepository;

        private UrlsController _controller;

        [SetUp]
        public void Setup()
        {
            _logger = new();
            browserDetector = new();
            urlRepository = new();
            _controller = new UrlsController(_logger.Object, browserDetector.Object, urlRepository.Object);
        }

        [Test]
        public async Task Visit_ShouldReturnNotFound()
        {
            var result = await _controller.Visit(It.IsAny<string>());

            var viewName = (result as ViewResult).ViewName;

            Assert.That(viewName, Is.EqualTo("404"));

        }

        [Test]
        public async Task Show_ShouldReturnNotFound()
        {
            var result = await _controller.Show(It.IsAny<string>());

            var viewName = (result as ViewResult).ViewName;

            Assert.That(viewName, Is.EqualTo("404"));

        }
    }
}