using Delivery.Domain.Order;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Domain.Food
{
    public class Comida_CaracteristicaPedido
    {
        public Comida_CaracteristicaPedido() { }

        public Comida_CaracteristicaPedido(string Contenido, int IdPedido)
        {
            this.Contenido = Contenido;
            this.IdPedido = IdPedido;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Contenido { get; set; }

        public int IdPedido { get; set; }

        [ForeignKey(nameof(IdPedido))]
        public Pedido Pedido { get; set; }

    }
}
