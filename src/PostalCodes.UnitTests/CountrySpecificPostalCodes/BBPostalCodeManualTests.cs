using NUnit.Framework;

namespace PostalCodes.UnitTests.CountrySpecificPostalCodes
{
    [TestFixture]
    internal class BBPostalCodeManualTests
    {
        [TestCase("BB12345", "12344")]
		[TestCase("12345", "12344")]
		public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            Assert.AreEqual(postalCodePredecessor, (new BBPostalCode(postalCode)).Predecessor.ToString());
        }

		[TestCase("BB12345", "12346")]
		[TestCase("12345", "12346")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            Assert.AreEqual(postalCodeSuccessor, (new BBPostalCode(postalCode)).Successor.ToString());
        }

        [TestCase("BB 00000")]
        [TestCase("00000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new BBPostalCode(postalCode)).Predecessor);
        }

        [Test]
        [TestCase("BB99999")]
        [TestCase("99999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new BBPostalCode(postalCode)).Successor);
        }

        [Test]
        public void Predecessor_ValidInput_ReturnsNetherlandicPostalCodeObject()
        {
            var x = (new BBPostalCode("BB11234")).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(BBPostalCode));
        }

        [Test]
        public void Successor_ValidInput_ReturnsNetherlandicPostalCodeObject()
        {
            var x = (new BBPostalCode("BB12345")).Successor;
            Assert.IsTrue(x.GetType() == typeof(BBPostalCode));
        }
    }
}
