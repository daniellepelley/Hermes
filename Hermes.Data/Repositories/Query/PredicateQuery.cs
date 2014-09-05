using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Data
{
    /// <summary>
    /// A query containing a predicate based on type T
    /// </summary>
    public class PredicateQuery<T> : IQuery<T> where T : class
    {
        private Func<T, bool> predicate = (t) => true;
        /// <summary>
        /// A method that takes type T and returns a boolean
        /// </summary>
        public Func<T, bool> Predicate
        {
            get { return predicate; }
            set { predicate = value; }
        }
    }
}
