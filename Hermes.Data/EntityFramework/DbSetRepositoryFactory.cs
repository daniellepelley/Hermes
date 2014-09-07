using System.Data.Entity;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.EntityFramework
{
    public class DbSetRepositoryFactory : IRepositoryFactory
    {
        private DbDataContext _dataContext;

        public DbDataContext DataContext
        {
            get { return _dataContext; }
            set { _dataContext = value; }
        }

        public DbSetRepositoryFactory(DbDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public DbSetRepositoryFactory(DbContext dbContext)
        {
            _dataContext = new DbDataContext(dbContext);
        }

        public DbSetRepositoryFactory(string connectionString)
        {
            _dataContext = new DbDataContext(
                new DbContext(connectionString));
        }

        public IRepository<T> Create<T>() where T : class
        {
            return new DbSetRepository<T>(_dataContext);
        }
    }
}