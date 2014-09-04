using Hermes.Validation.Rules;
using NUnit.Framework;

namespace Hermes.Validation.Test.Rules
{
    [TestFixture]
    public class CasingRuleTest
    {
        [Test]
        public void WhenIsLowerCase()
        {
            var sut = new CasingRule(CasingType.Lower);
            Assert.IsTrue(sut.CheckValid("lower")); 
        }

        [Test]
        public void WhenIsNotLowerCase()
        {
            var sut = new CasingRule(CasingType.Lower);
            Assert.IsFalse(sut.CheckValid("UPPER"));
        }

        [Test]
        public void WhenIsUpperCase()
        {
            var sut = new CasingRule(CasingType.Upper);
            Assert.IsFalse(sut.CheckValid("lower"));
        }

        [Test]
        public void WhenIsNotUpperCase()
        {
            var sut = new CasingRule(CasingType.Upper);
            Assert.IsTrue(sut.CheckValid("UPPER"));
        }

        [Test]
        public void EnforceLowerCase()
        {
            var sut = new CasingRule(CasingType.Lower);
            Assert.AreEqual("lower", sut.Enforce("LOWER"));
        }

        [Test]
        public void EnforceUpperCase()
        {
            var sut = new CasingRule(CasingType.Upper);
            Assert.AreEqual("UPPER", sut.Enforce("upper"));
        }

    }
}
