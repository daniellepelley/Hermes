using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hermes.Validation.Test.Rules
{
    [TestFixture]
    public class ContainsRuleTest
    {
        [Test]
        public void Contains()
        {
            var sut = new ContainsRule("text");
            Assert.IsTrue(sut.CheckValid("this text"));
        }

        [Test]
        public void DoesNotContain()
        {
            var sut = new ContainsRule("text");
            Assert.IsFalse(sut.CheckValid("this tex"));
        }

        [Test]
        public void Contains_ThenReturnsMessage()
        {
            var sut = new ContainsRule("text");
            Assert.AreEqual(sut.Message, sut.Check("this tex"));
        }

        [Test]
        public void Null_ThenReturnsEmpty()
        {
            var sut = new ContainsRule("text");
            Assert.AreEqual(string.Empty, sut.Check(null));
        }

        [Test]
        public void ReturnsContainsTest()
        {
            var sut = new ContainsRule("text");
            Assert.AreEqual("text", sut.ContainsString);
        }

    }
}
