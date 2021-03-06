﻿namespace Hermes.Data.Repositories.Interfaces
{
    public interface IRepository<T>
        : IReadOnlyRepository<T>
        where T : class
    {
        //void Save(T entity);

        void Delete(T entity);

        void Insert(T entity);
    }
}
