//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Data.Linq;
//using System.Data.Objects;

//using Newton.Data;

//namespace Newton.Data
//{
//    /// <summary>
//    /// A factory which produces Entity Framework repositories
//    /// </summary>
//    public class XmRepositoryFactory : IRepositoryFactory
//    {
//        #region Properties

//        private string directoryPath;
//        /// <summary>
//        /// The path of the directory to store the Xml files
//        /// </summary>
//        public string DirectoryPath
//        {
//            get { return directoryPath; }
//            set { directoryPath = value; }
//        }

//        private Dictionary<Type, Delegate> getFileNameFuncs = new Dictionary<Type, Delegate>();
//        /// <summary>
//        /// Collections of mapping between type and the file name for the XML file
//        /// </summary>
//        public Dictionary<Type, Delegate> GetFileNameFuncs
//        {
//            get { return getFileNameFuncs; }
//            set { getFileNameFuncs = value; }
//        }

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// A factory which produces Entity Framework repositories
//        /// </summary>
//        public XmRepositoryFactory(string directoryPath)
//        {
//            this.directoryPath = directoryPath;
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Creates a ObjectSetRepository for type T
//        /// </summary>
//        public IRepository<T> Create<T>() where T : class
//        {
//            if (getFileNameFuncs.ContainsKey(typeof(T)))
//                return new XmlRepository<T>(directoryPath, (Func<T, string>)getFileNameFuncs[typeof(T)]);
//            return null;
//        }

//        #endregion
//    }
//}