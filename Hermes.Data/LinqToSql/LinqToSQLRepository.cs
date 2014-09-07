using System.Data.Linq;
using System.Linq;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.LinqToSql
{
    public class LinqToSqlRepository<T> : IRepository<T>
        where T : class
    {
        private readonly LinqToSqlDataContext _dataContext;

        public IDataContext DataContext
        {
            get { return _dataContext; }
        }

        public IQueryable<T> Items
        {
            get { return _dataContext.DataContext.GetTable<T>().AsQueryable(); }
        }

        public LinqToSqlRepository(DataContext dataContext)
            : this(new LinqToSqlDataContext(dataContext))
        {
        }

        public LinqToSqlRepository(LinqToSqlDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Save(T entity)
        {
            _dataContext.DataContext.GetTable<T>().Attach(entity);
            _dataContext.DataContext.Refresh(RefreshMode.KeepCurrentValues, entity);
        }

        public void Delete(T entity)
        {
            _dataContext.DataContext.GetTable<T>().DeleteOnSubmit(entity);
        }

        public void Insert(T entity)
        {
            _dataContext.DataContext.GetTable<T>().InsertOnSubmit(entity);
        }
    }
}