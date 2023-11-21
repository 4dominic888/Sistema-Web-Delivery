using System.Linq.Expressions;

namespace Delivery.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> ObtenerPorId(int id); //Obtener un elemento de la entidad T

        Task<ICollection<T>> ObtenerTodos(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            bool isTracking = true
        ); // recuperar todos los elementos

        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null,
            bool isTracking = true
        ); //para obtener el primer elemento

        Task Agregar(T entity); //Agregar un registro
        void Remover(T entity); //Eliminar un registro
        void Actualizar(T entity);
        Task Guardar();
    }
}
