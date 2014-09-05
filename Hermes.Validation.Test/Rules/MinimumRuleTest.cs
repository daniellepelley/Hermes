using Hermes.Validation.Rules;
using Hermes.Validation.Rules.Preset.Numeric;
using NUnit.Framework;

namespace Hermes.Validation.Test.Rules
{
    [TestFixture]
    public class MinimumRuleTest
    {
        [Test]
        public void WhenTextIsValid1()
        {
            var sut = new MinimumRule(5);

            var actual = sut.CheckValid(5);

            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenTextIsValid2()
        {
            var sut = new MinimumRule(5);

            var actual = sut.CheckValid(8);

            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenTextIsInvalid()
        {
            var sut = new MinimumRule(5);

            var actual = sut.CheckValid(4);

            Assert.IsFalse(actual);
        }
    }
}