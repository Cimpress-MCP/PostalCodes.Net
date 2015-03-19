using NUnit.Framework;
using PostalCodes.CountrySpecificPostalCodes;

namespace PostalCodes.UnitTests.CountrySpecificPostalCodes
{
    [TestFixture]
    internal class PTPostalCodeTests
    {
        [Test]
        [TestCase("2660", "2659999")]
        [TestCase("2660023", "2660022")]
        public void Predecessor_ValidInputWithStart_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            Assert.AreEqual(postalCodePredecessor, (new PTPostalCode(postalCode, true)).Predecessor.ToString());
        }

        [Test]
        [TestCase("2660", "2660998")]
        [TestCase("2660023", "2660022")]
        public void Predecessor_ValidInputWithEnd_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            Assert.AreEqual(postalCodePredecessor, (new PTPostalCode(postalCode, false)).Predecessor.ToString());
        }

        [Test]
        [TestCase("2660", "2660001")]
        [TestCase("2660023", "2660024")]
        public void Successor_ValidInputWithStart_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            Assert.AreEqual(postalCodeSuccessor, (new PTPostalCode(postalCode, true)).Successor.ToString());
        }

        [Test]
        [TestCase("2660", "2661000")]
        [TestCase("2660023", "2660024")]
        public void Successor_ValidInputWithEnd_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            Assert.AreEqual(postalCodeSuccessor, (new PTPostalCode(postalCode, false)).Successor.ToString());
        }

        [Test]
        [TestCase("0000")]
        [TestCase("0000000")]
        public void Predecessor_FirstInRangeWithStart_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new PTPostalCode(postalCode, true)).Predecessor);
        }

        [Test]
        [TestCase("0000000")]
        public void Predecessor_FirstInRangeWithEnd_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new PTPostalCode(postalCode, false)).Predecessor);
        }

        [Test]
        [TestCase("9999999")]
        public void Successor_LastInRangeWithStart_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new PTPostalCode(postalCode, true)).Successor);
        }

        [Test]
        [TestCase("9999")]
        [TestCase("9999999")]
        public void Successor_LastInRangeWithEnd_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new PTPostalCode(postalCode, false)).Successor);
        }

        [Test]
        public void Predecessor_ValidInput_ReturnsPortugalPostalCodeObject()
        {
            var x = (new PTPostalCode("1241",false)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(PTPostalCode));
        }

        [Test]
        public void Successor_ValidInput_ReturnsPortugalPostalCodeObject()
        {
            var x = (new PTPostalCode("1241123",true)).Successor;
            Assert.IsTrue(x.GetType() == typeof(PTPostalCode));
        }
    }
}
