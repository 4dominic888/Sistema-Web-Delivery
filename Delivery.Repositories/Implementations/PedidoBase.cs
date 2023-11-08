using Delivery.Domain.Order;
using Delivery.Persistence.Data;
using Delivery.Repositories.Interfaces;

namespace Delivery.Repositories.Implementations
{
    public class PedidoBase : RepositoryBase<Pedido>, IPedidoRepository
    {
        private readonly DeliveryDBContext _context;
        public PedidoBase(DeliveryDBContext context) : base(context)
        {
            this._context = context;
        }
    }
}
