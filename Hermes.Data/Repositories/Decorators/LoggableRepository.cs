//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Newton.Data
//{
//    /// <summary>
//    /// A decorator around a repository which injects logging
//    /// </summary>
//    public class LoggableRepository<T>
//        : RepositoryDecoratorBase<T> where T : class
//    {
//        #region Properties

//        private ILogger<T> logger;

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// A decorator around a repository which injects logging
//        /// </summary>
//        public LoggableRepository(IRepository<T> repository, ILogger<T> logger)
//            : base(repository)
//        {
//            this.logger = logger;
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Saves the entity to the base data
//        /// </summary>
//        public override void Save(T entity)
//        {
//            logger.Log(entity);
//            repository.Save(entity);
//        }

//        /// <summary>
//        /// Deletes the entity from the base data
//        /// </summary>
//        public override void Delete(T entity)
//        {
//            logger.Log(entity);
//            repository.Delete(entity);
//        }

//        /// <summary>
//        /// Creates a new entity of type T in the base data
//        /// </summary>
//        public override void Create(T entity)
//        {
//            logger.Log(entity);
//            repository.Create(entity);
//        }

//        #endregion
//    }
//}
