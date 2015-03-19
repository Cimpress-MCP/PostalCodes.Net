using System.Linq;
using NUnit.Framework;

namespace PostalCodes.UnitTests
{
    [TestFixture]
    internal class PostalCodeFactoryTests
    {
        [Test]
        [TestCase("GB", "A1 9ZZ", "A19", "GBPostalCode")]
        [TestCase("PT", "0042", "0042", "PTPostalCode")]
        [TestCase("CA", "A9A9", "A9A9", "CAPostalCode")]
        [TestCase("NL", "0024 ZZ", "0024", "NLPostalCode")]
        [TestCase("MT", "PLA1234", "PLA1234", "MTPostalCode")]
        [TestCase("??", "004", "004", "NumericPostalCode")]
        public void CreatePostalCode_ReturnsCorrectObjectType(string country, string postalCode, string normalizedPostalCode, string objectTypeName)
        {
            var postalCodeFactory = new PostalCodeFactory();
            var postalCodeObject = postalCodeFactory.CreatePostalCode(new Country(country), postalCode);
            
            Assert.AreEqual(objectTypeName, postalCodeObject.GetType().ToString().Split('.').Last());
            Assert.AreEqual(normalizedPostalCode, postalCodeObject.ToString());
        }

        [Test]
        public void TwoPostalCodeObjects_UsingStrings_HaveToShareStrings()
        {
            var country = new string(new char[] { 'D', 'Z' });
            var postalCodeFactory = new PostalCodeFactory();
            var postalCode1 = postalCodeFactory.CreatePostalCode(new Country(country), "12345");
            var postalCode2 = postalCodeFactory.CreatePostalCode(new Country(country), "12345");

            Assert.AreSame(postalCode1.ToString(), postalCode2.ToString(), "Country: " + country.ToUpper());
        }
    }
}
