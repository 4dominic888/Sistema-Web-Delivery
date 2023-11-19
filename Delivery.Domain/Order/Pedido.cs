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

        public Pedido() { }

        public Pedido(string vacio)
        {
            Direccion = new Direccion();
            MetodoPago = new MetodoPago();
        }

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

        [NotNull]
        public string Cliente { get; set; }
        [NotNull]
        public string Telefono { get; set; }

        [AllowNull]
        public int? IdRepartidor { get; set; }

        [AllowNull]
        public string? Repartidor { get; set; }

        [ForeignKey(nameof(IdDireccion))]
        public Direccion Direccion { get; set; }

        [ForeignKey(nameof(IdMetodoPago))]
        public MetodoPago? MetodoPago { get; set; }
    }
}
