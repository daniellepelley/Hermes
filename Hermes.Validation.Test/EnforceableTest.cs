﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.Validation.Interfaces;
using Hermes.Validation.Rules;
using NUnit.Framework;

namespace Hermes.Validation.Test
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