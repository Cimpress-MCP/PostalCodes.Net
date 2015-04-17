using System;
using System.Collections.Concurrent;
using System.Linq;

namespace PostalCodes
{
    /// <summary>
    /// Factory implementation for Country
    /// </summary>
    public class CountryFactory
    {
        private static readonly ConcurrentDictionary<string, Country> Countries = new ConcurrentDictionary<string, Country>();

        private static readonly Lazy<CountryFactory> LazyFactory =
            new Lazy<CountryFactory>(() => new CountryFactory(new IsoCountryCodeValidator()));

        private readonly IIsoCountryCodeValidator _countryCodeValidator;

        internal CountryFactory(IIsoCountryCodeValidator countryCodeValidator)
        {
            _countryCodeValidator = countryCodeValidator;
        }

        /// <summary>
        /// Gets an instance of <see cref="CountryFactory"/>
        /// </summary>
        public static CountryFactory Instance
        {
            get { return LazyFactory.Value; }
        }

        /// <summary>
        /// Retrieves a Country object using the provided country code and normalizer
        /// </summary>
        /// <param name="countryCode">Country code representing the country</param>
        /// <returns>A Country object</returns>
        public Country CreateCountry(string countryCode)
        {
            var normalizedCountryCode = _countryCodeValidator.GetNormalizedCountryCode(countryCode);
            var iso3166country = Iso3166Countries.Countries.ToList().FirstOrDefault(a => a.Alpha2Code.Equals(normalizedCountryCode));
            var countryName = iso3166country == null ? normalizedCountryCode : iso3166country.CountryName;
            return Countries.GetOrAdd(normalizedCountryCode, key => new Country(normalizedCountryCode, countryName));
        }
    }
}
