using Delivery.Domain.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Domain.Auxiliares
{
    [NotMapped]
    public class ListaUsuarios
    {
        public List<Cliente> Clientes { get; set; }
        public List<Repartidor> Repartidores { get; set; }
        public List<Chef> Chefs { get; set; }
        public List<Administrador> Administradores { get; set; }

    }
}
