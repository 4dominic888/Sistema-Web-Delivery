using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.User
{
    public class UsuarioLog
    {

        public UsuarioLog() { }
        public UsuarioLog(Usuario usuario)
        {
            Id = usuario.Id;
            Surname = usuario.Surname;
            Name = usuario.Name;
            Phone = usuario.Phone;
            Sexo = usuario.Sexo;
            Email = usuario.Email;
            Rol = usuario.Rol;
        }

        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Sexo Sexo { get; set; }
        public string Email { get; set; }
        public Rol Rol { get; set; }
    }
}
