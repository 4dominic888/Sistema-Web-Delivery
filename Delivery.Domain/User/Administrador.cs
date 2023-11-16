using System.ComponentModel.DataAnnotations;

namespace Delivery.Domain.User
{
    
    public class Administrador : Usuario
    {
        public Administrador(Usuario usuario) : base(usuario) { }
        public Administrador() { }
    }
}
