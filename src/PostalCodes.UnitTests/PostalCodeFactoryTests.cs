using System.Linq;
using NUnit.Framework;

namespace PostalCodes.UnitTests
{
    [TestFixture]
    internal class PostalCodeFactoryTests
    {
        [Test]
		[TestCase("GB", "A1 9", "A19", "GBPostalCode")] // short
		[TestCase("GB", "A1 9ZZ", "A19ZZ", "GBPostalCode")] // long
		[TestCase("PT", "0042", "0042", "PTPostalCode")]
        [TestCase("CA", "A9A9A9", "A9A9A9", "CAPostalCode")]
        [TestCase("NL", "1024", "1024", "NLPostalCode")] // short NL
        [TestCase("NL", "1024 ZZ", "1024ZZ", "NLPostalCode")] // long NL
        [TestCase("MT", "PLA1234", "PLA1234", "MTPostalCode")]
        [TestCase("??", "004", "004", "DefaultPostalCode")]
        [TestCase("GB", "ec1a 1bb", "EC1A1BB", "GBPostalCode")] // lowercase
        [TestCase("US", "10001-1234", "100011234", "USPostalCode")]
        [TestCase("CA", " k1a 0b1 ", "K1A0B1", "CAPostalCode")]
        [TestCase("NL", "1024- zz", "1024ZZ", "NLPostalCode")]
        public void CreatePostalCode_ReturnsCorrectObjectType(string country, string postalCode, string normalizedPostalCode, string objectTypeName)
        {
            var postalCodeFactory = new PostalCodeFactory();
            var postalCodeObject = postalCodeFactory.CreatePostalCode(new Country(country), postalCode);

            Assert.AreEqual(objectTypeName, postalCodeObject.GetType().Name);
            Assert.AreEqual(normalizedPostalCode, postalCodeObject.ToString());
        }

        [Test]
        public void TwoPostalCodeObjects_UsingStrings_HaveToShareStrings()
        {
            var country = new string(new[] { 'D', 'Z' });
            var postalCodeFactory = new PostalCodeFactory();
            var postalCode1 = postalCodeFactory.CreatePostalCode(new Country(country), "12345");
            var postalCode2 = postalCodeFactory.CreatePostalCode(new Country(country), "12345");

            Assert.AreSame(postalCode1.ToString(), postalCode2.ToString(), "Country: " + country.ToUpper());
        }

        [TestCase("US", null)]
        [TestCase("GB", "")]
        [TestCase("US", "1234a")]
        [TestCase("GB", "EC1A 1BBB")]
        [TestCase("??", "ABCDEFGHIJKLMNOP")] // default format allows at most 15 characters
        public void TryCreatePostalCode_InvalidCode_ReturnsFalse(string country, string postalCode)
        {
            PostalCode result;

            Assert.IsFalse(new PostalCodeFactory().TryCreatePostalCode(new Country(country), postalCode, out result));
            Assert.IsNull(result);
        }

        [TestCase("US", "501", "00501")]
        [TestCase("GB", "ec1a 1bb", "EC1A1BB")]
        [TestCase("BB", "BB12345", "12345")]
        [TestCase("??", "abc-123", "ABC123")]
        public void TryCreatePostalCode_ValidCode_ReturnsNormalizedCode(string country, string postalCode, string normalizedPostalCode)
        {
            PostalCode result;

            Assert.IsTrue(new PostalCodeFactory().TryCreatePostalCode(new Country(country), postalCode, out result));
            Assert.AreEqual(normalizedPostalCode, result.ToString());
        }

        [Test]
        [SetCulture("tr-TR")]
        public void CreatePostalCode_UppercasesIndependentOfCulture()
        {
            // With the Turkish culture, "i".ToUpper() is "İ", which matches no format.
            var postalCode = new PostalCodeFactory().CreatePostalCode(new Country("GB"), "si1 1aa");

            Assert.AreEqual("SI11AA", postalCode.ToString());
        }
    }
}
