using Hermes.Validation.Rules.Preset.String;
using NUnit.Framework;

namespace Hermes.Validation.Test.Rules
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
