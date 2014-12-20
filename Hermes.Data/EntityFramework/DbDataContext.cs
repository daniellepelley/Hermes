using System.Data.Entity;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.EntityFramework
{
    public class DbDataContext : IDataContext
    {
        private DbContext _dbContext;

        public DbContext DbContext
        {
            get { return _dbContext; }
            set { _dbContext = value; }
        }

        public DbDataContext(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AreChanges()
        {
            return _dbContext.ChangeTracker.HasChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
