using System;
using System.Collections.Generic;

namespace PostalCodes
{
    /// <summary>
    /// Factory implementation for Country
    /// </summary>
    public class CountryFactory
    {
        private static readonly Dictionary<string, Country> Countries = new Dictionary<string, Country>();

        private static readonly Lazy<CountryFactory> LazyFactory =
            new Lazy<CountryFactory>(
                () => new CountryFactory(new IsoCountryCodeValidator()));

        private readonly IIsoCountryCodeValidator CountryCodeValidator;

        internal CountryFactory(IIsoCountryCodeValidator countryCodeValidator)
        {
            CountryCodeValidator = countryCodeValidator;
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
            lock (Countries)
            {
                Country result;
                if (Countries.TryGetValue(countryCode, out result))
                {
                    return result;
                }
            }

            var normalizedCountryCode = CountryCodeValidator.GetNormalizedCountryCode(countryCode);
            
            lock (Countries)
            {
                Country result;
                if (Countries.TryGetValue(normalizedCountryCode, out result))
                {
                    return result;
                }

                var country = new Country(normalizedCountryCode);
                Countries.Add(country.Code, country);
                return country;
            }
        }
    }
}
