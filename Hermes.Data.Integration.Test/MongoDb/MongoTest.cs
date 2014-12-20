using Hermes.Data.Integration.Test.MongoDb;
using Hermes.Data.MongoDb;
using MongoDB.Driver;
using NUnit.Framework;

namespace Hermes.Data.Integration.Test
{
    [TestFixture]
    public class MongoTest
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
                var connectionString = "mongodb://user:password@ds035750.mongolab.com:35750/test";
                var client = new MongoClient(connectionString);
                var server = client.GetServer();
                var database = server.GetDatabase("test");

                database.GetCollection<MongoTestClass>("MongoTestClass").Drop();

                //var dataContext = new MongoDbDataContext(database);

                var factory = new MongoDbRepositoryFactory(database);

                var repository = factory.Create<MongoTestClass>();
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

        //[Test]
        //public void Connection()
        //{
        //    var connectionString = "mongodb://user:password@ds035750.mongolab.com:35750/test";
        //    var client = new MongoClient(connectionString);
        //    var server = client.GetServer();
        //    var database = server.GetDatabase("test");

        //    var collection = database.GetCollection<MongoTestClass>("TestClass");

        //    collection.Insert(new MongoTestClass { Number = 1, Title = "rer" });
            
            

        //}


    }
}
