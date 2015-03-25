using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class MTPostalCodeTests
    {

        [TestCase("ZZZ0000","ZZY9999")]
        [TestCase("AAA1000","AAA0999")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new MTPostalCode(postalCode);
            var codePredecessor = new MTPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("ABC1234","ABC1235")]
        [TestCase("ZZY9999","ZZZ0000")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new MTPostalCode(postalCode);
            var codeSuccessor = new MTPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());

        }
        
        [TestCase("AAA0000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new MTPostalCode(postalCode)).Predecessor);
        }

        [TestCase("ZZZ9999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new MTPostalCode(postalCode)).Successor);
        }

        [TestCase("ABCABC")]
        [TestCase("123ABCD")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new MTPostalCode(postalCode));
        }

        [TestCase("ABC1234")]
        [TestCase("SHD4783")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new MTPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(MTPostalCode));
        }

        [TestCase("ABC1234")]
        [TestCase("SHD4783")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new MTPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(MTPostalCode));
        }
    }
}
