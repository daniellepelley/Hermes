//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Xml;
//using System.Xml.Serialization;

//namespace Newton.Data
//{
//    /// <summary>
//    /// A repository that uses Xml files saved to a directory
//    /// </summary>
//    public class XmlRepository<T> : IRepository<T> where T : class
//    {
//        #region Properties

//        private XmlSerializer<T> serializer = new XmlSerializer<T>();
//        /// <summary>
//        /// A serializer that converts entities to and from Xml
//        /// </summary>
//        public XmlSerializer<T> Serializer
//        {
//            get { return serializer; }
//            set { serializer = value; }
//        }

//        private Func<T, string> getFileName;
//        /// <summary>
//        /// A function to get a filename from an entity
//        /// </summary>
//        public Func<T, string> GetFileName
//        {
//            get { return getFileName; }
//            set { getFileName = value; }
//        }

//        private XmlDataContext xmlDataContext;
//        /// <summary>
//        /// The data context that interacts with the data source
//        /// </summary>
//        public IDataContext DataContext
//        {
//            get { return xmlDataContext; }
//        }

//        /// <summary>
//        /// The collection of items of type T in the base data
//        /// </summary>
//        public IQueryable<T> Items
//        {
//            get
//            {
//                if (System.IO.Directory.Exists(xmlDataContext.DirectoryPath))
//                {
//                    List<T> output = new List<T>();

//                    foreach (string fileName in System.IO.Directory.GetFiles(xmlDataContext.DirectoryPath))
//                    {
//                        string xml = System.IO.File.ReadAllText(fileName);

//                        T entity = serializer.DeSerializeObject(xml);
//                        output.Add(entity);
//                    }
//                    return output.AsQueryable();
//                }
//                return new T[0].AsQueryable();
//            }
//        }

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// A repository that uses Xml files saved to a directory
//        /// </summary>
//        public XmlRepository(string directoryPath, Func<T, string> getFileName)
//        {
//            this.xmlDataContext = new XmlDataContext(directoryPath);
//            this.getFileName = getFileName;
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Queries the base data using the passed query
//        /// </summary>
//        public IQueryable<T> Query(IQuery query)
//        {
//            return Items;
//        }

//        /// <summary>
//        /// Saves the entity to the base data
//        /// </summary>
//        public void Save(T entity)
//        {
//            string xml = serializer.SerializeObject(entity);
//            string fileName = string.Format("{0}/{1}", entity.GetType().Name, getFileName(entity));

//            if (xmlDataContext.SaveWork.ContainsKey(fileName))
//            {
//                xmlDataContext.SaveWork[fileName] = xml;
//            }
//            else
//            {
//                xmlDataContext.SaveWork.Add(getFileName(entity), xml);
//            }
//        }

//        /// <summary>
//        /// Deletes the entity from the base data
//        /// </summary>
//        public void Delete(T entity)
//        {
//            string xml = serializer.SerializeObject(entity);
//            string fileName = getFileName(entity);

//            if (xmlDataContext.DeleteWork.ContainsKey(fileName))
//            {
//                xmlDataContext.DeleteWork[fileName] = xml;
//            }
//            else
//            {
//                xmlDataContext.DeleteWork.Add(getFileName(entity), xml);
//            }
//        }

//        /// <summary>
//        /// Creates a new entity of type T in the base data
//        /// </summary>
//        public void Create(T entity)
//        {
//            string xml = serializer.SerializeObject(entity);
//            string fileName = getFileName(entity);

//            if (xmlDataContext.SaveWork.ContainsKey(fileName))
//            {
//                xmlDataContext.SaveWork[fileName] = xml;
//            }
//            else
//            {
//                xmlDataContext.SaveWork.Add(getFileName(entity), xml);
//            }
//        }

//        #endregion
//    }
//}
