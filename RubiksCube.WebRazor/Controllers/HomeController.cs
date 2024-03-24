using Microsoft.AspNetCore.Mvc;
using RubiksCube.Core.Interface;
using RubiksCube.WebRazor.Models;
using RubiksCube.WebRazor.Services;
using System.Diagnostics;

namespace RubiksCube.WebRazor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICubeFactory _cubeFactory;
        private readonly CommandService _commandService;
        public HomeController(ILogger<HomeController> logger, ICubeFactory cubeFactory, CommandService commandService)
        {
            _logger = logger;
            _cubeFactory = cubeFactory;
            _commandService = commandService;
        }

        [Route("/")]
        [HttpGet("{rotation?}")]
        public async Task<IActionResult> Index(string? rotation)
        {
            try
            {
                var cube = _cubeFactory.InstantiateCube();
                if (rotation != null)
                {
                    var result = await _commandService.ExecuteCommandsAsync(rotation);
                    return View(result);
                }
                var model = new CommandCubeResult(cube, string.Empty, false, null);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "home errror");
                var model = new CommandCubeResult(null, string.Empty, false, "Unexpected error");
                return View(model);
            }
        }

        //[Route("/privacy")]
        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
