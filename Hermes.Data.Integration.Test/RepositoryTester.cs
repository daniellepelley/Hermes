using System;
using System.Linq;
using Hermes.Data.Repositories.Interfaces;
using NUnit.Framework;

namespace Hermes.Data.Integration.Test
{
    public class RepositoryTester<T>
        where T : class
    {
        public Func<T> CreateEntity { get; set; }

        public Func<IRepository<T>> SetUpRepository { get; set; }

        public void VerifyInsert(IRepository<T> repository)
        {
            var newTestClass = CreateEntity();

            Assert.AreEqual(0, repository.Items.Count());

            repository.Insert(newTestClass);
            repository.DataContext.SaveChanges();

            Assert.AreEqual(1, repository.Items.Count());

            var actual = repository.Items.FirstOrDefault();
            Assert.AreEqual(newTestClass, actual);
        }

        public void VerifyDelete(IRepository<T> repository)
        {
            var newTestClass = CreateEntity();

            repository.Insert(newTestClass);
            repository.DataContext.SaveChanges();

            Assert.AreEqual(1, repository.Items.Count());

            var itemToDelete = repository.Items.FirstOrDefault();

            Assert.IsNotNull(itemToDelete);

            repository.Delete(itemToDelete);
            repository.DataContext.SaveChanges();

            Assert.AreEqual(0, repository.Items.Count());
        }
    }
}