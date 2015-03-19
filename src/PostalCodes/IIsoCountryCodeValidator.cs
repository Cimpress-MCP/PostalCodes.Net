namespace PostalCodes
{
    /// <summary>
    /// Validates country codes against ISO 3166-1 alpha-2 standards
    /// </summary>
    public interface IIsoCountryCodeValidator
    {
        /// <summary>
        /// Checks whether the provided country code is valid based on ISO 3166-1 alpha-2 standards
        /// </summary>
        /// <param name="countryCode">Country code to be validated</param>
        /// <returns>True if the provided country code is valid</returns>
        bool Validate(string countryCode);

        /// <summary>
        /// Gets the normalized country code based on ISO 3166-1 alpha-2 standards
        /// </summary>
        /// <param name="countryCode">Country code to be normalized</param>
        /// <returns>Normalized country code</returns>
        string GetNormalizedCountryCode(string countryCode);
    }
}
