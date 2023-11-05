using Delivery.Domain.Food;
using Delivery.Domain.Order;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Domain.User
{
    public class Cliente : Usuario
    {
        public CategoriaComida PreferenciaCategoria { get; set;}
        public List<Pedido>? Pedidos { get; set; } = new List<Pedido>();
    }
}
