using Hermes.Data;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.Repositories.ConcreteFactories
{
    public class EmptyRepositoryFactory : IRepositoryFactory
    {
        #region Methods

        public IRepository<T> Create<T>() where T : class
        {
            return new EmptyRepository<T>();
        }

        #endregion
    }
}