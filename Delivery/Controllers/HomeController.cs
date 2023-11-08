using Delivery.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


using Microsoft.AspNetCore.Authorization;

namespace Delivery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if(User.IsInRole("Cliente")) 
                    return RedirectToAction("IndexCliente", "Home");

                if (User.IsInRole("Repartidor"))
                    return RedirectToAction("IndexRepartidor", "Home");

                if (User.IsInRole("Administrador"))
                    return RedirectToAction("IndexAdministrador", "Home");
            }
            return View();
        }

        [Authorize(Roles = "Cliente")]
        public IActionResult IndexCliente()
        {
            return View();
        }

        [Authorize(Roles = "Repartidor")]
        public IActionResult IndexRepartidor()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult IndexAdministrador()
        {
            return View();
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