using Hermes.Data.Repositories.Decorators;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.Repositories.DecoratorFactories
{
    public class InstantPersistanceRepositoryFactory
        : IRepositoryFactory
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public InstantPersistanceRepositoryFactory(IRepositoryFactory repositoryFactory)
        {
            this._repositoryFactory = repositoryFactory;
        }

        public IRepository<T> Create<T>() where T : class
        {
            return new InstantPersistanceRepository<T>(_repositoryFactory.Create<T>());
        }
    }
}
