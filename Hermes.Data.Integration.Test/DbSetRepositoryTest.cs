using System;
using System.Linq;
using Hermes.Data.EntityFramework;
using Hermes.Data.Repositories.Interfaces;
using NUnit.Framework;

namespace Hermes.Data.Integration.Test
{
    public class DbSetRepositoryTest
    {
        private readonly RepositoryTester<TestClass> _tester = new RepositoryTester<TestClass>();

        [SetUp]
        public void SetUpDatabase()
        {
            _tester.CreateEntity = () => new TestClass
            {
                Number = 1,
                Title = "One"
            };

            _tester.SetUpRepository = () =>
            {
                var dataContext = new DbDataContext(new TestDatabaseEntities());

                dataContext.DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE TestClass");

                var factory = new DbSetRepositoryFactory(dataContext);

                var repository = factory.Create<TestClass>();
                factory.DataContext.SaveChanges();
                return repository;
            };
        }

        [Test]
        public void Insert()
        {
            _tester.VerifyInsert();
        }

        [Test]
        public void Delete()
        {
            _tester.VerifyDelete();
        }
    }

    public class RepositoryTester<T>
        where T : class
    {
        public Func<T> CreateEntity { get; set; }

        public Func<IRepository<T>> SetUpRepository { get; set; } 

        public void VerifyInsert()
        {
            var repository = SetUpRepository();

            var newTestClass = CreateEntity();

            Assert.AreEqual(0, repository.Items.Count());

            repository.Insert(newTestClass);
            repository.DataContext.SaveChanges();

            Assert.AreEqual(1, repository.Items.Count());

            var actual = repository.Items.FirstOrDefault();
            Assert.AreEqual(newTestClass, actual);
        }

        public void VerifyDelete()
        {
            var repository = SetUpRepository();

            var newTestClass = CreateEntity();

            repository.Insert(newTestClass);
            repository.DataContext.SaveChanges();

            Assert.AreEqual(1, repository.Items.Count());

            repository.Delete(repository.Items.FirstOrDefault());
            repository.DataContext.SaveChanges();

            Assert.AreEqual(0, repository.Items.Count());
        }

    }
}
