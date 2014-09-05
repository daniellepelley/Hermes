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
    }
}