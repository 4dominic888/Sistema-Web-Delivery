using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers
{
    public class PedidoController : Controller
    {
        public IActionResult CrearPedido()
        {
            return View();
        }

        public IActionResult TablaPedido()
        {
            return View();
        }
    }
}
