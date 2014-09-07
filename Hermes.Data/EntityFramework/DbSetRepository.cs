using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Hermes.Data.Repositories.Interfaces;


namespace Hermes.Data.EntityFramework
{
    public class DbSetRepository<T> : IRepository<T> where T : class
    {
        private readonly DbDataContext _dataContext;
        private readonly DbSet<T> _entitySet;

        public IDataContext DataContext
        {
            get { return _dataContext; }
        }

        public IQueryable<T> Items
        {
            get { return _dataContext.DbContext.Set<T>(); }
        }

        public DbSetRepository(DbContext objectContext)
            : this(new DbDataContext(objectContext))
        {
        }

        public DbSetRepository(DbDataContext dataContext)
        {
            _dataContext = dataContext;
            _entitySet = _dataContext.DbContext.Set<T>(); ;
        }

        public void Save(T entity)
        {
            if (!Contains(entity))
                _entitySet.Attach(entity);

            ((IObjectContextAdapter)_dataContext.DbContext).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }

        public void Delete(T entity)
        {
            if (!Contains(entity))
                _entitySet.Attach(entity);
            
            ((IObjectContextAdapter)_dataContext.DbContext).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Deleted);
        }

        public void Create(T entity)
        {
            if (!Contains(entity))
                _entitySet.Add(entity);

            ((IObjectContextAdapter)_dataContext.DbContext).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Added);
        }

        public IQueryable<T> Query(IQuery query)
        {
            if (query is PredicateQuery<T>)
                return _entitySet.Where(((PredicateQuery<T>) query).Predicate).AsQueryable();
            return null;
        }

        public bool Contains(T item)
        {
            ObjectStateEntry state;
            if (!((IObjectContextAdapter)_dataContext.DbContext).ObjectContext.ObjectStateManager.TryGetObjectStateEntry(item, out state))
                return false;
            return (state.State != EntityState.Detached);
        }
    }
}