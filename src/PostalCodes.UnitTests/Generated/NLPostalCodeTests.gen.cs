using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class NLPostalCodeTests
    {

        [TestCase("9999ZZ","9998")]
        [TestCase("1000AA","0999")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new NLPostalCode(postalCode);
            var codePredecessor = new NLPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("0999ZZ","1000")]
        [TestCase("3456JT","3457")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new NLPostalCode(postalCode);
            var codeSuccessor = new NLPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());

        }
        
        [TestCase("0000AA")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new NLPostalCode(postalCode)).Predecessor);
        }

        [TestCase("9999ZZ")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new NLPostalCode(postalCode)).Successor);
        }

        [TestCase("12j4h")]
        [TestCase("k3j51l")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new NLPostalCode(postalCode));
        }

        [TestCase("1235DF")]
        [TestCase("5983DH")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new NLPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("1235DF")]
        [TestCase("5983DH")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new NLPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(NLPostalCode));
        }

        [TestCase("1235DF")]
        [TestCase("5983DH")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new NLPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(NLPostalCode));
        }
    }
}
