namespace Delivery.Domain.Food
{
    public class Comida_Caracteristica
    {
        public Comida_Caracteristica(int IdComida, int IdCaracteristicaComida)
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
