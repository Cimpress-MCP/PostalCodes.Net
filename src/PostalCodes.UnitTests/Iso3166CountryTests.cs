using System;
using NUnit.Framework;

namespace PostalCodes
{
    [TestFixture]
    internal class Iso3166CountryTests
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        static private readonly Array TestStatuses;

        static Iso3166CountryTests()
        {
            TestStatuses = Enum.GetValues(typeof(Iso3166CountryCodeStatus));
        }

        [Test, Combinatorial]
        public void Throws_IfCountryCodeDifferentFromTwoLetters([Values("P", "PPP")] String countryCode, [ValueSource("TestStatuses")] Iso3166CountryCodeStatus status)
        {
            Assert.Throws<ArgumentException>(() => new Iso3166Country(countryCode, "Some name", status, new string[0]));
        }

        [Test, Combinatorial]
        public void Throws_IfAnyOfTheNewCountryCodesDifferentFromTwoLetters([Values("P", "PPP")] String countryCode, [ValueSource("TestStatuses")] Iso3166CountryCodeStatus status)
        {
            Assert.Throws<ArgumentException>(() => new Iso3166Country(countryCode, "Some name", status, new string[0]));
        }

        [Test, Combinatorial]
        public void DoesNotThrow_IfCountryCodesAreTwoLetters([Values("PL", "GB")] String newCountryCode, [ValueSource("TestStatuses")] Iso3166CountryCodeStatus status)
        {
            Assert.DoesNotThrow(() => new Iso3166Country("PL", "Some name", status, new[] { newCountryCode }));
        }
    }
}

