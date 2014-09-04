using System.Linq;
using Hermes.Validation.Rules;
using NUnit.Framework;

namespace Hermes.Validation.Test
{
    [TestFixture]
    public class RuleCollectionTest
    {
        private static RuleCollection<string> CreateSut()
        {
            var sut = new RuleCollection<string>();
            sut.Add(new MaximumLengthRule(5));
            sut.Add(new MinimumLengthRule(4));
            return sut;
        }

        [Test]
        public void NumberOfRulesAdded()
        {
            var sut = CreateSut();
            Assert.AreEqual(2, sut.GetIRules().Count());
        }

        [Test]
        public void WhenTextIsValid1()
        {
            var sut = CreateSut();

            var actual = sut.CheckValid("12345");

            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenTextIsValid2()
        {
            var sut = CreateSut();

            var actual = sut.CheckValid("1234");

            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenTextIsInValid1()
        {
            var sut = CreateSut();

            var actual = sut.CheckValid("123456");

            Assert.IsFalse(actual);
        }

        [Test]
        public void WhenTextIsInValid2()
        {
            var sut = CreateSut();

            var actual = sut.CheckValid("123");

            Assert.IsFalse(actual);
        }
    }
}
