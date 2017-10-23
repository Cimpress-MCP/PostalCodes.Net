using NUnit.Framework;
using System.Runtime.InteropServices;

namespace PostalCodes.UnitTests.CountrySpecificPostalCodes
{
    [TestFixture]
    internal class GBPostalCodeManualTests
    {
        [Test]
		[TestCase("AA9A 9", "AA9A8")]
        [TestCase("BA9A 9AA", "BA9A8ZZ")]
        [TestCase("A1 9ZZ", "A19ZY")]
        [TestCase("M1 1AA", "M10ZZ")]
        [TestCase("EC1A 1BB", "EC1A1BA")]
        [TestCase("W1A 1HQ", "W1A1HP")]
        [TestCase("B33 8TH", "B338TG")]
        [TestCase("CR2 6XH", "CR26XG")]
        [TestCase("DN55 1PT", "DN551PS")]
        [TestCase("Z9 9ZZ", "Z99ZY")]
        [TestCase("Z00AA", "Y99ZZ")]
        [TestCase("BA00 0AA", "AZ999ZZ")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var curr = new GBPostalCode(postalCode);
            var prev = curr.Predecessor;
            Assert.AreEqual(postalCodePredecessor, prev.ToString());
            Assert.IsTrue(curr >= prev);
            Assert.IsTrue(curr > prev);
        }

        [Test]
        [TestCase("AA9A 9AB", "AA9A9AC")]
        [TestCase("BA9A 9AA", "BA9A9AB")]
        [TestCase("A1 9ZZ", "A20AA")]
        [TestCase("M1 1AA", "M11AB")]
        [TestCase("EC1A 1BB", "EC1A1BC")]
        [TestCase("W1A 1HQ", "W1A1HR")]
        [TestCase("B33 8TH", "B338TI")]
        [TestCase("CR2 6XH", "CR26XI")]
        [TestCase("DN55 1PT", "DN551PU")]
        [TestCase("C7 9ZX", "C79ZY")]
        [TestCase("A9 9ZZ", "B00AA")]
        [TestCase("YZ99 9ZZ", "ZA000AA")]
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
            Assert.AreEqual(expectedMatch, GBPostalCode.HasSameFormat(code1, code2));
        }

        [Test, Category("Integration")]
        public void WithBritishZipCode_ReturnsFalseIfFormatsDifferentLength()
        {
            var cFactory = new CountryFactory(new IsoCountryCodeValidator());
            var pcFactory = new PostalCodeFactory();
            var country = cFactory.CreateCountry("GB");
            var right = new PostalCodeRange(pcFactory.CreatePostalCode(country, "AA99 9AA"), pcFactory.CreatePostalCode(country, "BB99 9AA"));
            var left = new PostalCodeRange(pcFactory.CreatePostalCode(country, "A9A 9AA"), pcFactory.CreatePostalCode(country, "C9A 9AA"));
            Assert.IsFalse(PostalCodeRange.Contains(left, right));
        }

        [Test, Category("Integration")]
        public void WithBritishZipCode_ReturnsFalseIfFormatsSameLengthButDifferentFormat()
        {
            var cFactory = new CountryFactory(new IsoCountryCodeValidator());
            var pcFactory = new PostalCodeFactory();
            var country = cFactory.CreateCountry("GB");
            var right = new PostalCodeRange(pcFactory.CreatePostalCode(country, "A99 9AA"), pcFactory.CreatePostalCode(country, "B99 9AA"));
            var left = new PostalCodeRange(pcFactory.CreatePostalCode(country, "AA9 9AA"), pcFactory.CreatePostalCode(country, "CC9 9AA"));
            Assert.IsFalse(PostalCodeRange.Contains(left, right));
        }

        [Test, Category("Integration")]
        public void WithBritishZipCode_ReturnsTrueIfFormatMatchedAndCodesAreContained()
        {
            var cFactory = new CountryFactory(new IsoCountryCodeValidator());
            var pcFactory = new PostalCodeFactory();
            var country = cFactory.CreateCountry("GB");
            var right = new PostalCodeRange(pcFactory.CreatePostalCode(country, "AA9 9AA"), pcFactory.CreatePostalCode(country, "BB9 9AA"));
            var left = new PostalCodeRange(pcFactory.CreatePostalCode(country, "AA9 9AA"), pcFactory.CreatePostalCode(country, "CC9 9AA"));
            Assert.IsTrue(PostalCodeRange.Contains(left, right));
        }
    }
}
