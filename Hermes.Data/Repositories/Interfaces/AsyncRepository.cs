using System;
using System.Threading.Tasks;

namespace Hermes.Data.Repositories.Interfaces
{
    public interface IAsyncRepository<T>
        where T : class
    {
        IDataContext DataContext { get; }

        IObservable<T> Items { get; }

        Task SaveAsync(T entity);

        Task DeleteAsync(T entity);

        Task CreateAsync(T entity);
    }
}
