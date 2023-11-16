using Delivery.Domain.Order;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Domain.User
{
    public class Repartidor : Usuario
    {

        public Repartidor(Usuario usuario) : base(usuario)
        {

        }

        public Repartidor()
        {
            
        }

        [DataType(DataType.Currency)]
        public Pedido? PedidoEnCurso { get; set; }
    }
}
