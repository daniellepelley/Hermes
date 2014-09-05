using System.Data.Linq;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.LinqToSql
{
    public class LinqToSqlDataContext : IDataContext
    {
        private System.Data.Linq.DataContext _dataContext;

        public LinqToSqlDataContext(System.Data.Linq.DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public System.Data.Linq.DataContext DataContext
        {
            get { return _dataContext; }
            set { _dataContext = value; }
        }

        public bool AreChanges()
        {
            ChangeSet changeSet = _dataContext.GetChangeSet();
            return
                changeSet.Deletes.Count > 0 ||
                changeSet.Inserts.Count > 0 ||
                changeSet.Updates.Count > 0;
        }

        public void SaveChanges()
        {
            _dataContext.SubmitChanges();
        }
    }
}