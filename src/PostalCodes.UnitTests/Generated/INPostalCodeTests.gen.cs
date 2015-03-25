using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class INPostalCodeTests
    {

        [TestCase("828688","828687")]
        [TestCase("112634","112633")]
        [TestCase("000001","000000")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new INPostalCode(postalCode);
            var codePredecessor = new INPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("999998","999999")]
        [TestCase("448952","448953")]
        [TestCase("142934","142935")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new INPostalCode(postalCode);
            var codeSuccessor = new INPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());

        }
        
        [TestCase("000000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new INPostalCode(postalCode)).Predecessor);
        }

        [TestCase("999999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new INPostalCode(postalCode)).Successor);
        }

        [TestCase("12233345")]
        [TestCase("1s223s")]
        [TestCase("x12xx3")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new INPostalCode(postalCode));
        }

        [TestCase("122334")]
        [TestCase("525678")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new INPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(INPostalCode));
        }

        [TestCase("122334")]
        [TestCase("525678")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new INPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(INPostalCode));
        }
    }
}
