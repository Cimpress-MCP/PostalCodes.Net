using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class RUPostalCodeTests
    {

        [TestCase("123456","123455")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new RUPostalCode(postalCode);
            var codePredecessor = new RUPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("123456","123457")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new RUPostalCode(postalCode);
            var codeSuccessor = new RUPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());

        }
        
        [TestCase("000000")]
        [TestCase("000000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new RUPostalCode(postalCode)).Predecessor);
        }

        [TestCase("999999")]
        [TestCase("999999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new RUPostalCode(postalCode)).Successor);
        }

        [TestCase("x1231s")]
        [TestCase("1231sd")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new RUPostalCode(postalCode));
        }

        [TestCase("123456")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new RUPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("123456")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new RUPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(RUPostalCode));
        }

        [TestCase("123456")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new RUPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(RUPostalCode));
        }
    }
}
