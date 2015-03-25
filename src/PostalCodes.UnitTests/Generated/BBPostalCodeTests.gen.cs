using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class BBPostalCodeTests
    {

        [TestCase("12345","12344")]
        [TestCase("BB 12345","12344")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new BBPostalCode(postalCode);
            var codePredecessor = new BBPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("12345","12346")]
        [TestCase("BB 12345","12346")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new BBPostalCode(postalCode);
            var codeSuccessor = new BBPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());

        }
        
        [TestCase("BB00000")]
        [TestCase("00000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new BBPostalCode(postalCode)).Predecessor);
        }

        [TestCase("BB99999")]
        [TestCase("99999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new BBPostalCode(postalCode)).Successor);
        }

        [TestCase("x1231s")]
        [TestCase("1231sd")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new BBPostalCode(postalCode));
        }

        [TestCase("BB12345")]
        [TestCase("12345")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new BBPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("BB12345")]
        [TestCase("12345")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new BBPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(BBPostalCode));
        }

        [TestCase("BB12345")]
        [TestCase("12345")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new BBPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(BBPostalCode));
        }
    }
}
