using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Data
{
    /// <summary>
    /// A query which consists of a collection of parameters
    /// </summary>
    public class ParametersQuery : IQuery
    {
        #region Properties

        private Dictionary<string, string> parameters = new Dictionary<string, string>();
        /// <summary>
        /// The query string to be added to a uri
        /// </summary>
        public Dictionary<string, string> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A query which is added to a uri
        /// </summary>
        public ParametersQuery()
        { }

        /// <summary>
        /// A query which is added to a uri
        /// </summary>
        public ParametersQuery(Dictionary<string, string> parameters)
        {
            this.parameters = parameters;
        }

        #endregion
    }
}
