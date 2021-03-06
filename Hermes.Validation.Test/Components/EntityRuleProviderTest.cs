﻿using System.Linq;
using Hermes.Validation.Rules;
using Hermes.Validation.Rules.Preset.String;
using Hermes.Validation.Test.Helpers;
using NUnit.Framework;

namespace Hermes.Validation.Test.Components
{
    [TestFixture]
    public class EntityRuleProviderTest
    {
        private static EntityRuleProvider<TestClass> CreateSut()
        {
            var sut = new EntityRuleProvider<TestClass>();
            sut.AddRules(x => x.Value, 
                new MaximumLengthRule(5),
                new MinimumLengthRule(4));
                        
            return sut;
        }

        [Test]
        public void NumberOfRulesAdded()
        {
            var sut = CreateSut();
            Assert.AreEqual(2, sut.PropertyRules["Value"].RulesCount);
        }

        [Test]
        public void WhenTextIsValid1()
        {
            var sut = CreateSut();

            var testClass = new TestClass { Value = "12345" };

            var actual = sut.Validate(testClass);

            Assert.IsTrue(!actual["Value"].Any());
        }

        [Test]
        public void WhenTextIsValid2()
        {
            var sut = CreateSut();

            var testClass = new TestClass { Value = "1234" };

            var actual = sut.Validate(testClass);

            Assert.IsTrue(!actual["Value"].Any());
        }

        [Test]
        public void WhenTextIsInValid1()
        {
            var sut = CreateSut();

            var testClass = new TestClass { Value = "123456" };

            var actual = sut.Validate(testClass);

            Assert.IsFalse(!actual["Value"].Any());
        }

        [Test]
        public void WhenTextIsInValid2()
        {
            var sut = CreateSut();

            var testClass = new TestClass { Value = "123" };

            var actual = sut.Validate(testClass);

            Assert.IsFalse(!actual["Value"].Any());
        }

        [Test]
        public void Clean_WhenTextIsInValid1()
        {
            var sut = CreateSut();

            var testClass = new TestClass { Value = "123456" };

            var actual = sut.Clean(testClass);

            Assert.AreEqual("12345", actual.Value);
        }

        [Test]
        public void CanAddEntityRule()
        {
            var sut = CreateSut();

            sut.AddRule(string.Empty, new CustomRule<TestClass>(
                x => x.Value != "invalid" ? string.Empty : "Must not equal invalid", string.Empty));

            Assert.AreEqual(1, sut.EntityRules.Rules.Count);
        }

        [Test]
        public void WhenEntityRuleIsValid()
        {
            var sut = CreateSut();

            sut.AddRule(string.Empty, new CustomRule<TestClass>(
                x => x.Value != "invalid" ? string.Empty : "Must not equal invalid", string.Empty));

            var testClass = new TestClass { Value = "valid" };

            var actual = sut.Validate(testClass);

            Assert.IsTrue(!actual["Value"].Any());
        }

        [Test]
        public void WhenEntityRuleIsInValid()
        {
            var sut = CreateSut();

            sut.AddRule(string.Empty, new CustomRule<TestClass>(
                x => x.Value != "invalid" ? string.Empty : "Must not equal invalid", string.Empty));

            var testClass = new TestClass { Value = "invalid" };

            var actual = sut.Validate(testClass);

            Assert.IsTrue(actual["Value"].Any());
        }
    }
}