using Delivery.Domain.Order;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;

namespace Delivery.Domain.Food
{
    public class Comida
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }


        [Required, NotNull]
        [MinLength(3), MaxLength(200)]
        public string Nombre { get; set; }


        [Required]
        [DataType(DataType.Text)]
        public string Descripcion { get; set; }


        public CategoriaComida Categoria { get; set; }


        [Required, NotNull]
        [DataType(DataType.Currency)]
        [Range(5, 100, ErrorMessage = "El precio no debe ser menor a S/. 5 ni mayor a S/. 100")]
        public float Precio { get; set; }


        [Required, NotNull]
        public bool MenuDelDia { get; set; }


        [Required, NotNull]
        [Range(0, 900, ErrorMessage = "El stock debe ser mayor a 0 y menor a 900")]
        public int Stock { get; set; }


        public string? Imagen { get; set; }


        public List<Comida_CaracteristicaMenu>? comida_CaracteristicasMenu { get; set; }
        public List<Comida_CaracteristicaPedido>? Comida_CaracteristicasPedido { get; set; }
    }
}
