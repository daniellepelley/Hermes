using System.Collections.Generic;
using System.Web.Mvc;
using Hermes.Validation.Interfaces;
using Hermes.Validation.Rules.Preset.Numeric;
using Hermes.Validation.Rules.Preset.String;
using NUnit.Framework;

namespace Hermes.Mvc.Angular.Test
{
    public class RulesToAngularAttributesTest
    {
        [Test]
        public void MaximumLengthRuleAttributeName()
        {
            var rule = new MaximumLengthRule(5);
            var sut = new RulesToAttributeConverter();

            var actual = sut.CreateAttribute(rule)[0];

            Assert.AreEqual("maxlength", actual);
        }

        [Test]
        public void MaximumLengthRuleAttributeValue()
        {
            var rule = new MaximumLengthRule(5);
            var sut = new RulesToAttributeConverter();

            var actual = sut.CreateAttribute(rule)[1];

            Assert.AreEqual("5", actual);
        }

        [Test]
        public void MinRuleAttributeName()
        {
            var rule = new MinimumRule(6);
            var sut = new RulesToAttributeConverter();

            var actual = sut.CreateAttribute(rule)[0];

            Assert.AreEqual("min", actual);
        }

        [Test]
        public void MinRuleAttributeValue()
        {
            var rule = new MinimumRule(6);
            var sut = new RulesToAttributeConverter();

            var actual = sut.CreateAttribute(rule)[1];

            Assert.AreEqual("6", actual);
        }

        [Test]
        public void MaxRuleAttributeName()
        {
            var rule = new MaximumRule(7);
            var sut = new RulesToAttributeConverter();

            var actual = sut.CreateAttribute(rule)[0];

            Assert.AreEqual("max", actual);
        }

        [Test]
        public void MaxRuleAttributeValue()
        {
            var rule = new MaximumRule(7);
            var sut = new RulesToAttributeConverter();

            var actual = sut.CreateAttribute(rule)[1];

            Assert.AreEqual("7", actual);
        }

        [Test]
        public void CreateAttributes()
        {
            var rules = new IRule[]
            {
                new MinimumRule(6),
                null,
                new MaximumRule(7),
                new MaximumLengthRule(5),
                null
            };

            var sut = new RulesToAttributeConverter();   

            var dictionary = new Dictionary<string, string>
            {
                {"min", "6"},
                {"max", "7"},
                {"maxlength", "5"}
            };

            var actual = sut.CreateAttributes(rules);

            CollectionAssert.AreEquivalent(dictionary,actual);
        }

        [Test]
        public void AddsAttributesToTag()
        {
            var rules = new IRule[]
            {
                new MinimumRule(6),
                new MaximumRule(7),
                new MaximumLengthRule(5)
            };
            var tagBuilder = new TagBuilder("div");

            var sut = new RulesToAttributeConverter();
            sut.SetUpTag(tagBuilder, rules);

            var expected = @"<div max=""7"" maxlength=""5"" min=""6"">";
            var actual = tagBuilder.ToString(TagRenderMode.StartTag);

            Assert.AreEqual(expected, actual);
        }

    }
}
