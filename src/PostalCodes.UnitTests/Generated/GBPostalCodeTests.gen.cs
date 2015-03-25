using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class GBPostalCodeTests
    {

        [TestCase("Z999ZZ","Z998")]
        [TestCase("ZZ9Z9ZZ","ZZ9Z8")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new GBPostalCode(postalCode);
            var codePredecessor = new GBPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("Z989ZZ","Z990")]
        [TestCase("ZZ0Z9","ZZ1A0")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new GBPostalCode(postalCode);
            var codeSuccessor = new GBPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());

        }
        
        [TestCase("AA0A0AA")]
        [TestCase("A0A0AA")]
        [TestCase("A00AA")]
        [TestCase("A000AA")]
        [TestCase("AA00AA")]
        [TestCase("AA000AA")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new GBPostalCode(postalCode)).Predecessor);
        }

        [TestCase("ZZ9Z9ZZ")]
        [TestCase("Z9Z9ZZ")]
        [TestCase("Z99ZZ")]
        [TestCase("Z999ZZ")]
        [TestCase("ZZ99ZZ")]
        [TestCase("ZZ999ZZ")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new GBPostalCode(postalCode)).Successor);
        }

        [TestCase("12345")]
        [TestCase("xxx123")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new GBPostalCode(postalCode));
        }

        [TestCase("ZZ9A9ZZ")]
        [TestCase("A9Z9ZZ")]
        [TestCase("Z29ZZ")]
        [TestCase("Z699ZZ")]
        [TestCase("ZX99ZZ")]
        [TestCase("ZC999ZZ")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new GBPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("ZZ9A9ZZ")]
        [TestCase("A9Z9ZZ")]
        [TestCase("Z29ZZ")]
        [TestCase("Z699ZZ")]
        [TestCase("ZX99ZZ")]
        [TestCase("ZC999ZZ")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new GBPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(GBPostalCode));
        }

        [TestCase("ZZ9A9ZZ")]
        [TestCase("A9Z9ZZ")]
        [TestCase("Z29ZZ")]
        [TestCase("Z699ZZ")]
        [TestCase("ZX99ZZ")]
        [TestCase("ZC999ZZ")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new GBPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(GBPostalCode));
        }
    }
}
