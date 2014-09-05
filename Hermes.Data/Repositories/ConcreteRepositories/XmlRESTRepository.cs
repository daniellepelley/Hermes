//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Xml;
//using System.Xml.Serialization;
//using System.Net;

//namespace Newton.Data
//{
//    /// <summary>
//    /// A repository that uses Xml files saved to a directory
//    /// </summary>
//    /// <remarks>
//    /// In development
//    /// </remarks>
//    public class XmlRESTRepository<T> : IRepository<T> where T : class
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

//        private string requestUrl;

//        public string RequestUrl
//        {
//            get { return requestUrl; }
//            set { requestUrl = value; }
//        }

//        private string token;

//        public string Token
//        {
//            get { return token; }
//            set { token = value; }
//        }

//        private string xmlRootAttribute;

//        public string XmlRootAttribute
//        {
//            get { return xmlRootAttribute; }
//            set { xmlRootAttribute = value; }
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
//                WebRequest request = WebRequest.Create(requestUrl);
//                request.Method = "GET";
//                request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(token));

//                WebResponse response = request.GetResponse();
//                Stream dataStream = response.GetResponseStream();
//                return ((T[])new XmlSerializer(typeof(T[]), new XmlRootAttribute(xmlRootAttribute)).Deserialize(dataStream)).AsQueryable();
//            }
//        }

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// A repository that uses Xml files saved to a directory
//        /// </summary>
//        public XmlRESTRepository(string requestUrl, string token, string xmlRootAttribute)
//        {
//            this.requestUrl = requestUrl;
//            this.token = token;
//            this.xmlRootAttribute = xmlRootAttribute;
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Queries the base data using the passed query
//        /// </summary>
//        public IQueryable<T> Query(IQuery query)
//        {
//            string uriQuery = string.Empty;
//            if (query is UriQuery)
//            {
//                uriQuery = ((UriQuery)query).UriQueryText;
//            }

//            WebRequest request = WebRequest.Create(string.Format("{0}{1}", requestUrl, uriQuery));
//            request.Method = "GET";
//            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(token));

//            WebResponse response = request.GetResponse();
//            Stream dataStream = response.GetResponseStream();
//            return ((T[])new XmlSerializer(typeof(T[]), new XmlRootAttribute(xmlRootAttribute)).Deserialize(dataStream)).AsQueryable();
//        }

//        /// <summary>
//        /// Saves the entity to the base data
//        /// </summary>
//        public void Save(T entity)
//        {

//        }

//        /// <summary>
//        /// Deletes the entity from the base data
//        /// </summary>
//        public void Delete(T entity)
//        {

//        }

//        /// <summary>
//        /// Creates a new entity of type T in the base data
//        /// </summary>
//        public void Create(T entity)
//        {
//        }

//        #endregion
//    }
//}
