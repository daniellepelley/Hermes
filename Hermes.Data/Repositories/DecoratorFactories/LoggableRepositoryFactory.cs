//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Newton.Data
//{
//    /// <summary>
//    /// A factory that creates a decorator around a repository that logs changes
//    /// </summary>
//    public class LoggableRepositoryFactory : IRepositoryFactory
//    {
//        #region Properties

//        private IRepositoryFactory repositoryFactory;

//        private ILoggerFactory loggerFactory;

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// A factory that creates a decorator around a repository that logs changes
//        /// </summary>
//        public LoggableRepositoryFactory(IRepositoryFactory repositoryFactory, ILoggerFactory loggerFactory)
//        {
//            this.repositoryFactory = repositoryFactory;
//            this.loggerFactory = loggerFactory;
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Creates a repository that logs changes
//        /// </summary>
//        public IRepository<T> Insert<T>() where T : class
//        {
//            return new LoggableRepository<T>(repositoryFactory.Insert<T>(), loggerFactory.Insert<T>());
//        }

//        #endregion
//    }
//}
