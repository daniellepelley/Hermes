using System.Linq;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly InMemoryDataContext<T> _dataContext;

        public IDataContext DataContext
        {
            get { return _dataContext; }
        }

        public IQueryable<T> Items
        {
            get { return _dataContext.List.AsQueryable(); }
        }

        public InMemoryRepository(InMemoryDataContext<T> dataContext)
        {
            _dataContext = dataContext;
        }

        public void Delete(T entity)
        {
            _dataContext.List.Remove(entity);
        }

        public void Insert(T entity)
        {
            _dataContext.List.Add(entity);
        }
    }
}