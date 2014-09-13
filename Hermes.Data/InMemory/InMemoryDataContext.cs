using System.Collections.Generic;
using System.Linq;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.InMemory
{
    public class InMemoryDataContext<T> : IDataContext
    {
        private readonly List<T> _innerList = new List<T>();
        
        private readonly List<T> _list = new List<T>();

        public List<T> List
        {
            get { return _list; }
        }

        public InMemoryDataContext()
        {
            _innerList = new List<T>();
            _list = new List<T>();
        }

        public bool AreChanges()
        {
            return !_innerList.Except(_list).Any();
        }

        public void SaveChanges()
        {
            _innerList.Clear();
            _innerList.AddRange(_list);
        }
    }
}
