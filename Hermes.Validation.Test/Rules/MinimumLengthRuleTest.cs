using System.Collections.Generic;
using System.Linq;
using Hermes.Validation.Rules;
using Hermes.Validation.Rules.Preset.String;
using NUnit.Framework;

namespace Hermes.Validation.Test.Rules
{
    [TestFixture]
    public class MinimumLengthRuleTest
    {
        [Test]
        public void Valid()
        {
            var sut = new MinimumLengthRule(5);

            var actual = sut.CheckValid("valid");

            Assert.IsTrue(actual);
        }

        [Test]
        public void Invalid()
        {
            var sut = new MinimumLengthRule(5);

            var actual = sut.CheckValid("tiny");

            Assert.IsFalse(actual);
        }

        [Test]
        public void Sortable()
        {
            var list = new List<MinimumLengthRule>
            {
                new MinimumLengthRule(5),
                new MinimumLengthRule(3),
                new MinimumLengthRule(4)
            };

            var expected = list.OrderBy(x => x.Length);

            list.Sort();

            CollectionAssert.AreEqual(expected, list);
        }
    }
}
