using NUnit.Framework;
using System.Runtime.InteropServices;

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
            Assert.IsTrue(x.GetType() == typeof(GBPostalCode));
        }

        [Test]
        public void Successor_ValidInput_ReturnsGbPostalCodeObject()
        {
            var x = (new GBPostalCode("CC4C4CC")).Successor;
            Assert.IsTrue(x.GetType() == typeof(GBPostalCode));
        }

        [TestCase("ZZ9Z9ZZ", "ZZ9Z9ZZ", true)]
        [TestCase("ZZ9Z9ZZ", "ZZ9Z9", true)]

        [TestCase("Z9Z9ZZ", "Z9Z9ZZ", true)]
        [TestCase("Z9Z9ZZ", "Z9Z9", true)]

        [TestCase("Z99ZZ", "Z99ZZ", true)]
        [TestCase("Z99ZZ", "Z99", true)]

        [TestCase("Z999ZZ", "Z999ZZ", true)]
        [TestCase("Z999ZZ", "Z999", true)]

        [TestCase("ZZ99ZZ", "ZZ99ZZ", true)]
        [TestCase("ZZ99ZZ", "ZZ99", true)]

        [TestCase("ZZ999ZZ", "ZZ999ZZ", true)]
        [TestCase("ZZ999ZZ", "ZZ999", true)]

        [TestCase("ZZ999ZZ", "ZZ99", false)]
        [TestCase("ZZ999ZZ", "ZZ9Z9", false)]
        [TestCase("ZZ999ZZ", "Z9Z9", false)]
        [TestCase("ZZ999ZZ", "Z99", false)]
        [TestCase("ZZ999ZZ", "Z999", false)]

        [TestCase("ZZ99ZZ", "ZZ9Z9", false)]
        [TestCase("ZZ99ZZ", "Z9Z9", false)]
        [TestCase("ZZ99ZZ", "Z99", false)]
        [TestCase("ZZ99ZZ", "Z999", false)]
        [TestCase("ZZ99ZZ", "ZZ999", false)]

        [TestCase("Z999ZZ", "ZZ99", false)]
        [TestCase("Z999ZZ", "ZZ9Z9", false)]
        [TestCase("Z999ZZ", "Z9Z9", false)]
        [TestCase("Z999ZZ", "Z99", false)]
        [TestCase("Z999ZZ", "ZZ999", false)]

        [TestCase("Z99ZZ", "ZZ9Z9", false)]
        [TestCase("Z99ZZ", "Z9Z9", false)]
        [TestCase("Z99ZZ", "Z999", false)]
        [TestCase("Z99ZZ", "ZZ99", false)]
        [TestCase("Z99ZZ", "ZZ999", false)]

        [TestCase("Z9Z9ZZ", "ZZ9Z9", false)]
        [TestCase("Z9Z9ZZ", "Z99", false)]
        [TestCase("Z9Z9ZZ", "Z999", false)]
        [TestCase("Z9Z9ZZ", "ZZ99", false)]
        [TestCase("Z9Z9ZZ", "ZZ999", false)]

        [TestCase("ZZ9Z9ZZ", "Z9Z9", false)]
        [TestCase("ZZ9Z9ZZ", "Z99", false)]
        [TestCase("ZZ9Z9ZZ", "Z999", false)]
        [TestCase("ZZ9Z9ZZ", "ZZ99", false)]
        [TestCase("ZZ9Z9ZZ", "ZZ999", false)]
        public void PostalCodeFormatsMatch(string code1, string code2, bool expectedMatch)
        {
            Assert.AreEqual(expectedMatch, GBPostalCode.PostalCodeFormatsMatch(code1, code2));
        }
    }
}
