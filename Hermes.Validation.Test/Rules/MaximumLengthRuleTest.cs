using System.Collections.Generic;
using System.Linq;
using Hermes.Validation.Rules;
using Hermes.Validation.Rules.Preset.String;
using NUnit.Framework;

namespace Hermes.Validation.Test.Rules
{
    [TestFixture]
    public class MaximumLengthRuleTest
    {
        [Test]
        public void Valid()
        {
            var sut = new MaximumLengthRule(5);

            var actual = sut.CheckValid("valid");

            Assert.IsTrue(actual);
        }

        [Test]
        public void LengthIsZero()
        {
            var sut = new MaximumLengthRule(0);

            var actual = sut.CheckValid("valid");

            Assert.IsTrue(actual);
        }

        [Test]
        public void Invalid()
        {
            var sut = new MaximumLengthRule(5);

            var actual = sut.CheckValid("invalid");

            Assert.IsFalse(actual);
        }

        [Test]
        public void ValueIsNull()
        {
            var sut = new MaximumLengthRule(5);

            var actual = sut.CheckValid(null);

            Assert.IsTrue(actual);
        }

        [Test]
        public void ValueIsEmpty()
        {
            var sut = new MaximumLengthRule(5);

            var actual = sut.CheckValid(string.Empty);

            Assert.IsTrue(actual);
        }

        [Test]
        public void Sortable()
        {
            var list = new List<MaximumLengthRule>
            {
                new MaximumLengthRule(5),
                new MaximumLengthRule(3),
                new MaximumLengthRule(4)
            };

            var expected = list.OrderBy(x => x.Length);

            list.Sort();

            CollectionAssert.AreEqual(expected, list);
        }


    }
}
