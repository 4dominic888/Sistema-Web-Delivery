using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Delivery.Domain.Order
{
    public class Direccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }



        [Required, NotNull]
        [MinLength(3), MaxLength(200)]
        public string Nombre_Calle { get; set; }


        [Required, NotNull]
        public int Numero { get; set; }


        [DataType(DataType.Text)]
        public string Detalle { get; set; }


        public Pedido? Pedido { get; set; }
    }
}
