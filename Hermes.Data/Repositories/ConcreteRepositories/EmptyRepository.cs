using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.Data.Repositories.Interfaces;

namespace Hermes.Data
{
    public class EmptyRepository<T> : IRepository<T> where T : class
    {
        #region Properties

        public IDataContext DataContext
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryable<T> Items
        {
            get { return new List<T>().AsQueryable(); }
        }

        #endregion

        #region Methods

        public void Save(T entity)
        { }

        public void Delete(T entity)
        { }

        public void Insert(T entity)
        { }

        #endregion
    }
}
