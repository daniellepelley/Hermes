using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.Data.Operation;
using Hermes.Data.Test;
using NUnit.Framework;

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
            var entities = new TestDatabaseEntities();
            entities.Database.ExecuteSqlCommand("TRUNCATE TABLE TestClass");
            entities.TestClasses.AddRange(TestList());
            entities.SaveChanges();
        }

        [Test]
        public void SortedFilteredPaged()
        {
            var entities = new TestDatabaseEntities();
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

            var actual = entities.TestClasses.GetData(dataOperator).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
