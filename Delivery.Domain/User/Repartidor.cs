using Delivery.Domain.Order;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Domain.User
{
    public class Repartidor : Usuario
    {
        [DataType(DataType.Currency)]
        public float? Sueldo { get; set; }

        public Pedido? PedidoEnCurso { get; set; }
    }
}
