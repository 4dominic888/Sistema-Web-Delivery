using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Domain.Food
{
    [NotMapped]
    [Serializable]
    public class CaracteristicaComidaLog
    {
        public CaracteristicaComidaLog() { }
        public int ID { get; set; }
        public string Nombre { get; set; }
        public float Precio { get; set; }
    }
}
