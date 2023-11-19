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

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> TablaUsuarios(string tipo = "cliente")
        {
            ViewBag.usuarios = await _usuarioRepository.ObtenerTodos(u => u.Rol.ToLower() == tipo.ToLower());
            ViewBag.tipo = tipo;

            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult RegisterEmpleados(string tipo = "repartidor")
        {
            ViewBag.tipo_e = tipo;
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> RegisterEmpleados(
             [Bind("Surname, Name, Phone, Sexo, Email, Password, DateBirth")] Usuario usuario, string tipo)
        {
            bool modelovalido = true;

            if (await _usuarioRepository.EmailRepetido(usuario.Email))
            {
                modelovalido = false;
                ViewBag.EmailRepetido = "El correo ingresado ya existe";
            }
            if (await _usuarioRepository.PhoneRepetido(usuario.Phone))
            {
                modelovalido = false;
                ViewBag.PhoneRepetido = "El telefono ingresado ya fue registrado";
            }

            if (ModelState.IsValid && modelovalido)
            {
                usuario.Rol = tipo;
                usuario.Password = _usuarioRepository.EncriptarSHA256(usuario.Password); //Encriptar contraseña

                await _usuarioRepository.RegistrarUsuario(usuario);
                return RedirectToAction("TablaUsuarios", "Usuario");

            }


            ViewBag.SurnameError = ModelState["Surname"].Errors.Count > 0;
            ViewBag.NameError = ModelState["Name"].Errors.Count > 0;
            ViewBag.PhoneError = ModelState["Phone"].Errors.Count > 0;
            ViewBag.EmailError = ModelState["Email"].Errors.Count > 0;
            ViewBag.PasswordError = ModelState["Password"].Errors.Count > 0;
            ViewBag.DataBirthError = ModelState["DateBirth"].Errors.Count > 0;

            /*
            foreach (var estado in ModelState.Values)
            {
                foreach (var error in estado.Errors)
                {
                    Console.WriteLine($"Campo: {estado} - Error: {error.ErrorMessage}");
                }
            }
            */

            return View();
        }

        [Authorize]
        public async Task<IActionResult> EditarDatos()
        {
            int idUser = int.Parse(User.FindFirstValue("ID"));
            Usuario usuario = await _usuarioRepository.BuscarUsuario(idUser);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult EditarDatosCliente(
            [Bind("Id, Surname, Name, Phone, Email, Sexo, DateBirth, ContenidoDestacado, Recomendaciones")] Usuario usuario)
        {
            Console.WriteLine(ModelState.IsValid);

            ViewBag.EmailError = ModelState["Email"].Errors.Count > 0;
            ViewBag.SurnameError = ModelState["Cliente.Surname"].Errors.Count > 0;
            ViewBag.NameError = ModelState["Cliente.Name"].Errors.Count > 0;
            ViewBag.PhoneError = ModelState["Cliente.Phone"].Errors.Count > 0;

            return View();
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

                if (User.IsInRole("Chef"))
                    return RedirectToAction("IndexChef", "Home");

                if (User.IsInRole("Administrador"))
                    return RedirectToAction("IndexAdministrador", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            string contraseña = _usuarioRepository.EncriptarSHA256(usuario.Password);
            var _usuario = await _usuarioRepository.ValidarUsuario(usuario.Email, contraseña);

            if(_usuario != null)
            {
                var claims = new List<Claim> {
                    new Claim("Nombre", _usuario.Name),
                    new Claim("Correo", _usuario.Email),
                    new Claim("ID", _usuario.Id.ToString()),
                    new Claim("Destacado", _usuario.ContenidoDestacado.ToString()),
                    new Claim("Apellido", _usuario.Surname),
                    new Claim("Telefono", _usuario.Phone)

                };

                claims.Add(new Claim(ClaimTypes.Role, _usuario.Rol));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                //variable
                if(_usuario.Rol == "Cliente") return RedirectToAction("IndexCliente", "Home");
                if (_usuario.Rol == "Repartidor") return RedirectToAction("IndexRepartidor", "Home");
                if (_usuario.Rol == "Chef") return RedirectToAction("IndexChef", "Home");
                if (_usuario.Rol == "Administrador") return RedirectToAction("IndexAdministrador", "Home");
                return RedirectToAction("Index", "Home");
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

                if (User.IsInRole("Chef"))
                    return RedirectToAction("IndexChef", "Home");

                if (User.IsInRole("Administrador"))
                    return RedirectToAction("IndexAdministrador", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(
            [Bind("Surname, Name, Phone, Sexo, Email, Password, DateBirth")] Usuario cliente)
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
                await _usuarioRepository.RegistrarUsuario(cliente);
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
