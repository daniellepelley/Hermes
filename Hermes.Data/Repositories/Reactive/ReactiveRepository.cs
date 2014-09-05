using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.Repositories.Reactive
{
    public class ReactiveRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public IDataContext DataContext
        {
            get { return _repository.DataContext; }
        }

        public IObservable<T> Items
        {
            get { return itemsEnumerable.ToObservable(); }
        }

        private IEnumerable<T> itemsEnumerable
        {
            get
            {
                foreach (var item in _repository.Items)
                {
                    System.Threading.Thread.Sleep(1000);
                    yield return item;
                }
            }
        }

        public ReactiveRepository(IRepository<T> repository)
        {
            this._repository = repository;
        }

        public async Task SaveAsync(T entity)
        {
            await Task.Factory.StartNew(() => _repository.Save(entity));
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Factory.StartNew(() => _repository.Delete(entity));
        }

        public async Task CreateAsync(T entity)
        {
            await Task.Factory.StartNew(() => _repository.Create(entity));
        }
    }
}
