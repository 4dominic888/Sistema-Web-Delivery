namespace Delivery.Domain.Food
{
    /// <summary>
    /// Tabla que almacena las relaciones entre la comida y su características
    /// seleccionable en el menú.
    /// </summary>
    public class Comida_CaracteristicaMenu
    {
        public Comida_CaracteristicaMenu(int IdComida, int IdCaracteristicaComida)
        {
            this.IdComida = IdComida;
            this.IdCaracteristicaComida = IdCaracteristicaComida;
        }
        public int IdComida { get; set; }
        public Comida ?Comida { get; set; }
        public int IdCaracteristicaComida { get; set; }
        public CaracteristicaComida ?CaracteristicaComida { get; set; }
    }
}
