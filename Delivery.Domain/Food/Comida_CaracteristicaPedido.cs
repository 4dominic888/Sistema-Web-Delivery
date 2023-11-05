using Delivery.Domain.Order;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Domain.Food
{
    public class Comida_CaracteristicaPedido
    {
        public Comida_CaracteristicaPedido() { }

        public int IdComida { get; set; }
        public Comida? Comida { get; set; }
        public int? IdCaracteristicaComida { get; set; }
        public CaracteristicaComida? CaracteristicaComida { get; set; }

        public int? IdPedido { get; set; }
        public Pedido? Pedido { get; set; }

        public int IdCliente { get; set; }
        public int agrupamiento { get; set; }
    }
}
