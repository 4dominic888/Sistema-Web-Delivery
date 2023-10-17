using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;

namespace Delivery.Domain.Food
{
    public class CaracteristicaComida
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [NotNull]
        [MinLength(3), MaxLength(200)]
        public string Nombre { get; set; }


        [DataType(DataType.Text)]
        public string Detalle { get; set; }



        [Required]
        [NotNull]
        [DataType(DataType.Currency)]
        public float Precio { get; set; }



        public int? IdComida { get; set; }

        [ForeignKey(nameof(IdComida))]
        public Comida? Comida { get; set; }
    }
}
