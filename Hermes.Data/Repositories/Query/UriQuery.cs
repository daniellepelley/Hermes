using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Data
{
    /// <summary>
    /// A query which is added to a uri
    /// </summary>
    public class UriQuery : IQuery
    {
        #region Properties

        private string uriQueryText;
        /// <summary>
        /// The query string to be added to a uri
        /// </summary>
        public string UriQueryText
        {
            get { return uriQueryText; }
            set { uriQueryText = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A query which is added to a uri
        /// </summary>
        public UriQuery(string uriQueryText)
        {
            this.uriQueryText = uriQueryText;
        }

        #endregion
    }
}
