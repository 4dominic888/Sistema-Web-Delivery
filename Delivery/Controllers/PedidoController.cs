using Delivery.Domain.Food;
using Delivery.Domain.User;
using Delivery.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IComidaRepository _comidaRepository;
        public PedidoController(IComidaRepository comidaRepository)
        {
            this._comidaRepository = comidaRepository;
        }

        [Authorize(Roles = "Cliente")]
        public IActionResult CrearPedido()
        {
            var lista = TempData["lista"];
            var idCliente = TempData["idCliente"];
            if (lista == null) return RedirectToAction("VerMenu", "Comida");
            var listado = _comidaRepository.DeserealizarJSONPedidoCliente(lista.ToString(), int.Parse(idCliente.ToString()));
            return View();
        }

        [Authorize(Roles = "Repartidor, Administrador, Cliente")]
        public IActionResult TablaPedido()
        {
            return View();
        }
    }
}
