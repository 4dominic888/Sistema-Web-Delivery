using Delivery.Domain.Food;

namespace Delivery.Repositories.Interfaces
{
	public interface IComidaRepository : IRepositoryBase<Comida>
	{
		Task<IEnumerable<CaracteristicaComida>> ObtenerCaracteristicasComidas();
        Task<IEnumerable<Comida>> ObtenerComidas();
		Task<CaracteristicaComida> ObtenerCaracteristicaPorID(int id);
		Task EliminarCaracteristica(CaracteristicaComida caracteristicaComida);
		Task ActualizarCategoria(CaracteristicaComida categoriaComida);

        Task AgregarCaracteristica(CaracteristicaComida caracteristica);

		Task<bool> CaracteristicaNombreUnico(string nombre);
	}
}
