using System.Linq;
using Hermes.Data.Operation;
using Hermes.Data.Test.Helpers;
using NUnit.Framework;

namespace Hermes.Data.Test
{
    [TestFixture]
    public class DataOperatorTest
    {
        [Test]
        public void Orderby()
        {
            var list = TestClass.ConstructList();

            var expected = list.OrderBy(x => x.Title).ToArray();

            var dataOperator = new DataOperatorBuilder()
                .AddOrderBy("Title")
                .Build();
            
            var actual = list.AsQueryable().GetData(dataOperator).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderByDescending()
        {
            var list = TestClass.ConstructList();

            var expected = list.OrderByDescending(x => x.Title).ToArray();

            var dataOperator = new DataOperatorBuilder()
                .AddOrderBy("Title", true)
                .Build();

            var actual = list.AsQueryable().GetData(dataOperator).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderbyThenBy()
        {
            var list = TestClass.ConstructList2();

            var expected = list
                .OrderBy(x => x.Number)
                .ThenByDescending(x => x.Title)
                .ToArray();

            var dataOperator = new DataOperatorBuilder()
                .AddOrderBy("Number")
                .AddOrderBy("Title", true)
                .Build();

            var actual = list.AsQueryable().GetData(dataOperator).ToArray();
            
            CollectionAssert.AreEqual(expected, actual);
        }


        [Test]
        public void FilterEquals()
        {
            var list = TestClass.ConstructList();

            var expected = list.Where(x => x.Number == 5).ToArray();

            var dataOperator = new DataOperatorBuilder()
                .AddFilter("Number", "eq", 5)
                .Build();

            var actual = list.AsQueryable().GetData(dataOperator).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void FilterGreaterThan()
        {
            var list = TestClass.ConstructList();

            var expected = list.Where(x => x.Number > 5).ToArray();

            var dataOperator = new DataOperatorBuilder()
                .AddFilter("Number", "gr", 5)
                .Build();

            var actual = list.AsQueryable().GetData(dataOperator).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void PagingFirstPage()
        {
            var list = TestClass.ConstructList();

            var expected = list.Take(2).ToArray();

            var dataOperator = new DataOperatorBuilder()
                .Paging(2, 0)
                .Build();

            var actual = list.AsQueryable().GetData(dataOperator).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void PagingSecondPageWith10()
        {
            var list = TestClass.ConstructList();

            var expected = list.Skip(2).Take(2).ToArray();

            var dataOperator = new DataOperatorBuilder()
                .Paging(2, 2)
                .Build();

            var actual = list.AsQueryable().GetData(dataOperator).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void PagingSecondPageWith5()
        {
            var list = TestClass.ConstructList().Take(5).ToArray();

            var expected = list.Skip(3).Take(3).ToArray();

            var dataOperator = new DataOperatorBuilder()
                .Paging(3, 2)
                .Build();

            var actual = list.AsQueryable().GetData(dataOperator).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void SortedFilteredPaged()
        {
            var list = TestClass.ConstructList();
            
            var expected = list
                .Where(x => x.Number > 5)
                .OrderBy(x => x.Title)
                .Skip(3).Take(3);

            var dataOperator = new DataOperatorBuilder()
                .AddFilter("Number", "gr", 5)    
                .AddOrderBy("Title")
                .Paging(3, 2)
                .Build();

            var actual = list.AsQueryable().GetData(dataOperator).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }


    }
}
