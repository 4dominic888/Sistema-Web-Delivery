using Delivery.Domain.Order;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Domain.Food
{
    public class Comida_CaracteristicaPedido
    {
        public Comida_CaracteristicaPedido() { }

        public Comida_CaracteristicaPedido(int IdComida, 
            int IdCaracteristicaComida,
            int IdCliente, int agrupamiento)
        {
            this.IdComida = IdComida;
            this.IdCaracteristicaComida = IdCaracteristicaComida;
            this.IdCliente = IdCliente;
            this.agrupamiento = agrupamiento;
        }

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
