using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Domain.Food
{
    [NotMapped]
    [Serializable]
    public class ComidaLog
    {
        public ComidaLog() {  }

        public string Nombre { get; set; }
        public float Precio { get; set; }
        public Dictionary<int, CaracteristicaComidaLog> Caracteristicas { get; set; }
    }
}
