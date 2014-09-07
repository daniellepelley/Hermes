using System;
using System.Linq;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data.Repositories.Decorators
{
    public class SoftDeleteRepository<T>
        : IRepository<T> where T : class
    {
        private readonly IRepository<T> _repository;

        private readonly System.Linq.Expressions.Expression<Func<T, bool>> _isLiveFilter;

        private readonly System.Linq.Expressions.Expression<Action<T>> _markAsDeleted;

        public IDataContext DataContext
        {
            get { return _repository.DataContext; }
        }

        public IQueryable<T> Items
        {
            get
            {
                return _repository.Items.Where(_isLiveFilter);
            }
        }

        public SoftDeleteRepository(IRepository<T> repository, System.Linq.Expressions.Expression<Func<T, bool>> isLiveFilter, System.Linq.Expressions.Expression<Action<T>> markAsDeleted)
        {
            _repository = repository;
            _isLiveFilter = isLiveFilter;
            _markAsDeleted = markAsDeleted;
        }

        //public void Save(T entity)
        //{
        //    _repository.Save(entity);
        //}

        public void Delete(T entity)
        {
            _markAsDeleted.Compile().Invoke(entity);
            //_repository.Save(entity);
        }

        public void Insert(T entity)
        {
            _repository.Insert(entity);
        }
    }
}
