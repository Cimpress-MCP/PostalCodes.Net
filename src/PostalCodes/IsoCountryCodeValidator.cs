using System;

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
            Iso3166Country country;
            try
            {
                country = GetIso3166Country(countryCode);
            }
            catch (ArgumentException)
            {
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
            var isoCountry = GetIso3166Country(countryCode);
            if (isoCountry.NewCountryCodes.Length > 0) {
                return isoCountry.NewCountryCodes[0];
            }
            return isoCountry.Alpha2Code;
        }

        private static Iso3166Country GetIso3166Country(string countryCode)
        {
            if (countryCode == null)
            {
                throw new ArgumentException("Country code must not be null.");
            }

            countryCode = countryCode.Trim();
            countryCode = countryCode.ToUpperInvariant();
            Iso3166Country isoCountry;
            Iso3166Countries.Countries.TryGetValue(countryCode, out isoCountry);
            if (isoCountry == default(Iso3166Country))
            {
                throw new ArgumentException(string.Format("The specified country code is not valid: {0}", countryCode));
            }

            return isoCountry;
        }
    }
}
