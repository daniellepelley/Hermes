using System.Data.Entity;
using Hermes.Data.EntityFramework;
using Hermes.Data.Repositories.Interfaces;
using MongoDB.Driver;

namespace Hermes.Data.MongoDb
{
    public class MongoDbRepositoryFactory : IRepositoryFactory
    {
        private MongoDbDataContext _dataContext;

        public MongoDbDataContext DataContext
        {
            get { return _dataContext; }
            set { _dataContext = value; }
        }

        public MongoDbRepositoryFactory(MongoDbDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public MongoDbRepositoryFactory(MongoDatabase mongoDatabase)
            :this(new MongoDbDataContext(mongoDatabase))
        { }

        public IRepository<T> Create<T>() where T : class
        {
            return new MongoDbRepository<T>(_dataContext);
        }
    }
}