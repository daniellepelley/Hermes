using System.Linq;

namespace Hermes.Data.Repositories.Interfaces
{
    public interface IReadOnlyRepository<T> where T : class
    {
        IDataContext DataContext { get; }

        IQueryable<T> Items { get; }
    }
}
