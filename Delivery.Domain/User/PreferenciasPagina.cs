using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Domain.User
{
    public class PreferenciasPagina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public bool ContenidoDestacado { get; set; } = false;
        public bool Recomendaciones { get; set; } = false;

        
        public Usuario? Usuarios { get; set; }
    }
}
