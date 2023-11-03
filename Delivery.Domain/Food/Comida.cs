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
        [Range(0, 100)]
        public float Precio { get; set; }


        [Required, NotNull]
        public bool MenuDelDia { get; set; }


        [Required, NotNull]
        [Range(0, 900, ErrorMessage = "El stock debe ser mayor a 0 y menor a 900")]
        public int Stock { get; set; }


        public string? Imagen { get; set; }


        public int? IdPedido { get; set; }


        [ForeignKey(nameof(IdPedido))]
        public Pedido? Pedido { get; set; }

        public List<Comida_Caracteristica>? comida_Caracteristicas { get; set; }

    }
}
