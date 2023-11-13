using System.ComponentModel.DataAnnotations;

namespace Delivery.Domain.Order
{
    public enum EstadoPedido
    {
        [Display(Name = "En proceso")]
        En_Proceso,
        Aceptado,
        Cancelado,
        Terminado
    }
}
