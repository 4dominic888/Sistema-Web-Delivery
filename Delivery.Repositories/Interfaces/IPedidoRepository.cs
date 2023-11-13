using Delivery.Domain.Food;
using Delivery.Domain.Order;

namespace Delivery.Repositories.Interfaces
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        public Task Registrar_Direccion(Direccion direccion);
        public Task Registrar_MetodoPago(MetodoPago metodoPago);
        public Task Registrar_Pedido(Pedido pedido);

        public Task<Direccion> BuscarDirreccionId(int id);
        public Task<MetodoPago> BuscarMetodoPagoId(int? metodoPagoId);
        public Task<List<Comida_CaracteristicaPedido>> BuscarComidasPedido(int id);
    }
}
