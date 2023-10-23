using Delivery.Domain.User;
using Delivery.Persistence.Data;
using Delivery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Delivery.Repositories.Implementations
{
    public class UsuarioBase : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly DeliveryDBContext _Usercontext;
        public UsuarioBase(DeliveryDBContext context) : base(context)
        {
            _Usercontext = context;
            
        }

        public string EncriptarSHA256(string password)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public async Task RegistrarCliente(Cliente cliente)
        {
            await _Usercontext.Clientes.AddAsync(cliente);
            await _Usercontext.SaveChangesAsync();
        }

        public async Task<bool> EmailRepetido(string correo)
        {
            Cliente cliente = await _Usercontext.Clientes.Where(c => c.Email == correo).FirstOrDefaultAsync();
            if(cliente != null) return true;
            Repartidor repartidor = await _Usercontext.Repartidores.Where(c => c.Email == correo).FirstOrDefaultAsync();
            if (repartidor != null) return true;
            Administrador administrador = await _Usercontext.Administradores.Where(c => c.Email == correo).FirstOrDefaultAsync();
            if (administrador != null) return true;
            return false;
        }

        public async Task<bool> PhoneRepetido(string phone)
        {
            Cliente cliente = await _Usercontext.Clientes.Where(c => c.Phone == phone).FirstOrDefaultAsync();
            if (cliente != null) return true;
            Repartidor repartidor = await _Usercontext.Repartidores.Where(c => c.Phone == phone).FirstOrDefaultAsync();
            if (repartidor != null) return true;
            Administrador administrador = await _Usercontext.Administradores.Where(c => c.Phone == phone).FirstOrDefaultAsync();
            if (administrador != null) return true;
            return false;
        }

        public async Task<Usuario> ValidarUsuario(string correo, string password)
        {
            Cliente cUser = await _Usercontext.Clientes.Where(c => c.Email == correo && c.Password == password).FirstOrDefaultAsync();
            if (cUser != null) return cUser;
            Repartidor rUser = await _Usercontext.Repartidores.Where(c => c.Email == correo && c.Password == password).FirstOrDefaultAsync();
            if (rUser != null) return rUser;
            Administrador aUser = await _Usercontext.Administradores.Where(c => c.Email == correo && c.Password == password).FirstOrDefaultAsync();
            if (aUser != null) return aUser;
            return null;
        }

        public async Task<Cliente> BuscarClienteID(int id)
        {
            return await _context.Clientes.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}
