using Delivery.Domain.User;
using Delivery.Persistence.Data;
using Delivery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task RegistrarRepartidor(Usuario repartidor)
        {
            await _Usercontext.Repartidores.AddAsync(new Repartidor(repartidor));
            await Guardar();
        }

        public async Task RegistrarChef(Usuario chef)
        {
            await _Usercontext.Chefs.AddAsync(new Chef(chef));
            await Guardar();
        }

        public async Task RegistrarAdministrador(Usuario administrador)
        {
            await _Usercontext.Administradores.AddAsync(new Administrador(administrador));
            await Guardar();
        }

        public async Task<bool> EmailRepetido(string correo)
        {
            Cliente cliente = await _Usercontext.Clientes.Where(c => c.Email == correo).FirstOrDefaultAsync();
            if(cliente != null) return true;
            Repartidor repartidor = await _Usercontext.Repartidores.Where(c => c.Email == correo).FirstOrDefaultAsync();
            if (repartidor != null) return true;
            Chef chef = await _Usercontext.Chefs.Where(c => c.Email == correo).FirstOrDefaultAsync();
            if (chef != null) return true;
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
            Chef chef = await _Usercontext.Chefs.Where(c => c.Phone == phone).FirstOrDefaultAsync();
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
            Chef chUser = await _Usercontext.Chefs.Where(c => c.Email == correo && c.Password == password).FirstOrDefaultAsync();
            if (chUser != null) return chUser;
            Administrador aUser = await _Usercontext.Administradores.Where(c => c.Email == correo && c.Password == password).FirstOrDefaultAsync();
            if (aUser != null) return aUser;
            return null;
        }

        public async Task<Cliente> BuscarClienteID(int id)
        {
            return await _context.Clientes.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Repartidor> BuscarRepartidorID(int? id)
        {
            return await _context.Repartidores.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Usuario> BuscarUsuarioID(int id)
        {
            Cliente cUser = await _Usercontext.Clientes.FindAsync(id);
            if(cUser != null) return cUser;
            Repartidor rUser = await _Usercontext.Repartidores.FindAsync(id);
            if(rUser != null) return rUser;
            Chef chUser = await _context.Chefs.FindAsync(id);
            if(chUser != null) return chUser;
            Administrador aUser = await _context.Administradores.FindAsync(id);
            if(aUser != null) return aUser;
            return null;
        }

        public async Task<List<Cliente>> ObtenerClientes()
        {
            return await _Usercontext.Clientes.ToListAsync();
        }

        public async Task<List<Repartidor>> ObtenerRepartidores()
        {
            return await _Usercontext.Repartidores.ToListAsync();
        }

        public async Task<List<Chef>> ObtenerChefs()
        {
            return await _Usercontext.Chefs.ToListAsync();
        }

        public async Task<List<Administrador>> ObtenerAdministradores(int id = 0)
        {
            if (id != 0)
            {
                return await _Usercontext.Administradores.Where(a => a.Id != id).ToListAsync();
            }
            else return await _Usercontext.Administradores.ToListAsync();
        }
    }
}
