using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Delivery.Domain.User
{
    [NotMapped]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "Este campo es requerido"), NotNull]
        [Display(Name = "Apellido")]
        [MinLength(3, 
            ErrorMessage = "El apellido es demasiado corto"), 
        MaxLength(200, 
            ErrorMessage = "El apellido es demasiado largo")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Este campo es requerido"), NotNull]
        [Display(Name = "Nombre")]
        [MinLength(3,
            ErrorMessage = "El nombre es demasiado corto"),
        MaxLength(200,
            ErrorMessage = "El nombre es demasiado largo")]
        public string Name { get; set; }

        [RegularExpression(@"^9\d{8}$", ErrorMessage = "El número de teléfono no es válido.")]
        [Required(ErrorMessage = "Este campo es requerido"), NotNull]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }


        [Required]
        public Sexo Sexo { get; set; }



        [Required(ErrorMessage = "Este campo es requerido"), NotNull]
        [Display(Name = "Correo electrónico")]
        [DataType(DataType.EmailAddress)]
        [MinLength(3,
            ErrorMessage = "El email es demasiado corto"),
        MaxLength(200,
            ErrorMessage = "El email es demasiado largo")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Este campo es requerido"), NotNull]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [MinLength(8, 
            ErrorMessage = "La contraseña es demasiada corta"), 
        MaxLength(100, ErrorMessage = "Lac contraseña es demasiada larga")]
        public string Password { get; set; }


        [Required, NotNull]
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime DateBirth { get; set; }


        [MaxLength(100)]
        public string Rol { get; set; }


        public bool ContenidoDestacado { get; set; } = false;
        public bool Recomendaciones { get; set; } = false;

    }
}
