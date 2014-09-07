using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Hermes.Data.EntityFramework;
using Hermes.Data.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Hermes.Data.MongoDb
{
    public class MongoDbRepository<T> : IRepository<T> where T : class
    {
        private readonly MongoDbDataContext _dataContext;
        private readonly MongoCollection<T> _collection;

        private Expression<Func<T, string>> expression;

        public IDataContext DataContext
        {
            get { return _dataContext; }
        }

        public IQueryable<T> Items
        {
            get { return _collection.AsQueryable(); }
        }

        public MongoDbRepository(MongoDatabase mongoDatabase)
            : this(new MongoDbDataContext(mongoDatabase))
        {
        }

        public MongoDbRepository(MongoDbDataContext dataContext)
        {
            _dataContext = dataContext;
            _collection = _dataContext.MongoDatabase.GetCollection<T>(typeof(T).Name);
            expression = GetPropGetter();
        }

        public void Delete(T entity)
        {
            var query = Query<T>.EQ(expression, expression.Compile()(entity));
            _collection.Remove(query);
        }

        private static Expression<Func<T, string>> GetPropGetter()
        {
            var paramExpression = Expression.Parameter(typeof(T), "value");

            var propertyGetterExpression = Expression.Property(paramExpression, "_id");

            var result =
                Expression.Lambda<Func<T, string>>(propertyGetterExpression, paramExpression);

            return result;
        }

        public void Insert(T entity)
        {
            _collection.Insert(entity);
        }
    }
}