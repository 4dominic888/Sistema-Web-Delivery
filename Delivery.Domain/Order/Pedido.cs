using Delivery.Domain.Food;
using Delivery.Domain.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;

namespace Delivery.Domain.Order
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }


        public EstadoPedido Estado { get; set; }


        [DataType(DataType.Currency)]
        public float? Total { get; set; }


        [Display(Name = "Fecha del pedido")]
        [DataType(DataType.DateTime)]
        public DateTime Fecha_Inicio { get; set; }


        [Display(Name = "Fecha de finalización")]
        [DataType(DataType.DateTime)]
        public DateTime Fecha_Fin { get; set; }


        [DataType(DataType.Text)]
        [AllowNull]
        public string? Detalle { get; set; }

        public int IdDireccion { get; set; }
        public int? IdMetodoPago { get; set; }

        [NotNull]
        public int IdCliente { get; set; }

        [AllowNull]
        public int? IdRepartidor { get; set; }


        public Cliente Cliente { get; set; }
        public Repartidor? Repartidor { get; set; }


        [ForeignKey(nameof(IdDireccion))]
        public Direccion Direccion { get; set; }

        [ForeignKey(nameof(IdMetodoPago))]
        public MetodoPago? MetodoPago { get; set; }


        public List<Comida_CaracteristicaPedido>? Comida_CaracteristicasPedido { get; set; }
    }
}
