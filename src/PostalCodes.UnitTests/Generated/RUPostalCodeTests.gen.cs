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
            Assert.AreEqual(postalCodePredecessor, code.Predecessor.ToString());
        }

		[TestCase("123456","123457")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new RUPostalCode(postalCode);
            Assert.AreEqual(postalCodeSuccessor, code.Successor.ToString());
        }

		[TestCase("000000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new RUPostalCode(postalCode)).Predecessor);
        }

		[TestCase("999999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new RUPostalCode(postalCode)).Successor);
        }

		[TestCase("1231sd")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new RUPostalCode(postalCode));
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
