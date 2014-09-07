using Hermes.Data.EntityFramework;
using NUnit.Framework;

namespace Hermes.Data.Integration.Test.EntityFramework
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
