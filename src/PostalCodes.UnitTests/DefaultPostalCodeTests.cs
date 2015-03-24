using System;
using NUnit.Framework;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes.UnitTests
{
    [TestFixture]
    internal class DefaultPostalCodeTests
    {
        [Test]
        [TestCase("0002", "0001")]
        [TestCase("0020", "0019")]
        [TestCase("0300", "0299")]
        [TestCase("4000", "3999")]
        [TestCase("3453", "3452")]
        [TestCase("0999", "0998")]
        [TestCase("0139290", "0139289")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            Assert.AreEqual(postalCodePredecessor, (new DefaultPostalCode(postalCode)).Predecessor.ToString());
        }

        [Test]
        [TestCase("0001", "0002")]
        [TestCase("0019", "0020")]
        [TestCase("0299", "0300")]
        [TestCase("3999", "4000")]
        [TestCase("3452", "3453")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            Assert.AreEqual(postalCodeSuccessor, (new DefaultPostalCode(postalCode)).Successor.ToString());
        }
        
        [Test]
        [TestCase("00000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new DefaultPostalCode(postalCode)).Predecessor);
        }

        [Test]
        [TestCase("99999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new DefaultPostalCode(postalCode)).Successor);
        }

        [Test]
        [TestCase("999x99")]
        public void Constructor_InvalidNumber_ThrowsFormatException(string postalCode)
        {
            Assert.Throws<ArgumentException>( () => new DefaultPostalCode(postalCode));
        }

        [Test]
        public void Predecessor_ValidInput_ReturnsNumericPostalCodeObject()
        {
            var x = (new DefaultPostalCode("444")).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(DefaultPostalCode));
        }

        [Test]
        public void Successor_ValidInput_ReturnsNumericPostalCodeObject()
        {
            var x = (new DefaultPostalCode("444")).Successor;
            Assert.IsTrue(x.GetType() == typeof(DefaultPostalCode));
        }
    }
}
