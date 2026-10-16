using NUnit.Framework;

namespace PostalCodes.UnitTests
{
    [TestFixture]
    internal class ExpandPostalCodeTests
    {
        // One short code per format that has a short regex and an expansion.
        [TestCase("US", "10001", "0000", "9999")]
        [TestCase("US", "00501", "0000", "9999")]
        [TestCase("GB", "EC1A1", "AA", "ZZ")]
        [TestCase("GB", "W1A0", "AA", "ZZ")]
        [TestCase("GB", "M11", "AA", "ZZ")]
        [TestCase("GB", "B338", "AA", "ZZ")]
        [TestCase("GB", "CR26", "AA", "ZZ")]
        [TestCase("GB", "DN551", "AA", "ZZ")]
        [TestCase("NL", "1234", "AA", "ZZ")]
        [TestCase("PT", "1000", "000", "999")]
        [TestCase("RU", "101", "000", "999")]
        public void Expand_GivesSameCodeAsParsingTheExpandedString(string countryCode, string shortCode, string lowestSuffix, string highestSuffix)
        {
            var country = CountryFactory.Instance.CreateCountry(countryCode);
            var code = PostalCodeFactory.Instance.CreatePostalCode(country, shortCode);

            AssertSame(PostalCodeFactory.Instance.CreatePostalCode(country, shortCode + lowestSuffix), code.ExpandPostalCodeAsLowestInRange());
            AssertSame(PostalCodeFactory.Instance.CreatePostalCode(country, shortCode + highestSuffix), code.ExpandPostalCodeAsHighestInRange());
        }

        [Test]
        public void Expand_DoesNotChangeTheShortCode()
        {
            var code = PostalCodeFactory.Instance.CreatePostalCode(CountryFactory.Instance.CreateCountry("US"), "10001");

            code.ExpandPostalCodeAsLowestInRange();
            code.ExpandPostalCodeAsHighestInRange();

            Assert.AreEqual("10001", code.ToString());
            Assert.AreEqual("10001", code.ToHumanReadableString());
            Assert.AreEqual("100010000", code.ExpandPostalCodeAsLowestInRange().ToString());
            Assert.AreEqual("100019999", code.ExpandPostalCodeAsHighestInRange().ToString());
        }

        private static void AssertSame(PostalCode expected, PostalCode actual)
        {
            Assert.AreEqual(expected.GetType(), actual.GetType());
            Assert.AreEqual(expected.ToString(), actual.ToString());
            Assert.AreEqual(expected.ToHumanReadableString(), actual.ToHumanReadableString());
            Assert.AreSame(expected.PostalCodeFormat, actual.PostalCodeFormat);
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected.GetHashCode(), actual.GetHashCode());
            Assert.AreEqual(expected.Predecessor?.ToString(), actual.Predecessor?.ToString());
            Assert.AreEqual(expected.Successor?.ToString(), actual.Successor?.ToString());
            Assert.AreSame(actual, actual.ExpandPostalCodeAsLowestInRange());
            Assert.AreSame(actual, actual.ExpandPostalCodeAsHighestInRange());
        }
    }
}
