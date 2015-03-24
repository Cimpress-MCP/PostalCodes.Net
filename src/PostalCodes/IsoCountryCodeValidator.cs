using System;
using System.Linq;

namespace PostalCodes
{
    /// <summary>
    /// Validates country codes against ISO 3166-1 alpha-2 standards
    /// </summary>
    public class IsoCountryCodeValidator : IIsoCountryCodeValidator
    {
        /// <summary>
        /// Checks whether the provided country code is valid based on ISO 3166-1 alpha-2 standards
        /// </summary>
        /// <param name="countryCode">Country code to be validated</param>
        /// <returns>True if the provided country code is valid</returns>
        public bool Validate(string countryCode)
        {
            string normalizedCountryCode;

            try
            {
                normalizedCountryCode = NormalizeToIso3166p1Alpha2(countryCode);
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            var country = Iso3166Countries.Countries.FirstOrDefault(a => a.Alpha2Code == normalizedCountryCode);
            if (country == default(Iso3166Country)) {
                return false;
            }

            return country.Status != Iso3166CountryCodeStatus.NotUsed
                && country.Status != Iso3166CountryCodeStatus.Unassigned
                && country.Status != Iso3166CountryCodeStatus.UserAssigned;
        }

        /// <summary>
        /// Gets the normalized country code based on ISO 3166-1 alpha-2 standards
        /// </summary>
        /// <param name="countryCode">Country code to be normalized</param>
        /// <returns>Normalized country code</returns>
        public string GetNormalizedCountryCode(string countryCode)
        {
            var normalizedCountryCode = NormalizeToIso3166p1Alpha2(countryCode);
            
            if (!Iso3166Countries.Countries.Any(a => a.Alpha2Code == normalizedCountryCode))
            {
                throw new InvalidOperationException(string.Format("The specified country code is not valid: {0}", countryCode));
            }

            return normalizedCountryCode;
        }

        private static string NormalizeToIso3166p1Alpha2(string countryCode)
        {
            if (countryCode == null)
            {
                throw new InvalidOperationException("Country code must not be null.");
            }

            countryCode = countryCode.Trim();

            if (countryCode.Length != 2)
            {
                throw new InvalidOperationException("Country code must contain exactly two characters.");
            }

            countryCode = countryCode.ToUpperInvariant();
            var isoCountry = Iso3166Countries.Countries.FirstOrDefault (a => a.Alpha2Code == countryCode);
            if (isoCountry != default(Iso3166Country)) {
                if (isoCountry.NewCountryCodes.Length > 0) {
                    return isoCountry.NewCountryCodes[0];
                }
                return isoCountry.Alpha2Code;
            }

            throw new InvalidOperationException("Unknown");
        }
    }
}
