using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class BBPostalCodeTests
    {

        [TestCase("BB 12345","12344")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new BBPostalCode(postalCode);
            Assert.AreEqual(postalCodePredecessor, code.Predecessor.ToString());
        }

        [TestCase("BB 12345","12346")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new BBPostalCode(postalCode);
            Assert.AreEqual(postalCodeSuccessor, code.Successor.ToString());
        }

        [TestCase("00000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new BBPostalCode(postalCode)).Predecessor);
        }

        [TestCase("99999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new BBPostalCode(postalCode)).Successor);
        }

        [TestCase("1231sd")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new BBPostalCode(postalCode));
        }

        [TestCase("12345")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new BBPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(BBPostalCode));
        }

        [TestCase("12345")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new BBPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(BBPostalCode));
        }
    }
}
