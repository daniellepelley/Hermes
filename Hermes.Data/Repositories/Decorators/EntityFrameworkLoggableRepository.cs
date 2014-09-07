//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Newton.Data
//{
//    /// <summary>
//    /// A decorator around a repository which injects logging
//    /// </summary>
//    public class EntityFrameworkLoggableRepository<T>
//        : IRepository<T> where T : class
//    {
//        #region Properties

//        private ObjectSetRepository<T> repository;

//        private EntityFrameworkChangeLogger logger;

//        /// <summary>
//        /// The data context that interacts with the data source
//        /// </summary>
//        public IDataContext DataContext
//        {
//            get { return repository.DataContext; }
//        }

//        /// <summary>
//        /// The collection of items of type T in the base data
//        /// </summary>
//        public IQueryable<T> Items
//        {
//            get { return repository.Items; }
//        }

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// A decorator around a repository which injects logging
//        /// </summary>
//        public EntityFrameworkLoggableRepository(ObjectSetRepository<T> repository)
//        {
//            this.repository = repository;
//            this.logger = new EntityFrameworkChangeLogger(((ObjectDataContext)this.repository.DataContext).DbContext);
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Queries the base data using the passed query
//        /// </summary>
//        public IQueryable<T> Query(IQuery query)
//        {
//            return repository.Query(query);
//        }

//        /// <summary>
//        /// Saves the entity to the base data
//        /// </summary>
//        public void Save(T entity)
//        {
//            repository.Save(entity);
//            var result = logger.CreateChangeLog(entity);
//        }

//        /// <summary>
//        /// Deletes the entity from the base data
//        /// </summary>
//        public void Delete(T entity)
//        {
//            repository.Delete(entity);
//            var result = logger.CreateChangeLog(entity);
//        }

//        /// <summary>
//        /// Creates a new entity of type T in the base data
//        /// </summary>
//        public void Create(T entity)
//        {
//            repository.Create(entity);
//            var result = logger.CreateChangeLog(entity);
//        }

//        #endregion
//    }
//}
