using System.Collections.Generic;

namespace Hermes.Data.Test.Helpers
{
    public class TestClass
    {
        public string Title { get; set; }
        public int Number { get; set; }

        public static List<TestClass> ConstructList()
        {
            return new List<TestClass>
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

        public static List<TestClass> ConstructList2()
        {
            return new List<TestClass>
            {
                new TestClass {Title = "a", Number = 1},
                new TestClass {Title = "a", Number = 2},
                new TestClass {Title = "a", Number = 3},
                new TestClass {Title = "a", Number = 1},
                new TestClass {Title = "a", Number = 2},
                new TestClass {Title = "b", Number = 3},
                new TestClass {Title = "b", Number = 1},
                new TestClass {Title = "b", Number = 2},
                new TestClass {Title = "b", Number = 3},
                new TestClass {Title = "b", Number = 1}
            };
        }

        public static TestClass CreateNew()
        {
            return new TestClass {Title = "Eleven", Number = 11};
        }
    } 

}

