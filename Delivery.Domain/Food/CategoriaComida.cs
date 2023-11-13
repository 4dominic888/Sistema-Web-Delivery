using System.ComponentModel.DataAnnotations;

namespace Delivery.Domain.Food
{
    public enum CategoriaComida
    {
        Caldo,
        Estofado,
        Guisado,
        Saltado,
        Frito,
        Ceviche,
        [Display(Name = "Plato típico")]
        Plato_típico,
        Picante,
        Parilla,
        [Display(Name = "Con tortilla")]
        Con_Tortilla,
        Otros
    }
}
