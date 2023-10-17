using Delivery.Repositories.Interfaces;

namespace Delivery.Repositories.Implementations
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public RepositoryBase()
        {

        }
    }
}