using System.Data.Common;
using System.Data.Objects;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.EntityFramework
{
    public class EntityFrameworkRepositoryFactory : IRepositoryFactory
    {
        private ObjectContext _objectContext;

        public ObjectContext ObjectContext
        {
            get { return _objectContext; }
            set { _objectContext = value; }
        }

        public EntityFrameworkRepositoryFactory(ObjectContext objectContext)
        {
            _objectContext = objectContext;
        }

        public EntityFrameworkRepositoryFactory(string connectionString)
        {
            _objectContext = new ObjectContext(connectionString);
        }

        public IRepository<T> Create<T>() where T : class
        {
            return new EntityFrameworkRepository<T>(_objectContext);
        }
    }
}