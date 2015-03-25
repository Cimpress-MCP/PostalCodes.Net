using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class PTPostalCodeTests
    {

        [TestCase("1234","1233")]
        [TestCase("1234567","1234566")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new PTPostalCode(postalCode);
            var codePredecessor = new PTPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("1235","1236")]
        [TestCase("1248192","1248193")]
        [TestCase("9999998","9999999")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new PTPostalCode(postalCode);
            var codeSuccessor = new PTPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());

        }
        
        [TestCase("0000000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new PTPostalCode(postalCode)).Predecessor);
        }

        [TestCase("9999999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new PTPostalCode(postalCode)).Successor);
        }

        [TestCase("1d2345")]
        [TestCase("xxx123")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new PTPostalCode(postalCode));
        }

        [TestCase("1231242")]
        [TestCase("1234")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new PTPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("1231242")]
        [TestCase("1234")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new PTPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(PTPostalCode));
        }

        [TestCase("1231242")]
        [TestCase("1234")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new PTPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(PTPostalCode));
        }
    }
}
