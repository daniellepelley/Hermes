using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.Data.InMemory;
using Hermes.Data.Integration.Test.MongoDb;
using Hermes.Data.MongoDb;
using Hermes.Data.Repositories.Interfaces;
using MongoDB.Driver;
using NUnit.Framework;

namespace Hermes.Data.Integration.Test
{
    [TestFixture]
    public class InMemoryTest
    {
        private readonly RepositoryTester<MongoTestClass> _tester = new RepositoryTester<MongoTestClass>();

        [SetUp]
        public void SetUpDatabase()
        {
            _tester.CreateEntity = () => new MongoTestClass
            {
                Number = 1,
                Title = "One"
            };

            _tester.SetUpRepository = () =>
            {
                var dataContext = new InMemoryDataContext<MongoTestClass>();

                var repository = new InMemoryRepository<MongoTestClass>(dataContext);
                dataContext.SaveChanges();
                return repository;
            };
        }

        [Test]
        public void Insert()
        {
            var repository = _tester.SetUpRepository();
            _tester.VerifyInsert(repository);
        }

        [Test]
        public void Delete()
        {
            var repository = _tester.SetUpRepository();
            _tester.VerifyDelete(repository);
        }
    }
}