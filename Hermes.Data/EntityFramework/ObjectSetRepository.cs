using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.EntityFramework
{
    public class ObjectSetRepository<T> : IRepository<T> where T : class
    {
        private readonly ObjectDataContext _dataContext;
        private readonly ObjectSet<T> _entitySet;

        public IDataContext DataContext
        {
            get { return _dataContext; }
        }

        public IQueryable<T> Items
        {
            get { return _dataContext.ObjectContext.CreateObjectSet<T>(); }
        }

        public ObjectSetRepository(ObjectContext objectContext)
            : this(new ObjectDataContext(objectContext))
        {
        }

        public ObjectSetRepository(ObjectDataContext dataContext)
        {
            _dataContext = dataContext;
            _entitySet = _dataContext.ObjectContext.CreateObjectSet<T>();
        }

        public void Save(T entity)
        {
            if (!Contains(entity))
                _entitySet.Attach(entity);

            _dataContext.ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }

        public void Delete(T entity)
        {
            if (!Contains(entity))
                _entitySet.Attach(entity);

            _dataContext.ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Deleted);
        }

        public void Insert(T entity)
        {
            if (!Contains(entity))
                _entitySet.AddObject(entity);

            _dataContext.ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Added);
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
            if (!_dataContext.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(item, out state))
                return false;
            return (state.State != EntityState.Detached);
        }
    }
}