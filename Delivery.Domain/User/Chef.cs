using System.ComponentModel.DataAnnotations;
namespace Delivery.Domain.User
{
    public class Chef : Usuario
    {
        public float Sueldo { get; set; }
    }
}
