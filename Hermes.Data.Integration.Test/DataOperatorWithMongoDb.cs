using System.Collections.Generic;
using System.Linq;
using Hermes.Data.MongoDb;
using Hermes.Data.Operation;
using Hermes.Data.Repositories.Interfaces;
using Hermes.Data.Test;
using MongoDB.Driver;
using NUnit.Framework;
using Hermes.Data.EntityFramework;

namespace Hermes.Data.Integration.Test
{
    [TestFixture]
    public class DataOperatorWithMongoDb
    {
        private IRepository<MongoTestClass> _repository;

        public IEnumerable<MongoTestClass> TestList()
        {
            return new[]
            {
                new MongoTestClass {Title = "One", Number = 1},
                new MongoTestClass {Title = "Two", Number = 2},
                new MongoTestClass {Title = "Three", Number = 3},
                new MongoTestClass {Title = "Four", Number = 4},
                new MongoTestClass {Title = "Five", Number = 5},
                new MongoTestClass {Title = "Six", Number = 6},
                new MongoTestClass {Title = "Seven", Number = 7},
                new MongoTestClass {Title = "Eight", Number = 8},
                new MongoTestClass {Title = "Nine", Number = 9},
                new MongoTestClass {Title = "Ten", Number = 10}
            };
        }
              
        [TestFixtureSetUp]
        public void SetUpDatabase()
        {
            var connectionString = "mongodb://user:password@ds035750.mongolab.com:35750/test";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("test");

            database.GetCollection<MongoTestClass>("MongoTestClass").Drop();

            var factory = new MongoDbRepositoryFactory(database);

            _repository = factory.Create<MongoTestClass>();

            foreach (var item in TestList())
            {
                _repository.Insert(item);
            }
        }

        [Test]
        public void SortedFilteredPaged()
        {
            var list = TestList().ToArray();
            
            var expected = list
                .Where(x => x.Number > 5)
                .OrderBy(x => x.Title)
                .Skip(3)
                .Take(3)
                .ToArray();

            var dataOperator = new DataOperatorBuilder()
                .AddFilter("Number", "gr", 5)
                .AddOrderBy("Title")
                .Paging(3, 2)
                .Build();

            var actual = GetData(dataOperator).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void SortedFilteredPagedFromJson()
        {
            var list = TestList().ToArray();

            var expected = list
                .Where(x => x.Number == 5)
                .OrderByDescending(x => x.Title)
                .Skip(3)
                .Take(3)
                .ToArray();

            const string json = @"{
                'Filters':[{'FilterProperty':'Number','FilterOperator':'eq','FilterValue':5}],
                'OrderBys':[{'SortProperty':'Title','SortDirection': 'DESC'}],
                'Pager':{'PageNumber':2,'NumberPerPage':3}}";
             
            var actual = GetData(json).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        public MongoTestClass[] GetData(string json)
        {
            var dataOperator = Newtonsoft.Json.JsonConvert.DeserializeObject<DataOperator>(json);
            
            return GetData(dataOperator);
        }

        public MongoTestClass[] GetData(DataOperator dataOperator)
        {
            return _repository.Items.GetData(dataOperator).ToArray();
        }
    }
}
