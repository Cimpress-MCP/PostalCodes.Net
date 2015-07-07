using System;
using System.Collections.Concurrent;
using System.Linq;

namespace PostalCodes
{
    /// <summary>
    /// Factory implementation for Country
    /// </summary>
    public class CountryFactory : ICountryFactory
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
        /// Retrieves a Country object using the provided country code
        /// </summary>
        /// <param name="countryCode">Country code representing the country</param>
        /// <returns>A Country object</returns>
        public Country CreateCountry(string countryCode)
        {
            var normalizedCountryCode = _countryCodeValidator.GetNormalizedCountryCode(countryCode);
            Iso3166Country iso3166Country;
            if (Iso3166Countries.Countries.TryGetValue(normalizedCountryCode, out iso3166Country) == false)
            {
                throw new ArgumentException(string.Format("Unsupported country code: {0}", countryCode));
            }
            if (iso3166Country.Status != Iso3166CountryCodeStatus.OfficiallyAssigned && iso3166Country.Status != Iso3166CountryCodeStatus.TransitionallyReserved)
            {
                throw new ArgumentException(string.Format("Country: {0}, is in status {1}", countryCode, iso3166Country.Status));
            }
            var countryName = iso3166Country.CountryName;
            return Countries.GetOrAdd(normalizedCountryCode, key => new Country(normalizedCountryCode, countryName));
        }
    }
}
