using System.Data.Entity.Core.Objects;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.EntityFramework
{
    public class ObjectSetRepositoryFactory : IRepositoryFactory
    {
        private ObjectContext _objectContext;

        public ObjectContext ObjectContext
        {
            get { return _objectContext; }
            set { _objectContext = value; }
        }

        public ObjectSetRepositoryFactory(ObjectContext objectContext)
        {
            _objectContext = objectContext;
        }

        public ObjectSetRepositoryFactory(string connectionString)
        {
            _objectContext = new ObjectContext(connectionString);
        }

        public IRepository<T> Create<T>() where T : class
        {
            return new ObjectSetRepository<T>(_objectContext);
        }
    }
}