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
        private new readonly DeliveryDBContext _context;
        public UsuarioBase(DeliveryDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> Bloquear_Usuario(int UID)
        {
            var Usuario = await BuscarUsuario(UID);

            if (Usuario.Bloqueado) return "Este usuario ya está bloqueado";
            else
            {
                Usuario.Bloqueado = true;
                _context.Usuarios.Update(Usuario);
                await Guardar();
                return null;
            }
        }

        public async Task<string> Desbloquear_Usuario(int UID)
        {
            var Usuario = await BuscarUsuario(UID);

            if (!Usuario.Bloqueado) return "Este usuario ya está desbloqueado";
            else
            {
                Usuario.Bloqueado = false;
                _context.Usuarios.Update(Usuario);
                await Guardar();
                return null;
            }
        }

        public async Task Cambiar_Rol(int UID, Rol rol)
        {
            var Usuario = await BuscarUsuario(UID);
            Usuario.Rol = rol;
            _context.Update(Usuario);
            await Guardar();
        }

        public async Task RegistrarUsuario(Usuario usuario)
        {
            await Agregar(usuario);
            await Guardar();
        }

        public async Task<bool> EmailRepetido(string correo)
            => await ObtenerPrimero(u => u.Email == correo) is not null;

        public async Task<bool> PhoneRepetido(string phone)
            => await ObtenerPrimero(u => u.Phone == phone) is not null;

        public async Task<Usuario> ValidarUsuario(string correo, string password)
            => await ObtenerPrimero(c => c.Email == correo && c.Password == password);

        public async Task<Usuario> BuscarUsuario(int id) => await ObtenerPrimero(u => u.Id == id);

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

    }
}
