using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.LinqToSql
{
    public class LinqToSqlRepositoryFactory : IRepositoryFactory
    {
        private LinqToSqlDataContext _dataContext;

        public LinqToSqlRepositoryFactory(System.Data.Linq.DataContext dataContext)
            : this(new LinqToSqlDataContext(dataContext))
        {
        }

        public LinqToSqlRepositoryFactory(LinqToSqlDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public LinqToSqlDataContext DataContext
        {
            get { return _dataContext; }
            set { _dataContext = value; }
        }

        public IRepository<T> Create<T>() where T : class
        {
            return new LinqToSqlRepository<T>(_dataContext);
        }
    }
}