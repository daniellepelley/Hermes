﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Newton.Data
//{
//    /// <summary>
//    /// A factory that creates a decorator around a repository that instantly persists on Save or Delete
//    /// </summary>
//    public class EntityFrameworkLoggableRepositoryFactory
//        : IRepositoryFactory
//    {
//        #region Properties

//        private IRepositoryFactory repositoryFactory;

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// A factory that creates a decorator around a repository that instantly persists on Save or Delete
//        /// </summary>
//        public EntityFrameworkLoggableRepositoryFactory(ObjectSetRepositoryFactory repositoryFactory)
//        {
//            this.repositoryFactory = repositoryFactory;
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Creates a repository that instantly persists on Save or Delete
//        /// </summary>
//        public IRepository<T> Insert<T>() where T : class
//        {
//            return new EntityFrameworkLoggableRepository<T>((ObjectSetRepository<T>)repositoryFactory.Insert<T>());
//        }

//        #endregion
//    }
//}
