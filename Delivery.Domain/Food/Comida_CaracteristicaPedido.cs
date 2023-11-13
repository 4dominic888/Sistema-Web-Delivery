using Delivery.Domain.Order;
using Delivery.Domain.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }


        public int IdComida { get; set; }

        [ForeignKey(nameof(IdComida))]
        public Comida? Comida { get; set; }

        
        public int? IdCaracteristicaComida { get; set; }

        [ForeignKey(nameof(IdCaracteristicaComida))]
        public CaracteristicaComida? caracteristicaComida { get; set; }

        public int? IdPedido { get; set; }
        [ForeignKey(nameof(IdPedido))]
        public Pedido? Pedido { get; set; }

        public int IdCliente { get; set; }

        [ForeignKey(nameof(IdCliente))]
        public Cliente? Cliente { get; set; }

        public int agrupamiento { get; set; }
    }
}
