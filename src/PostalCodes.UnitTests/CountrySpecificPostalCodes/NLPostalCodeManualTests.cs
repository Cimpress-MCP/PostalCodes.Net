using NUnit.Framework;

namespace PostalCodes.UnitTests.CountrySpecificPostalCodes
{
    [TestFixture]
	internal class NLPostalCodeManualTests
    {
        [Test]
        [TestCase("9992 ZZ", "9991")]
        [TestCase("9992", "9991")]
        [TestCase("1000 ZZ", "0999")]
        [TestCase("4000 ZZ", "3999")]
        [TestCase("1422", "1421")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            Assert.AreEqual(postalCodePredecessor, (new NLPostalCode(postalCode)).Predecessor.ToString());
        }

        [Test]
        [TestCase("9992 ZZ", "9993")]
        [TestCase("9992", "9993")]
        [TestCase("1000 ZZ", "1001")]
        [TestCase("4000 ZZ", "4001")]
        [TestCase("8999", "9000")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            Assert.AreEqual(postalCodeSuccessor, (new NLPostalCode(postalCode)).Successor.ToString());
        }

        [Test]
        [TestCase("0000 ZZ")]
        [TestCase("0000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new NLPostalCode(postalCode)).Predecessor);
        }

        [Test]
        [TestCase("9999 ZZ")]
        [TestCase("9999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new NLPostalCode(postalCode)).Successor);
        }

        [Test]
        public void Predecessor_ValidInput_ReturnsNetherlandicPostalCodeObject()
        {
            var x = (new NLPostalCode("1234 ZZ")).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(NLPostalCode));
        }

        [Test]
        public void Successor_ValidInput_ReturnsNetherlandicPostalCodeObject()
        {
            var x = (new NLPostalCode("1234 ZZ")).Successor;
            Assert.IsTrue(x.GetType() == typeof(NLPostalCode));
        }
    }
}
