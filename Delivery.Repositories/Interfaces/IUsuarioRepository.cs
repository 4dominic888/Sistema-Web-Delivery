using Delivery.Domain.User;

namespace Delivery.Repositories.Interfaces
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<List<Cliente>> ObtenerClientes();
        Task RegistrarCliente(Cliente cliente);
        Task<List<Repartidor>> ObtenerRepartidores();
        Task RegistrarRepartidor(Usuario repartidor);
        Task<List<Chef>> ObtenerChefs();
        Task RegistrarChef(Usuario chef);
        Task<List<Administrador>> ObtenerAdministradores(int id);
        Task RegistrarAdministrador(Usuario administrador);
        Task<bool> EmailRepetido(string correo);
        Task<bool> PhoneRepetido(string phone);
        Task<Usuario> ValidarUsuario(string correo, string password);
        Task<Cliente> BuscarClienteID(int id);
        Task<Usuario> BuscarUsuarioID(int id);
        Task<Repartidor> BuscarRepartidorID(int? id);
        string EncriptarSHA256(string password);
    }
}
