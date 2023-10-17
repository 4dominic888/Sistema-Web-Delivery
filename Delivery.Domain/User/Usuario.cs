using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Delivery.Domain.User
{
    [NotMapped]
    public abstract class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required, NotNull]
        [Display(Name = "Apellido")]
        [MinLength(3), MaxLength(200)]
        public string Surname { get; set; }


        [Required,NotNull]
        [Display(Name = "Nombre")]
        [MinLength(3), MaxLength(200)]
        public string Name { get; set; }


        [Required,NotNull]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        [MinLength(10), MaxLength(15)]
        public string Phone { get; set; }


        [Required]
        public Sexo Sexo { get; set; }



        [Required, NotNull]
        [Display(Name = "Correo electrónico")]
        [DataType(DataType.EmailAddress)]
        [MinLength(3), MaxLength(200)]
        public string Email { get; set; }


        [Required,NotNull]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [MinLength(8), MaxLength(80)]
        public string Password { get; set; }


        [Required, NotNull]
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime DateBirth { get; set; }


        [MaxLength(100)]
        public string Rol { get; set; } = nameof(Usuario);


        public int IdPreferenciaPagina { get; set; }

        [ForeignKey(nameof(IdPreferenciaPagina))]
        public PreferenciasPagina PreferenciasPagina { get; set; }

    }
}
