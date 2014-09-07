using System.Linq;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.Repositories.Decorators
{
    public class RepositoryDecoratorBase<T>
        : IRepository<T> where T : class
    {
        protected IRepository<T> Repository;

        public virtual IDataContext DataContext
        {
            get { return Repository.DataContext; }
        }

        public virtual IQueryable<T> Items
        {
            get
            {
                return Repository.Items;
            }
        }

        public RepositoryDecoratorBase(IRepository<T> repository)
        {
            Repository = repository;
        }

        public virtual void Delete(T entity)
        {
            Repository.Delete(entity);
        }

        public virtual void Insert(T entity)
        {
            Repository.Insert(entity);
        }
    }
}
