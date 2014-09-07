using System.Data.Entity.Core.Objects;
using System.Linq;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.EntityFramework
{
    public class ObjectDataContext : IDataContext
    {
        private ObjectContext _objectContext;

        public ObjectContext ObjectContext
        {
            get { return _objectContext; }
            set { _objectContext = value; }
        }

        public ObjectDataContext(ObjectContext objectContext)
        {
            _objectContext = objectContext;
        }

        public bool AreChanges()
        {
            return
                _objectContext.ObjectStateManager.GetObjectStateEntries(System.Data.Entity.EntityState.Added).Any() ||
                _objectContext.ObjectStateManager.GetObjectStateEntries(System.Data.Entity.EntityState.Deleted).Any() ||
                _objectContext.ObjectStateManager.GetObjectStateEntries(System.Data.Entity.EntityState.Modified).Any();
        }

        public void SaveChanges()
        {
            _objectContext.SaveChanges();
        }
    }
}
