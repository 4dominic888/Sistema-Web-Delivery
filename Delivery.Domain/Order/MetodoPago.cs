using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Delivery.Domain.Order
{
    public class MetodoPago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public TipoMetodoPago Tipo { get; set; }


        [MinLength(6), MaxLength(150)]
        public string? NombreTarjeta { get; set; }


        [MinLength(13), MaxLength(18)]
        [DataType(DataType.CreditCard)]
        public string? Numero { get; set; }


        [DataType(DataType.Date)]
        [AllowNull]
        public DateTime? fechaExpiracion { get; set; }


        public string? CVV { get; set; }
    }
}
