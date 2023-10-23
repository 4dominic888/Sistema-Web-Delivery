using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Delivery.Domain.Auxiliares
{
    [NotMapped]
    public class PasswordAux
    {
        [Required(ErrorMessage = "Este campo es requerido"), NotNull]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "La contraseña es demasiada corta"),
                MaxLength(100, ErrorMessage = "Lac contraseña es demasiada larga")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Este campo es requerido"), NotNull]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "La contraseña es demasiada corta"),
        MaxLength(100, ErrorMessage = "Lac contraseña es demasiada larga")]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Este campo es requerido"), NotNull]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "La contraseña es demasiada corta"),
        MaxLength(100, ErrorMessage = "Lac contraseña es demasiada larga")]
        public string ConfirmPassword { get; set; }
    }
}
