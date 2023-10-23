namespace Delivery.Domain.Food
{
    public class Comida_Caracteristica
    {
        public int IdComida { get; set; }
        public Comida ?Comida { get; set; }
        public int IdCaracteristicaComida { get; set; }
        public CaracteristicaComida ?CaracteristicaComida { get; set; }


    }
}
