using Hermes.Validation.Rules;
using NUnit.Framework;

namespace Hermes.Validation.Test
{
    [TestFixture]
    public class MinimumLengthRuleTest
    {
        [Test]
        public void WhenTextIsValid()
        {
            var sut = new MinimumLengthRule(5);

            var actual = sut.CheckValid("valid");

            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenTextIsInvalid()
        {
            var sut = new MinimumLengthRule(5);

            var actual = sut.CheckValid("tiny");

            Assert.IsFalse(actual);
        }
    }
}
