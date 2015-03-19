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
            string normalizedCountryCode;

            try
            {
                normalizedCountryCode = NormalizeToIso(countryCode);
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            return CountryCodes.ValidCountryCodes.Contains(normalizedCountryCode);
        }

        /// <summary>
        /// Gets the normalized country code based on ISO 3166-1 alpha-2 standards
        /// </summary>
        /// <param name="countryCode">Country code to be normalized</param>
        /// <returns>Normalized country code</returns>
        public string GetNormalizedCountryCode(string countryCode)
        {
            var normalizedCountryCode = NormalizeToIso(countryCode);
            
            if (!CountryCodes.ValidCountryCodes.Contains(normalizedCountryCode))
            {
                throw new InvalidOperationException(string.Format("The specified country code is not valid: {0}", countryCode));
            }

            return normalizedCountryCode;
        }

        private static string NormalizeToIso(string countryCode)
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

            switch (countryCode)
            {
                // 'AN' split into 3 country codes - CW, SX, and BQ
                // For clients that still use 'AN', assume its CW (the largest of the 3) instead
                case "AN":
                    return "CW";
                case "UK":
                    return "GB";
                default:
                    return countryCode;
            }
        }
    }
}
