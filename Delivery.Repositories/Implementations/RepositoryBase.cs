using Delivery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Delivery.Persistence.Data;

namespace Delivery.Repositories.Implementations
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public RepositoryBase()
        { }

        protected readonly DeliveryDBContext _context;
        internal DbSet<T> dbSet;

        public RepositoryBase(DeliveryDBContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }
        public async Task Agregar(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task Guardar()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T> ObtenerPorId(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<ICollection<T>> ObtenerTodos(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filter is not null)
                query = query.Where(filter);

            //Include Properties separados por coma
            if (includeProperties is not null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy is not null)
                query = orderBy(query);

            if (!isTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }
        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filter = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filter is not null)
                query = query.Where(filter);

            //Include Properties separados por coma
            if (includeProperties is not null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (!isTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public void Remover(T entity)
        {
            dbSet.Remove(entity);
        }
    
    }
}