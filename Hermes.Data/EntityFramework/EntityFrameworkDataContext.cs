using System.Data.Objects;
using System.Linq;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.EntityFramework
{
    public class EntityFrameworkDataContext : IDataContext
    {
        private ObjectContext _objectContext;

        public ObjectContext ObjectContext
        {
            get { return _objectContext; }
            set { _objectContext = value; }
        }

        public EntityFrameworkDataContext(ObjectContext objectContext)
        {
            _objectContext = objectContext;
        }

        public bool AreChanges()
        {
            return
                _objectContext.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added).Any() ||
                _objectContext.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Deleted).Any() ||
                _objectContext.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified).Any();
        }

        public void SaveChanges()
        {
            _objectContext.SaveChanges();
        }
    }
}
