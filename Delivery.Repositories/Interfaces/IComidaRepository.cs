using Delivery.Domain.Food;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;


namespace Delivery.Repositories.Interfaces
{
	public interface IComidaRepository : IRepositoryBase<Comida>
	{
        #region Caracteristicas Comida

        /// <summary>
        /// Agregas una característica
        /// </summary>
        /// <param name="caracteristica">Objeto a agregar</param>
        /// <returns></returns>
        Task AgregarCaracteristica(CaracteristicaComida caracteristica);

        /// <summary>
        /// Busca en la base de datos una característica con el nombre pasado
        /// </summary>
        /// <param name="nombre">Nombre de la característica a validar que exista</param>
        /// <returns></returns>
        Task<bool> CaracteristicaNombreUnico(string nombre);

        /// <summary>
        /// Actualizar la caracteristica
        /// </summary>
        /// <param name="categoriaComida">Objeto a actualizar</param>
        /// <returns></returns>
        Task ActualizarCaracteristica(CaracteristicaComida categoriaComida);

        /// <summary>
        /// Eliminas la característica en cascada
        /// </summary>
        /// <param name="caracteristicaComida">Objeto a eliminar</param>
        /// <returns></returns>
        Task EliminarCaracteristica(CaracteristicaComida caracteristicaComida);

        /// <summary>
        /// Obtener la caracteristica mediante el ID
        /// </summary>
        /// <param name="id">ID del objeto</param>
        /// <returns></returns>
        Task<CaracteristicaComida> ObtenerCaracteristicaPorID(int id);

        /// <summary>
        /// Obtener el listado completo de características registradas
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CaracteristicaComida>> ObtenerCaracteristicasComidas();

        /// <summary>
        /// Obtener las caracteristicas de una comida
        /// </summary>
        /// <param name="id">Id de la comida</param>
        /// <returns></returns>
        public Task<IEnumerable<CaracteristicaComida>> ObtenerCaracteristicasPorComidaID(int id);

        /// <summary>
        /// Agregar la relación entre caracteristica y comida
        /// </summary>
        /// <param name="idComida">ID de la comida</param>
        /// <param name="idCaracteristica">ID de la caracteristica</param>
        /// <returns></returns>
        Task AgregarRelacionCaracteristicaComida(int idComida, int idCaracteristica);

        #endregion


        /// <summary>
        /// Agregar una comida a la base de datos
        /// </summary>
        /// <param name="comida">Objeto a agregar</param>
        /// <param name="listaIndicescarac">Lista de indices en formato JSON string</param>
        /// <returns></returns>
        public Task AgregarComida(Comida comida, string listaIndicescarac);

        /// <summary>
        /// Editar la comida de la base de datos
        /// </summary>
        /// <param name="comida">Objeto a actualizar</param>
        /// <param name="listaIndicescara">Lista de indices nueva en formato JSON string</param>
        /// <returns></returns>
        public Task EditarComida(Comida comida, string listaIndicescara);

        /// <summary>
        /// Obtener todo el listado de comidas
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Comida>> ObtenerComidas();

        /// <summary>
        /// Cargar una imagen al servidor, solo válido
        /// para formularios en los que se cargue un archivo,
        /// no se acepta valores nulos, controlarlo en un
        /// try catch
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="_webHostEnvironment"></param>
        /// <returns></returns>
        string CargarImagen(HttpContext httpContext, IWebHostEnvironment _webHostEnvironment);


        /// <summary>
        /// Se elimina una imagen del servidor
        /// </summary>
        /// <param name="productURL">URL de la imagen a borrar</param>
        /// <param name="_webHostEnvironment"></param>
        public void EliminarImagen(string productURL, IWebHostEnvironment _webHostEnvironment);
    }
}
