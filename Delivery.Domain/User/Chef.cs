using System.ComponentModel.DataAnnotations;
namespace Delivery.Domain.User
{
    public class Chef : Usuario
    {
        public Chef(Usuario usuario) : base(usuario) { }
        public Chef() { }
    }
}
