namespace PostalCodes
{
    public interface IPostalCodeInputNormalizer
    {
        /// <summary>
        /// Normalizes postal codes for a given country for use in later comparisons; whether the postal code
        /// should be normalized to be the start or end of a range can be specified.
        /// </summary>
        /// <param name="country">Normalizes postal codes based on country</param>
        /// <param name="postalCode">Postal code to be normalized</param>
        /// <param name="start">Specifies whether postal code is start or end range</param>
        /// <returns>Normalized postal code depending on country</returns>
        string Normalize(Country country, string postalCode, bool start);
    }
}