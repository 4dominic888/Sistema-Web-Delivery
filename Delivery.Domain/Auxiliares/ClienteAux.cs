using Delivery.Domain.User;

namespace Delivery.Domain.Auxiliares
{
    public class ClienteAux
    {
        public ClienteAux()
        {
            
        }
        public ClienteAux(Cliente Cliente)
        {
            this.Cliente = Cliente;
        }
        public Cliente Cliente { get; set; }
        public PasswordAux PasswordAux { get; set; }
    }
}
