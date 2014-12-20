using Hermes.Validation.Interfaces;
using Hermes.Validation.Rules.Preset.String;
using NUnit.Framework;

namespace Hermes.Validation.Test.Components
{
    [TestFixture]
    public class EnforceableTest
    {
        private IEnforcable<string> CreateSut()
        {
            var sut = new MaximumLengthRule(5);
            return sut;
        }

        [Test]
        public void WhenTextIsInValid_ThenEnforceRule()
        {
            var sut = CreateSut();

            var actual = sut.Enforce("123456");

            Assert.AreEqual("12345", actual);
        }

        [Test]
        public void WhenTextIsInValid_ThenDoNotEnforceRule()
        {
            var sut = CreateSut();

            var actual = sut.Enforce("1234");

            Assert.AreEqual("1234", actual);
        }

    }
}
