using Delivery.Domain.User;

namespace Delivery.Repositories.Interfaces
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<bool> EmailRepetido(string correo);
        Task<bool> PhoneRepetido(string phone);
        Task<Cliente> ValidarCliente(string correo, string password);
        Task RegistrarCliente(Cliente cliente);
        string EncriptarSHA256(string password);
    }
}
