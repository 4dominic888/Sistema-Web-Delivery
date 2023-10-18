using Microsoft.AspNetCore.Mvc;
using Delivery.Domain.User;

namespace Delivery.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(
            [Bind("Surname, Name, Phone, Sexo, Email, Password, DateBirth")] Cliente cliente)
        {
            Console.WriteLine(ModelState.IsValid);
            
            return View();
        }
    }
}
