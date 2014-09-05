using System.Collections.Generic;
using System.Linq;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data
{
    /// <summary>
    ///     A repository factories that can contain inner repository factories
    /// </summary>
    public class MultiRepositoryFactory : IRepositoryFactory
    {
        private readonly List<IRepositoryFactory> _repositoryFactories = new List<IRepositoryFactory>();

        public List<IRepositoryFactory> RepositoryFactories
        {
            get { return _repositoryFactories; }
        }

        public IRepository<T> Create<T>() where T : class
        {
            return _repositoryFactories
                .Select(repositoryFactory => repositoryFactory.Create<T>())
                .FirstOrDefault(repository => repository != null);
        }
    }
}