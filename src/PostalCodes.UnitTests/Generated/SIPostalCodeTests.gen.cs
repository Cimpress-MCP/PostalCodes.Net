using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class SIPostalCodeTests
    {

        [TestCase("1234","1233")]
        [TestCase("8888","8887")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new SIPostalCode(postalCode);
            var codePredecessor = new SIPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("1234","1235")]
        [TestCase("4852","4853")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new SIPostalCode(postalCode);
            var codeSuccessor = new SIPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());

        }
        
        [TestCase("0000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new SIPostalCode(postalCode)).Predecessor);
        }

        [TestCase("9999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new SIPostalCode(postalCode)).Successor);
        }

        [TestCase("12345")]
        [TestCase("123s")]
        [TestCase("12x3")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new SIPostalCode(postalCode));
        }

        [TestCase("1234")]
        [TestCase("5678")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new SIPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("1234")]
        [TestCase("5678")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new SIPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(SIPostalCode));
        }

        [TestCase("1234")]
        [TestCase("5678")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new SIPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(SIPostalCode));
        }
    }
}
