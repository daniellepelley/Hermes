using System.Data;
using System.Data.Objects;
using System.Linq;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.EntityFramework
{
    /// <summary>
    ///     A repository that uses entity framework to comunicate with a database
    /// </summary>
    public class EntityFrameworkRepository<T> : IRepository<T> where T : class
    {
        #region Properties

        private readonly EntityFrameworkDataContext _dataContext;
        private readonly ObjectSet<T> _entitySet;

        /// <summary>
        ///     The data context that interacts with the data source
        /// </summary>
        public IDataContext DataContext
        {
            get { return _dataContext; }
        }

        /// <summary>
        ///     The collection of items of type T in the base data
        /// </summary>
        public IQueryable<T> Items
        {
            get { return _dataContext.ObjectContext.CreateObjectSet<T>(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     A repository that uses entity framework to comunicate with a database
        /// </summary>
        public EntityFrameworkRepository(ObjectContext objectContext)
            : this(new EntityFrameworkDataContext(objectContext))
        {
        }

        /// <summary>
        ///     A repository that uses entity framework to comunicate with a database
        /// </summary>
        public EntityFrameworkRepository(EntityFrameworkDataContext dataContext)
        {
            _dataContext = dataContext;
            _entitySet = _dataContext.ObjectContext.CreateObjectSet<T>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Saves the entity to the base data
        /// </summary>
        public void Save(T entity)
        {
            if (!Contains(entity))
                _entitySet.Attach(entity);

            _dataContext.ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }

        /// <summary>
        ///     Deletes the entity from the base data
        /// </summary>
        public void Delete(T entity)
        {
            if (!Contains(entity))
                _entitySet.Attach(entity);

            _dataContext.ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Deleted);
        }

        /// <summary>
        ///     Creates a new entity of type T in the base data
        /// </summary>
        public void Create(T entity)
        {
            if (!Contains(entity))
                _entitySet.AddObject(entity);

            _dataContext.ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Added);
        }

        /// <summary>
        ///     Queries the base data using the passed query
        /// </summary>
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

        #endregion
    }
}