using System.Linq;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.Repositories.Decorators
{
    public class InstantPersistanceRepository<T>
        : IRepository<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public IDataContext DataContext
        {
            get { return _repository.DataContext; }
        }

        public IQueryable<T> Items
        {
            get { return _repository.Items; }
        }

        public InstantPersistanceRepository(IRepository<T> repository)
        {
            _repository = repository;
        }

        //public void Save(T entity)
        //{
        //    _repository.Save(entity);
        //    DataContext.SaveChanges();
        //}

        public void Delete(T entity)
        {
            _repository.Delete(entity);
            DataContext.SaveChanges();
        }

        public void Insert(T entity)
        {
            _repository.Insert(entity);
            DataContext.SaveChanges();
        }
    }
}
