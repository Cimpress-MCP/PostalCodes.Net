using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.CountrySpecificPostalCodes
{
    [TestFixture]
	internal class MTPostalCodeManualTests
    {
        [TestCase("PLA123")]
        [TestCase("PL1234")]
        [TestCase("PLA12354")]
        [TestCase("PLAA1235")]
        public void Constructor_InvalidPostalCode_ThrowsArgumentException(string postalCode)
        {
            Assert.That(() => new MTPostalCode(postalCode), Throws.InstanceOf<ArgumentException>());
        }

        [TestCase("PLA 1235", "PLA1235")]
        [TestCase("PLA-1235", "PLA1235")]
        [TestCase("PLA1235", "PLA1235")]
        public void Constructor_ValidPostalCode_ReturnsValidMTPostalCode(string postalCode, string expectedToString)
        {
            var mtPostalCode = new MTPostalCode(postalCode);
            Assert.AreEqual(expectedToString, mtPostalCode.ToString());
        }

        [TestCase("PLA1234", "PLA1233")]
        [TestCase("PLA1233", "PLA1232")]
        [TestCase("ABC0000", "ABB9999")]
        [TestCase("ABA0000", "AAZ9999")]
        [TestCase("ZZZ9999", "ZZZ9998")]
        public void Predecessor_ValidInputNotFirstInRange_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            Assert.AreEqual(postalCodePredecessor, (new MTPostalCode(postalCode)).Predecessor.ToString());
        }

        [TestCase("PLA1234", "PLA1235")]
        [TestCase("PLA1199", "PLA1200")]
        [TestCase("PLA9999", "PLB0000")]
        [TestCase("PZZ9999", "QAA0000")]
        public void Successor_ValidInputNotLastInRange_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            Assert.AreEqual(postalCodeSuccessor, (new MTPostalCode(postalCode)).Successor.ToString());
        }

        [TestCase("AAA0000")]
        public void Predecessor_ValidInputFirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new MTPostalCode(postalCode)).Predecessor);
        }

        [TestCase("ZZZ9999")]
        public void Successor_ValidInputLastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new MTPostalCode(postalCode)).Successor);
        }

        [Test]
        public void Predecessor_ValidInput_ReturnsMTPostalCodeObject()
        {
            var x = (new MTPostalCode("PLA1234")).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(MTPostalCode));
        }

        [Test]
        public void Successor_ValidInput_ReturnsMTPostalCodeObject()
        {
            var x = (new MTPostalCode("PLA1234")).Successor;
            Assert.IsTrue(x.GetType() == typeof(MTPostalCode));
        }
    }
}
