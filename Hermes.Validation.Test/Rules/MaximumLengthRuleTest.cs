using Hermes.Validation.Rules;
using NUnit.Framework;

namespace Hermes.Validation.Test.Rules
{
    [TestFixture]
    public class MaximumLengthRuleTest
    {
        [Test]
        public void WhenTextIsValid()
        {
            var sut = new MaximumLengthRule(5);

            var actual = sut.CheckValid("valid");

            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenTextIsInvalid()
        {
            var sut = new MaximumLengthRule(5);

            var actual = sut.CheckValid("invalid");

            Assert.IsFalse(actual);
        }

        [Test]
        public void WhenTextIsNull()
        {
            var sut = new MaximumLengthRule(5);

            var actual = sut.CheckValid(null);

            Assert.IsTrue(actual);
        }

        [Test]
        public void WhenTextIsEmpty()
        {
            var sut = new MaximumLengthRule(5);

            var actual = sut.CheckValid(string.Empty);

            Assert.IsTrue(actual);
        }
    }
}
