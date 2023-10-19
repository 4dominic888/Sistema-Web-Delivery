using Delivery.Domain.User;
using Delivery.Persistence.Data;
using Delivery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Delivery.Repositories.Implementations
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly DeliveryDBContext _Usercontext;
        public UsuarioRepository(DeliveryDBContext context) : base(context)
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
            var cliente = await _Usercontext.Clientes.Where(c => c.Email == correo).FirstOrDefaultAsync();
            if(cliente == null) return false;
            else return true;
        }

        public async Task<bool> PhoneRepetido(string phone)
        {
            var cliente = await _Usercontext.Clientes.Where(c => c.Phone == phone).FirstOrDefaultAsync();
            if (cliente == null) return false;
            else return true;
        }

        public async Task<Cliente> ValidarCliente(string correo, string password)
        {
            return await _Usercontext.Clientes.Where(c => c.Email == correo && c.Password == password).FirstOrDefaultAsync();
        }


    }
}
