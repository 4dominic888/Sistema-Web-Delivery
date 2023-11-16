using Delivery.Domain.Food;
using Delivery.Domain.Order;
using Delivery.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Delivery.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IComidaRepository _comidaRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public PedidoController(IComidaRepository comidaRepository, 
            IPedidoRepository pedidoRepository,
            IUsuarioRepository usuarioRepository)
        {
            _comidaRepository = comidaRepository;
            _pedidoRepository = pedidoRepository;
            _usuarioRepository = usuarioRepository;

        }

        [Authorize(Roles = "Cliente")]
        public IActionResult CrearPedido()
        {
            //Almacenar temporalmente la información de lo que se pidio
            var lista = TempData["lista"];
            TempData["lista_post"] = TempData["lista"];
            TempData["idCliente_post"] = TempData["idCliente"];
            TempData["PrecioTotal_post"] = TempData["PrecioTotal"];

            //En caso de que se refresque la pagina o se acceda a esta vista sin info del pedido
            if (lista == null) return RedirectToAction("VerMenu", "Comida");
            return View();
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> CrearPedido(
            [Bind("Direccion, MetodoPago")] Pedido pedido
            )
        {
            //Recuperar y restaurar data del pedido
            var listaStr = TempData["lista_post"];
            var idCliente = TempData["idCliente_post"];

            float precio = float.Parse(TempData["PrecioTotal_post"].ToString().Replace('.', ','));
            List<Comida_CaracteristicaPedido> lista = _comidaRepository.DeserealizarJSONPedidoCliente(listaStr.ToString(), int.Parse(idCliente.ToString()));

            

            //Guardar data
            Direccion address = pedido.Direccion;
            MetodoPago method_pay = pedido.MetodoPago;

            await _pedidoRepository.Registrar_Direccion(address);
            MetodoPago mp_aux = await _pedidoRepository.Buscar_MetodoPago_NumeroTarjeta(method_pay.Numero);
            //Evitar que se guarde un método de pago siendo este uno de efectivo
            if (method_pay.Tipo != TipoMetodoPago.Efectivo)
            {
                if (mp_aux is not null)
                {
                    mp_aux.NombreTarjeta = method_pay.NombreTarjeta;
                    mp_aux.CVV = method_pay.CVV;
                    mp_aux.fechaExpiracion = method_pay.fechaExpiracion;
                    await _pedidoRepository.ActualizarMetodoPago(mp_aux);
                }
                else await _pedidoRepository.Registrar_MetodoPago(method_pay);
            }
            
            pedido.IdCliente = lista.First().IdCliente;
            pedido.Fecha_Inicio = DateTime.Now;
            pedido.Estado = EstadoPedido.En_Proceso;
            pedido.IdDireccion = address.Id;

            //Cambiar valor a null
            if (method_pay.Tipo == TipoMetodoPago.Efectivo)
            {
                pedido.IdMetodoPago = null;
                pedido.MetodoPago = null;
            }
            else
            {
                if (mp_aux is null)
                {
                    pedido.IdMetodoPago = method_pay.Id;
                }
                else
                {
                    pedido.IdMetodoPago= mp_aux.Id;
                    pedido.MetodoPago= mp_aux;
                }
            }


            pedido.Total = precio;

            await _pedidoRepository.Agregar(pedido);
            await _pedidoRepository.Guardar();

            foreach (var item in lista)
            {
                if (item.IdCaracteristicaComida == 0) item.IdCaracteristicaComida = null;
                item.IdPedido = pedido.Codigo;

                item.Comida = await _comidaRepository.ObtenerPorId(item.IdComida);
                if(item.Comida.Stock > 0)
                {
                    item.Comida.Stock--;
                }
                await _comidaRepository.EditarComida(item.Comida);
            }
            await _comidaRepository.Registrar_Comidas_Pedido(lista);

            return RedirectToAction("TablaPedido");
        }

        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> TablaPedido()
        {
            //Inicia vacio
            IEnumerable<Pedido> lista_pedidos = Enumerable.Empty<Pedido>();

            int ID_Cliente = int.Parse(User.FindFirst("ID").Value);
            lista_pedidos = await _pedidoRepository.ObtenerTodos(x => x.IdCliente == ID_Cliente);

            foreach (var item in lista_pedidos)
            {
                item.Repartidor = await _usuarioRepository.BuscarRepartidorID(item.IdRepartidor.GetValueOrDefault());
                item.Cliente = await _usuarioRepository.BuscarClienteID(item.IdCliente);
                item.Direccion = await _pedidoRepository.BuscarDirreccionId(item.IdDireccion);
                item.MetodoPago = await _pedidoRepository.BuscarMetodoPagoId(item.IdMetodoPago.GetValueOrDefault());
            }



            return View(lista_pedidos);
        }


        [Authorize(Roles = "Administrador, Repartidor")]
        public async Task<IActionResult> TablaPedidoTotal()
        {
            //Inicia vacio
            IEnumerable<Pedido> lista_pedidos = Enumerable.Empty<Pedido>();

            lista_pedidos = await _pedidoRepository.ObtenerTodos();

            foreach (var item in lista_pedidos)
            {
                item.Repartidor = await _usuarioRepository.BuscarRepartidorID(item.IdRepartidor.GetValueOrDefault());
                item.Cliente = await _usuarioRepository.BuscarClienteID(item.IdCliente);
                item.Direccion = await _pedidoRepository.BuscarDirreccionId(item.IdDireccion);
                item.MetodoPago = await _pedidoRepository.BuscarMetodoPagoId(item.IdMetodoPago.GetValueOrDefault());
            }

            return View("TablaPedido", lista_pedidos);
        }
        public async Task<IActionResult> _DetallePedido(int? id)
        {
            var lista_pedidos_comida = new List<Comida_CaracteristicaPedido>();

            if (id != null)
            {
                var pedido = await _pedidoRepository.ObtenerPorId(id.GetValueOrDefault());
                pedido.Cliente = await _usuarioRepository.BuscarClienteID(pedido.IdCliente);
                pedido.Direccion = await _pedidoRepository.BuscarDirreccionId(pedido.IdDireccion);

                ViewBag.pedido = pedido;

                if (pedido.IdMetodoPago is not null)
                    pedido.MetodoPago = await _pedidoRepository.BuscarMetodoPagoId(pedido.IdMetodoPago.GetValueOrDefault());

                lista_pedidos_comida = await _pedidoRepository.BuscarComidasPedido(id.GetValueOrDefault());
                foreach (var item in lista_pedidos_comida)
                {
                    item.Comida = await _comidaRepository.ObtenerPorId(item.IdComida);
                    item.caracteristicaComida = await _comidaRepository.ObtenerCaracteristicaPorID(item.IdCaracteristicaComida.GetValueOrDefault());
                }
            }
            return PartialView(lista_pedidos_comida);
        }
    
    }
}

