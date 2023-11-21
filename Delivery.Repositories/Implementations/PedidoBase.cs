using Delivery.Domain.Food;
using Delivery.Domain.Order;
using Delivery.Persistence.Data;
using Delivery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Delivery.Repositories.Implementations
{
    public class PedidoBase : RepositoryBase<Pedido>, IPedidoRepository
    {
        private new readonly DeliveryDBContext _context;
        public PedidoBase(DeliveryDBContext context) : base(context)
        {
            _context = context;
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

        public async Task<bool> Aceptar_Pedido(int IdRepartidor)
        {
            //Si hay un pedido ya aceptado por un repartidor, retornará false, indicando que no puede tomar el pedido
            var pedido = await _context.Pedidos.Where(p => p.Estado == EstadoPedido.Aceptado && p.IdRepartidor == IdRepartidor).FirstOrDefaultAsync();
            if (pedido is null) return true;
            else return false;

        }

        public Dictionary<int, ComidaLog> DeserealizarJSON(string JSON)
            => JsonConvert.DeserializeObject<Dictionary<int, ComidaLog>>(JSON);

        public async Task<Direccion?> Buscar_Direccion(Expression<Func<Direccion, bool>> filtro)
            => await _context.Direcciones.Where(filtro).FirstOrDefaultAsync();

        public async Task<MetodoPago?> Buscar_MetodoPago(Expression<Func<MetodoPago, bool>> filtro)
            => await _context.MetodoPagos.Where(filtro).FirstOrDefaultAsync();

        public async Task ActualizarMetodoPago(MetodoPago metodoPago)
        {
            _context.MetodoPagos.Update(metodoPago);
            await Guardar();
        }

    }
}
