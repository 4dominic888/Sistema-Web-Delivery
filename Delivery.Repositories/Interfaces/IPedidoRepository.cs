using Delivery.Domain.Food;
using Delivery.Domain.Order;
using System.Linq.Expressions;

namespace Delivery.Repositories.Interfaces
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        /// <summary>
        /// Registra una dirección en la base de datos
        /// </summary>
        /// <param name="direccion"></param>
        /// <returns></returns>
        Task Registrar_Direccion(Direccion direccion);


        /// <summary>
        /// Registra un método de pago en la base de datos
        /// </summary>
        /// <param name="metodoPago"></param>
        /// <returns></returns>
        Task Registrar_MetodoPago(MetodoPago metodoPago);

        /// <summary>
        /// Registra un pedido en la base de datos
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        Task Registrar_Pedido(Pedido pedido);

        /// <summary>
        /// Para convertir la cadena guardada en JSON a un formato de vista para C#
        /// </summary>
        /// <param name="JSON"></param>
        /// <returns></returns>
        Dictionary<int, ComidaLog> DeserealizarJSON(string JSON);

        /// <summary>
        /// Se busca una dirección en base al filtro usado
        /// </summary>
        /// <param name="filtro">query</param>
        /// <returns></returns>
        Task<Direccion?> Buscar_Direccion(Expression<Func<Direccion, bool>> filtro);

        /// <summary>
        /// Se busca un método de pago en base al filtro usado
        /// </summary>
        /// <param name="filtro">query</param>
        /// <returns></returns>
        Task<MetodoPago?> Buscar_MetodoPago(Expression<Func<MetodoPago, bool>> filtro);

        /// <summary>
        /// Actualizar un método de pago existente
        /// </summary>
        /// <param name="metodoPago"></param>
        /// <returns></returns>
        Task ActualizarMetodoPago(MetodoPago metodoPago);
    }
}
