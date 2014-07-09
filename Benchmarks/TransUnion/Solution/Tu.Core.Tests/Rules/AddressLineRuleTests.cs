﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tu.Rules;

namespace Tu.Core.Tests.Rules
{
    [TestClass]
    public class AddressLineRuleTests
    {
        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("AddressLineRule_IsValid")]
        public void AddressLineRule_IsValid_CorrectRules_Invoked()
        {
            //------------Setup for test--------------------------
            var validator = new Mock<IValidator>();
            validator.Setup(v => v.IsNullOrEmpty(It.IsAny<string>())).Returns(false).Verifiable();
            validator.Setup(v => v.IsLengthLessThanOrEqualTo(It.IsAny<string>(), It.Is<int>(i => i == 100))).Returns(true).Verifiable();

            var rule = new AddressLineRule(validator.Object, "1");

            //------------Execute Test---------------------------
            rule.IsValid(It.IsAny<string>());

            //------------Assert Results-------------------------
            validator.Verify(v => v.IsNullOrEmpty(It.IsAny<string>()));
            validator.Verify(v => v.IsLengthLessThanOrEqualTo(It.IsAny<string>(), It.Is<int>(i => i == 100)));
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("AddressLineRule_IsValid")]
        public void AddressLineRule_IsValid_IsNull_True()
        {
            AddressLineRule_IsValid_Verify(expected: true, isNull: true, isLengthLessThanOrEqualTo50: false);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("AddressLineRule_IsValid")]
        public void AddressLineRule_IsValid_IsNotNullAndLengthGreaterThan50_False()
        {
            AddressLineRule_IsValid_Verify(expected: false, isNull: false, isLengthLessThanOrEqualTo50: false);
        }

        [TestMethod]
        [Owner("Trevor Williams-Ros")]
        [TestCategory("AddressLineRule_IsValid")]
        public void AddressLineRule_IsValid_IsNotNullAndLengthLessThanOrEqualTo50_True()
        {
            AddressLineRule_IsValid_Verify(expected: true, isNull: false, isLengthLessThanOrEqualTo50: true);
        }

        static void AddressLineRule_IsValid_Verify(bool expected, bool isNull, bool isLengthLessThanOrEqualTo50)
        {
            //------------Setup for test--------------------------
            var validator = new Mock<IValidator>();
            validator.Setup(v => v.IsNullOrEmpty(It.IsAny<string>())).Returns(isNull);
            validator.Setup(v => v.IsLengthLessThanOrEqualTo(It.IsAny<string>(), It.IsAny<int>())).Returns(isLengthLessThanOrEqualTo50);

            var rule = new AddressLineRule(validator.Object, "1");

            //------------Execute Test---------------------------
            var actual = rule.IsValid(isNull ? null : It.IsAny<string>());

            //------------Assert Results-------------------------
            Assert.AreEqual(expected, actual);
        }
    }
}
