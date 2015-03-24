using NUnit.Framework;

namespace PostalCodes.UnitTests.CountrySpecificPostalCodes
{
    [TestFixture]
    internal class CAPostalCodeManualTests
    {
        [Test]
        [TestCase("A0A0A1", "A0A0A0")]        
        [TestCase("J4G0A0", "J4F9Z9")]
        [TestCase("L2J0A0", "L2I9Z9")]
        [TestCase("L7G0A0", "L7F9Z9")]
        public void Predecessor_ValidInputNotFirstInRange_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            Assert.AreEqual(postalCodePredecessor, (new CAPostalCode(postalCode)).Predecessor.ToString());
        }

        [Test]
        [TestCase("A0A0A1", "A0A0A2")]
        [TestCase("J4G0A0", "J4G0A1")]
        [TestCase("L2I9Z9", "L2J0A0")]
        public void Successor_ValidInputNotLastInRange_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            Assert.AreEqual(postalCodeSuccessor, (new CAPostalCode(postalCode)).Successor.ToString());
        }

        [TestCase("A0A0A0")]       
        public void Predecessor_ValidInputFirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new CAPostalCode(postalCode)).Predecessor);
        }

        [TestCase("Z9Z9Z9")]
        public void Successor_ValidInputLastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new CAPostalCode(postalCode)).Successor);
        }

        [Test]
        public void Predecessor_ValidInput_ReturnsCanadianPostalCodeObject()
        {
            var x = (new CAPostalCode("C4C4C4")).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(CAPostalCode));
        }

        [Test]
        public void Successor_ValidInput_ReturnsCanadianPostalCodeObject()
        {
            var x = (new CAPostalCode("C4C4C4")).Successor;
            Assert.IsTrue(x.GetType() == typeof(CAPostalCode));
        }
    }
}
