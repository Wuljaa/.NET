using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Mvc.Models;
using TuristickaAgencija.Mvc.Services;

namespace TuristickaAgencija.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITrenutniKorisnikService _trenutniKorisnikService;

        public HomeController(
            ILogger<HomeController> logger,
            ITrenutniKorisnikService trenutniKorisnikService)
        {
            _logger = logger;
            _trenutniKorisnikService = trenutniKorisnikService;
        }

        public IActionResult Index()
        {
            ViewData["KorisnikEmail"] = _trenutniKorisnikService.GetEmail();
            ViewData["JePrijavljen"] = _trenutniKorisnikService.IsLoggedIn();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
}