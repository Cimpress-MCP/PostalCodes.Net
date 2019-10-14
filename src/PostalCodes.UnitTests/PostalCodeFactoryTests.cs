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
    }
}
