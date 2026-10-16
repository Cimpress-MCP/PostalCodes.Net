namespace PostalCodes
{
    /// <summary>
    /// Interface IPostalCodeFactory
    /// </summary>
    public interface IPostalCodeFactory
    {
        /// <summary>
        /// Creates a PostalCode object using the provided Country and postal code
        /// </summary>
        /// <param name="country">Country associated with the provided postal code</param>
        /// <param name="postalCode">Postal code value</param>
        /// <returns>A PostalCode representing the provided values</returns>
        PostalCode CreatePostalCode(Country country, string postalCode);

        /// <summary>
        /// Creates a PostalCode object using the provided Country and postal code, without throwing when the postal
        /// code matches no format of the country
        /// </summary>
        /// <param name="country">Country associated with the provided postal code</param>
        /// <param name="postalCode">Postal code value</param>
        /// <param name="result">The PostalCode representing the provided values, or null</param>
        /// <returns>True if the postal code is not null and matches a format of the country</returns>
        bool TryCreatePostalCode(Country country, string postalCode, out PostalCode result);
    }
}