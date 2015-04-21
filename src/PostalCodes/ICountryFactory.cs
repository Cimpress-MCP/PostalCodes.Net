namespace PostalCodes
{
    /// <summary>
    /// Interface ICountryFactory
    /// </summary>
    public interface ICountryFactory
    {
        /// <summary>
        /// Retrieves a Country object using the provided country code and normalizer
        /// </summary>
        /// <param name="countryCode">Country code representing the country</param>
        /// <returns>A Country object</returns>
        Country CreateCountry(string countryCode);
    }
}