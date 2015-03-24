using NUnit.Framework;

namespace PostalCodes.UnitTests.CountrySpecificPostalCodes
{
    [TestFixture]
    internal class GBPostalCodeManualTests
    {
        [Test]
        [TestCase("AA9A 9", "AA9A8")]
        [TestCase("BA9A 9AA", "BA9A8")]
        [TestCase("A1 9ZZ", "A18")]
        [TestCase("M1 1AA", "M10")]
        [TestCase("EC1A 1BB", "EC1A0")]
        [TestCase("W1A 1HQ", "W1A0")]
        [TestCase("B33 8TH", "B337")]
        [TestCase("CR2 6XH", "CR25")]
        [TestCase("DN55 1PT", "DN550")]
        [TestCase("Z9 9ZZ", "Z98")]
        [TestCase("Z0 0AA", "Y99")]
        [TestCase("BA00 0AA", "AZ999")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var curr = new GBPostalCode(postalCode);
            var prev = curr.Predecessor;
            Assert.AreEqual(postalCodePredecessor, prev.ToString());
            Assert.IsTrue(curr >= prev);
            Assert.IsTrue(curr > prev);
        }

        [Test]
        [TestCase("AA9A 9AB", "AA9B0")]
        [TestCase("BA9A 9AA", "BA9B0")]
        [TestCase("A1 9ZZ", "A20")]
        [TestCase("M1 1AA", "M12")]
        [TestCase("EC1A 1BB", "EC1A2")]
        [TestCase("W1A 1HQ", "W1A2")]
        [TestCase("B33 8TH", "B339")]
        [TestCase("CR2 6XH", "CR27")]
        [TestCase("DN55 1PT", "DN552")]
        [TestCase("C7 9ZX", "C80")]
        [TestCase("A9 9ZZ", "B00")]
        [TestCase("YZ99 9ZZ", "ZA000")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var curr = new GBPostalCode(postalCode);
            var succ = curr.Successor;
            Assert.AreEqual(postalCodeSuccessor, succ.ToString());
            Assert.IsTrue(curr <= succ);
            Assert.IsTrue(curr < succ);
        }

        [Test]
        [TestCase("AA0A0AA")]
        [TestCase("A0A0AA")]
        [TestCase("A00AA")]
        [TestCase("A000AA")]
        [TestCase("AA00AA")]
        [TestCase("AA000AA")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new GBPostalCode(postalCode)).Predecessor);
        }

        [Test]
        [TestCase("ZZ9Z9ZZ")]
        [TestCase("Z9Z9ZZ")]
        [TestCase("Z99ZZ")]
        [TestCase("Z999ZZ")]
        [TestCase("ZZ99ZZ")]
        [TestCase("ZZ999ZZ")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new GBPostalCode(postalCode)).Successor);
        }

        [Test]
        public void Predecessor_ValidInput_ReturnsGbPostalCodeObject()
        {
            var x = (new GBPostalCode("CC4C4CC")).Predecessor;
            Assert.IsTrue( x.GetType() == typeof(GBPostalCode) );
        }

        [Test]
        public void Successor_ValidInput_ReturnsGbPostalCodeObject()
        {
            var x = (new GBPostalCode("CC4C4CC")).Successor;
            Assert.IsTrue(x.GetType() == typeof(GBPostalCode));
        }
    }
}
