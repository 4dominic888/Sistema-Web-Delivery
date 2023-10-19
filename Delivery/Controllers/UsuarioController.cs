using Delivery.Domain.User;
using Delivery.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Delivery.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Cliente"))
                    return RedirectToAction("IndexCliente", "Home");

                if (User.IsInRole("Repartidor"))
                    return RedirectToAction("IndexRepartidor", "Home");

                if (User.IsInRole("Administrador"))
                    return RedirectToAction("IndexAdministrador", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Cliente cliente)
        {
            string contraseña = _usuarioRepository.EncriptarSHA256(cliente.Password);
            Cliente _cliente = await _usuarioRepository.ValidarCliente(cliente.Email, contraseña);

            if(_cliente != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, _cliente.Name),
                    new Claim("Correo", _cliente.Email)
                };

                claims.Add(new Claim(ClaimTypes.Role, _cliente.Rol));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("IndexCliente", "Home");
            }
            else
            {
                ViewBag.UserNoValid = "El correo y la contraseña no coinciden, intentelo nuevamente";
                return View();
            }
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Cliente"))
                    return RedirectToAction("IndexCliente", "Home");

                if (User.IsInRole("Repartidor"))
                    return RedirectToAction("IndexRepartidor", "Home");

                if (User.IsInRole("Administrador"))
                    return RedirectToAction("IndexAdministrador", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(
            [Bind("Surname, Name, Phone, Sexo, Email, Password, DateBirth")] Cliente cliente)
        {

            bool modelovalido = true;

            if (await _usuarioRepository.EmailRepetido(cliente.Email))
            {
                modelovalido = false;
                ViewBag.EmailRepetido = "El correo ingresado ya existe";
            }
            if (await _usuarioRepository.PhoneRepetido(cliente.Phone))
            {
                modelovalido = false;
                ViewBag.PhoneRepetido = "El telefono ingresado ya fue registrado";
            }

            if (ModelState.IsValid && modelovalido)
            {
                cliente.Rol = "Cliente";
                cliente.Password = _usuarioRepository.EncriptarSHA256(cliente.Password); //Encriptar contraseña
                await _usuarioRepository.RegistrarCliente(cliente);
                return RedirectToAction("Login");

            }


            ViewBag.SurnameError = ModelState["Surname"].Errors.Count > 0;
            ViewBag.NameError = ModelState["Name"].Errors.Count > 0;
            ViewBag.PhoneError = ModelState["Phone"].Errors.Count > 0;
            ViewBag.EmailError = ModelState["Email"].Errors.Count > 0;
            ViewBag.PasswordError = ModelState["Password"].Errors.Count > 0;
            ViewBag.DataBirthError = ModelState["DateBirth"].Errors.Count > 0;

            return View();
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Usuario");
        }
    }
}
