//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Newton.Data
//{
//    /// <summary>
//    /// A factory that creates a decorator around a repository that provides security
//    /// </summary>
//    public class SecureRepositoryFactory : IRepositoryFactory
//    {
//        #region Properties

//        private IRepositoryFactory repositoryFactory;

//        private IDataSecurityProvider dataSecurityProvider;

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// A factory that creates a decorator around a repository that provides security
//        /// </summary>
//        public SecureRepositoryFactory(IRepositoryFactory repositoryFactory, IDataSecurityProvider dataSecurityProvider)
//        {
//            this.repositoryFactory = repositoryFactory;
//            this.dataSecurityProvider = dataSecurityProvider;
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Creates a repository that provides security
//        /// </summary>
//        public IRepository<T> Create<T>() where T : class
//        {
//            return new SecureRepository<T>(repositoryFactory.Create<T>(), dataSecurityProvider);
//        }

//        #endregion
//    }
//}
