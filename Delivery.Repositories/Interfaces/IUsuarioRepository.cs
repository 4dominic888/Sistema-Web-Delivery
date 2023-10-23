using Delivery.Domain.User;

namespace Delivery.Repositories.Interfaces
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<bool> EmailRepetido(string correo);
        Task<bool> PhoneRepetido(string phone);
        Task<Usuario> ValidarUsuario(string correo, string password);
        Task RegistrarCliente(Cliente cliente);
        Task<Cliente> BuscarClienteID(int id);
        string EncriptarSHA256(string password);
    }
}
