using Moq;
using NUnit.Framework;

namespace PostalCodes.UnitTests
{
    [TestFixture]
    internal class CountryFactoryTests
    {
        public static IIsoCountryCodeValidator CreateMockValidator()
        {
            var mockValidator = new Mock<IIsoCountryCodeValidator>();
            mockValidator.Setup(x => x.GetNormalizedCountryCode(It.IsAny<string>())).Returns((string countryCode) => countryCode);
            return mockValidator.Object;
        }
        
        public void CreateCountry_ValidCountryCode_ReturnsValidObject()
        {
            const string countryCode = "aa";

            var countryFactory = new CountryFactory(CreateMockValidator());
            var country = countryFactory.CreateCountry(countryCode);

            Assert.IsNotNull(country);
            Assert.AreEqual(countryCode, country.Code);
        }

        [Test]
        public void CreateCountry_SameCountryCodes_ReturnsSameObjects()
        {
            var countryFactory = new CountryFactory(CreateMockValidator());
            var c1 = countryFactory.CreateCountry("BG");
            var c2 = countryFactory.CreateCountry("BG");
            Assert.AreSame(c1, c2);
        }

        [Test]
        public void CreateCountry_DifferentCountryCodes_ReturnsDifferentObjects()
        {
            var countryFactory = new CountryFactory(CreateMockValidator());
            var c1 = countryFactory.CreateCountry("BG");
            var c2 = countryFactory.CreateCountry("GB");
            Assert.AreNotSame(c1, c2);
        }

        [Test]
        public void TwoCountryObjectsShouldShareTheString()
        {
            var country = new string(new char[] { 'D', 'Z' });
            var countryCopy = new string(new char[] { 'D', 'Z' });
            Assert.AreNotSame(country, countryCopy);
            
            var countryFactory = new CountryFactory(CreateMockValidator());
            var c1 = countryFactory.CreateCountry(country);
            var c2 = countryFactory.CreateCountry(countryCopy);
            
            Assert.AreSame(c1, c2);
            Assert.AreSame(c1.Code, c2.Code);
            Assert.AreEqual(c1.Code, country.ToUpperInvariant());
        }
    }
}
