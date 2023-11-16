using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;

namespace Delivery.Domain.Food
{
    public class CaracteristicaComida
    {
        public CaracteristicaComida(CaracteristicaComida caracteristica)
        {
            Id = caracteristica.Id;
            Nombre = caracteristica.Nombre;
            Detalle = caracteristica.Detalle;
            Precio = caracteristica.Precio;
        }
        public CaracteristicaComida(){ }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "Este campo es necesario")]
        [NotNull]
        [MinLength(3), MaxLength(200)]
        public string Nombre { get; set; }


        [DataType(DataType.Text)]
        public string Detalle { get; set; }



        [Required(ErrorMessage = "El campo no puede estar vacío")]
        [Range(0.0, 30, ErrorMessage = "El precio no puede ser negativo ni mayor a 30")]
        [DataType(DataType.Currency, ErrorMessage = "El precio es inválido")]
        public float Precio { get; set; }

        public List<Comida_CaracteristicaMenu>? comida_CaracteristicasMenu { get; set; }

        public List<Comida_CaracteristicaPedido>? Comida_CaracteristicasPedido { get; set; }
    }
}
