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


        [Required ,NotNull]
        [Display(Name = "Descripción")]
        [DataType(DataType.Text)]
        public string Descripcion { get; set; }


        public CategoriaComida Categoria { get; set; }


        [Required, NotNull]
        [DataType(DataType.Currency)]
        public float Precio { get; set; }


        [Required, NotNull]
        public bool MenuDelDia { get; set; }


        [Required, NotNull]
        public int Stock { get; set; }


        [Required, NotNull]
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }


        public int? IdPedido { get; set; }


        [ForeignKey(nameof(IdPedido))]
        public Pedido? Pedido { get; set; }

        List<CaracteristicaComida> CaracteristicaComidas { get; set; }

    }
}
