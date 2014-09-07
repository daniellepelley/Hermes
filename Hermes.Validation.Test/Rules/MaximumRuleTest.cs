using System.Collections.Generic;
using System.Linq;
using Hermes.Validation.Rules;
using Hermes.Validation.Rules.Preset.Numeric;
using NUnit.Framework;

namespace Hermes.Validation.Test.Rules
{
    [TestFixture]
    public class MaximumRuleTest
    {
        [Test]
        public void WhenTextIsValid1()
        {
            var sut = new MaximumRule(5);

            var actual = sut.CheckValid(5);

            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenTextIsValid2()
        {
            var sut = new MaximumRule(5);

            var actual = sut.CheckValid(1);

            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenTextIsInvalid()
        {
            var sut = new MaximumRule(5);

            var actual = sut.CheckValid(6);

            Assert.IsFalse(actual);
        }

        [Test]
        public void Sortable()
        {
            var list = new List<MaximumRule>
            {
                new MaximumRule(5),
                new MaximumRule(3),
                new MaximumRule(4)
            };

            var expected = list.OrderBy(x => x.ComparisonValue);

            list.Sort();

            CollectionAssert.AreEqual(expected, list);
        }
    }
}