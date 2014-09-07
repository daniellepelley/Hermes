using Hermes.Validation.Rules;
using Hermes.Validation.Rules.Preset.String;
using NUnit.Framework;

namespace Hermes.Validation.Test.Rules
{
    [TestFixture]
    public class CasingRuleTest
    {
        [Test]
        public void CasingCanBeAccessed()
        {
            var sut = new CasingRule(CasingType.Lower);
            Assert.AreEqual(CasingType.Lower, sut.CasingType);    
        }

        [Test]
        public void LowerCase_Valid()
        {
            var sut = new CasingRule(CasingType.Lower);
            Assert.IsTrue(sut.CheckValid("lower")); 
        }

        [Test]
        public void LowerCase_Invalid()
        {
            var sut = new CasingRule(CasingType.Lower);
            Assert.AreEqual(sut.Message, sut.Check("UPPER"));
        }

        [Test]
        public void UpperCase_Valid()
        {
            var sut = new CasingRule(CasingType.Upper);
            Assert.IsTrue(sut.CheckValid("UPPER"));
        }

        [Test]
        public void UpperCase_Invalid()
        {
            var sut = new CasingRule(CasingType.Upper);
            Assert.AreEqual(sut.Message, sut.Check("lower"));
        }

        [Test]
        public void ValueIsNull()
        {
            var sut = new CasingRule(CasingType.Lower);
            Assert.IsTrue(sut.CheckValid(null));
        }

        [Test]
        public void ValueIsEmpty()
        {
            var sut = new CasingRule(CasingType.Lower);
            Assert.IsTrue(sut.CheckValid(string.Empty));
        }

        [Test]
        public void NormalCasing_MessageIsEmpty()
        {
            var sut = new CasingRule(CasingType.Normal);
            Assert.AreEqual(string.Empty, sut.Message);
        }

        [Test]
        public void NormalCasing_EnforceDoesNotChangeTheValue()
        {
            var sut = new CasingRule(CasingType.Normal);
            Assert.AreEqual("This", sut.Enforce("This"));
        }

        [Test]
        public void LowerCase_Enforce()
        {
            var sut = new CasingRule(CasingType.Lower);
            Assert.AreEqual("lower", sut.Enforce("LOWER"));
        }

        [Test]
        public void UpperCase_Enforce()
        {
            var sut = new CasingRule(CasingType.Upper);
            Assert.AreEqual("UPPER", sut.Enforce("upper"));
        }

    }
}
