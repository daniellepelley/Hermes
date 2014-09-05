namespace Hermes.Data.Repositories.Interfaces
{
    public interface IRepositoryFactory
    {
        IRepository<T> Create<T>() where T : class;
    }
}
