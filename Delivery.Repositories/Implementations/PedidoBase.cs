using Delivery.Domain.Food;
using Delivery.Domain.Order;
using Delivery.Persistence.Data;
using Delivery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Repositories.Implementations
{
    public class PedidoBase : RepositoryBase<Pedido>, IPedidoRepository
    {
        private readonly DeliveryDBContext _context;
        public PedidoBase(DeliveryDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Comida_CaracteristicaPedido>> BuscarComidasPedido(int id)
        {
            return await _context.Comida_CaracteristicasPedido.Where(c => c.IdPedido == id).ToListAsync();
        }

        public async Task<Direccion> BuscarDirreccionId(int id)
        {
            return await _context.Direcciones.FindAsync(id);
        }

        public async Task<MetodoPago> BuscarMetodoPagoId(int? metodoPagoId)
        {
            return await _context.MetodoPagos.FindAsync(metodoPagoId);
        }

        public async Task Registrar_Direccion(Direccion direccion)
        {
            _context.Direcciones.Add(direccion);
            await Guardar();
        }

        public async Task Registrar_MetodoPago(MetodoPago metodoPago)
        {
            _context.MetodoPagos.Add(metodoPago);
            await Guardar();
        }

        public async Task Registrar_Pedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await Guardar();
        }
    }
}
