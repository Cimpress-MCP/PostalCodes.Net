using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class MYPostalCodeTests
    {

        [TestCase("00001","00000")]
        [TestCase("82888","82887")]
        [TestCase("11234","11233")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new MYPostalCode(postalCode);
            var codePredecessor = new MYPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("44852","44853")]
        [TestCase("99998","99999")]
        [TestCase("14234","14235")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new MYPostalCode(postalCode);
            var codeSuccessor = new MYPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());

        }
        
        [TestCase("00000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new MYPostalCode(postalCode)).Predecessor);
        }

        [TestCase("99999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new MYPostalCode(postalCode)).Successor);
        }

        [TestCase("122345")]
        [TestCase("1223s")]
        [TestCase("x12u3")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new MYPostalCode(postalCode));
        }

        [TestCase("12234")]
        [TestCase("52678")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new MYPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(MYPostalCode));
        }

        [TestCase("12234")]
        [TestCase("52678")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new MYPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(MYPostalCode));
        }
    }
}
