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
    }
}