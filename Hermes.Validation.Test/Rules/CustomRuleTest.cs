using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.Validation.Interfaces;
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
        public void Valid()
        {
            var sut = CreateSut();
            Assert.IsNullOrEmpty(sut.Check("valid"));
            Assert.IsTrue(sut.CheckValid("valid"));
        }

        [Test]
        public void Invalid()
        {
            var sut = CreateSut();
            Assert.IsNotNullOrEmpty(sut.Check("invalid"));
            Assert.IsFalse(sut.CheckValid("invalid"));
        }

        [Test]
        public void NoLogic()
        {
            var sut = new CustomRule<string>(null, "This is invalid");
            Assert.IsNullOrEmpty(sut.Check("valid"));
        }

        [Test]
        public void LogicCanBeAccessed()
        {
            var logic = new Func<string, string>(s => s == "valid" ? string.Empty : "Not Valid");
            var sut = new CustomRule<string>(logic, "This is invalid");
            Assert.AreSame(logic, sut.Logic);
        }

        [Test]
        public void MessageCanBeAccessed()
        {
            const string expected = "Test Message";

            var logic = new Func<string, string>(s => s == "valid" ? string.Empty : "Not Valid");
            var sut = new CustomRule<string>(logic, expected);

            Assert.AreSame(expected, sut.Message);
        }

        [Test]
        public void Sortable()
        {
            var list = new List<IRule>();

            for (int i = 0; i < 2; i++)
            {
                var logic = new Func<string, string>(s => s == "valid" ? string.Empty : "Not Valid");
                list.Add(new CustomRule<string>(logic, "Test Message"));
            }

            list.Sort();
        }

        [Test]
        public void IsIComparable()
        {
            var logic = new Func<string, string>(s => s == "valid" ? string.Empty : "Not Valid");
            var sut = new CustomRule<string>(logic, "Test Message");
            Assert.IsInstanceOf<IComparable>(sut);
        }
    }
}
