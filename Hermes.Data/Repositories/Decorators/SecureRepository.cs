//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Newton.Data
//{
//    /// <summary>
//    /// A decorator around a repository that makes a security check before each transaction
//    /// </summary>
//    public class SecureRepository<T>
//        : IRepository<T> where T : class
//    {
//        #region Properties

//        private IRepository<T> repository;

//        private IDataSecurityProvider dataSecurityProvider;

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
//            get
//            {
//                if (dataSecurityProvider.IsReadPermitted<T>())
//                {
//                    return repository.Items;
//                }
//                return new List<T>().AsQueryable();
//            }
//        }

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// A decorator around a repository that makes a security check before each transaction
//        /// </summary>
//        public SecureRepository(IRepository<T> repository, IDataSecurityProvider dataSecurityProvider)
//        {
//            this.repository = repository;
//            this.dataSecurityProvider = dataSecurityProvider;
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Saves the entity to the base data
//        /// </summary>
//        public void Save(T entity)
//        {
//            if (dataSecurityProvider.IsSavePermitted<T>())
//            {
//                repository.Save(entity);
//            }
//        }

//        /// <summary>
//        /// Deletes the entity from the base data
//        /// </summary>
//        public void Delete(T entity)
//        {
//            if (dataSecurityProvider.IsDeletePermitted<T>())
//            {
//                repository.Delete(entity);
//            }
//        }

//        /// <summary>
//        /// Creates a new entity of type T in the base data
//        /// </summary>
//        public void Insert(T entity)
//        {
//            if (dataSecurityProvider.IsCreatePermitted<T>())
//            {
//                repository.Insert(entity);
//            }
//        }

//        #endregion
//    }
//}
