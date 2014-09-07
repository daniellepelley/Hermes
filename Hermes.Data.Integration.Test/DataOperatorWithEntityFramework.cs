using System.Collections.Generic;
using System.Linq;
using Hermes.Data.Operation;
using Hermes.Data.Test;
using NUnit.Framework;
using Hermes.Data.EntityFramework;

namespace Hermes.Data.Integration.Test
{
    [TestFixture]
    public class DataOperatorWithEntityFramework
    {
        public IEnumerable<TestClass> TestList()
        {
            return new[]
            {
                new TestClass {Title = "One", Number = 1},
                new TestClass {Title = "Two", Number = 2},
                new TestClass {Title = "Three", Number = 3},
                new TestClass {Title = "Four", Number = 4},
                new TestClass {Title = "Five", Number = 5},
                new TestClass {Title = "Six", Number = 6},
                new TestClass {Title = "Seven", Number = 7},
                new TestClass {Title = "Eight", Number = 8},
                new TestClass {Title = "Nine", Number = 9},
                new TestClass {Title = "Ten", Number = 10}
            };
        }
              
        [TestFixtureSetUp]
        public void SetUpDatabase()
        {
            var dataContext = new DbDataContext(new TestDatabaseEntities());

            dataContext.DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE TestClass");

            var factory = new DbSetRepositoryFactory(dataContext);

            var repository = factory.Create<TestClass>();

            foreach (var item in TestList())
            {
                repository.Insert(item);
            }

            factory.DataContext.SaveChanges();
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

        public TestClass[] GetData(string json)
        {
            var dataOperator = Newtonsoft.Json.JsonConvert.DeserializeObject<DataOperator>(json);
            
            return GetData(dataOperator);
        }

        public TestClass[] GetData(DataOperator dataOperator)
        {
            var repository = new DbSetRepository<TestClass>(new TestDatabaseEntities());
            return repository.Items.GetData(dataOperator).ToArray();
        }
    }
}
