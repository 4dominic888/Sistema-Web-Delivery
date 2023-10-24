using Delivery.Domain.Food;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;


namespace Delivery.Repositories.Interfaces
{
	public interface IComidaRepository : IRepositoryBase<Comida>
	{
        #region Caracteristicas Comida
        Task<IEnumerable<CaracteristicaComida>> ObtenerCaracteristicasComidas();
		Task<CaracteristicaComida> ObtenerCaracteristicaPorID(int id);
		Task EliminarCaracteristica(CaracteristicaComida caracteristicaComida);
		Task ActualizarCategoria(CaracteristicaComida categoriaComida);
        Task AgregarCaracteristica(CaracteristicaComida caracteristica);
		Task<bool> CaracteristicaNombreUnico(string nombre);

        #endregion


        Task AgregarRelacionCaracteristicaComida(int idComida, int idCaracteristica);

        Task<IEnumerable<Comida>> ObtenerComidas();
        string CargarImagen(HttpContext httpContext, IWebHostEnvironment _webHostEnvironment);

    }
}
