using Delivery.Domain.Food;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Linq.Expressions;

namespace Delivery.Repositories.Interfaces
{
	public interface IComidaRepository : IRepositoryBase<Comida>
	{
        /// <summary>
        /// Registra en la base de datos las comidas pedidas junto a las características
        /// </summary>
        /// <param name="comidaLog">JSON a guardar</param>
        /// <param name="IdPedido">ID del pedido</param>
        /// <returns></returns>
        Task Registrar_Comida_Log(string comidaLog, int IdPedido);


        /// <summary>
        /// Listas comidas pedidas en base al filtro
        /// </summary>
        /// <param name="filtro">query</param>
        /// <returns></returns>
        Task<List<Comida_CaracteristicaPedido>?> Obtener_ComidasPedido(Expression<Func<Comida_CaracteristicaPedido, bool>> filtro);

        Task<Comida_CaracteristicaPedido?> Obtener_comidaPedidoId(int idPedido);


        #region Paginacion
        /// <summary>
        /// Obtiene un listado de las comidas pero paginado
        /// </summary>
        /// <param name="page">Página a la que se hará referencia</param>
        /// <returns></returns>
        IEnumerable<Comida> Obtener_comidas_paginado(IEnumerable<Comida> comidas ,int page);


        /// <summary>
        /// Se obtiene las páginas totales
        /// </summary>
        /// <param name="total_elementos"></param>
        /// <returns></returns>
        int PaginasTotales(IEnumerable<Comida> comidas);

        #endregion


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

        #region Comida


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
        /// Editar la comida de la base de datos
        /// </summary>
        /// <param name="comida"></param>
        /// <returns></returns>
        public Task EditarComida(Comida comida);

        /// <summary>
        /// Obtener todo el listado de comidas
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Comida>> ObtenerComidas();


        /// <summary>
        /// Se elimina la comida en cascada
        /// </summary>
        /// <param name="comida"></param>
        /// <returns></returns>
        public Task EliminarComida(Comida comida);
        

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

        #endregion
    }
}
