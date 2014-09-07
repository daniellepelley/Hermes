using System.Data.Entity;
using Hermes.Data.Repositories.Interfaces;
using MongoDB.Driver;

namespace Hermes.Data.MongoDb
{
    public class MongoDbDataContext : IDataContext
    {
        private MongoDatabase _mongoDatabase;

        public MongoDatabase MongoDatabase
        {
            get { return _mongoDatabase; }
            set { _mongoDatabase = value; }
        }

        public MongoDbDataContext(MongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public bool AreChanges()
        {
            return false;
        }

        public void SaveChanges()
        {
            //_mongoDatabase.SaveChanges();
        }
    }
}
