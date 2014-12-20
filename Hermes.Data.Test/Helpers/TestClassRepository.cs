using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.Test.Helpers
{
    public class TestClassRepository : IRepository<TestClass>
    {
        private readonly List<TestClass> _list;

        public IDataContext DataContext { get; private set; }

        public IQueryable<TestClass> Items
        {
            get
            {
                return _list.AsQueryable();
            }
        }

        public TestClassRepository()
        {
            _list = TestClass.ConstructList();
        }
        
        public void Delete(TestClass entity)
        {
            Task.Delay(100).ContinueWith(
                t => _list.Remove(entity)
                ).Wait();
        }

        public void Insert(TestClass entity)
        {
            Task.Delay(100).ContinueWith(
                t => _list.Add(entity)
            ).Wait();
        }
    }
}
