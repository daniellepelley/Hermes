using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.Validation.Rules;
using Hermes.Validation.Rules.Preset.String;
using NUnit.Framework;

namespace Hermes.Validation.Test.Rules
{
    [TestFixture]
    public class CustomRuleTest
    {
        public CustomRule<string> CreateSut()
        {
            return new CustomRule<string>(s => s == "valid" ? string.Empty : "Not Valid", "This is invalid");
        }

        [Test]
        public void WhenValid()
        {
            var sut = CreateSut();
            Assert.IsNullOrEmpty(sut.Check("valid"));
            Assert.IsTrue(sut.CheckValid("valid"));
        }

        [Test]
        public void WhenInValid()
        {
            var sut = CreateSut();
            Assert.IsNotNullOrEmpty(sut.Check("invalid"));
            Assert.IsFalse(sut.CheckValid("invalid"));
        }
    }
}
