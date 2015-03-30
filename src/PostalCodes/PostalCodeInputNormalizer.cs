using System;

namespace PostalCodes
{
    /// <summary>
    /// Class PostalCodeInputNormalizer.
    /// </summary>
    public class PostalCodeInputNormalizer
    {
        // <returns> false if the GB postalCodeA is not the same format as postalCodeB
        /// <summary>
        /// Checks if gb postal code formats match.
        /// </summary>
        /// <param name="quotePostalCode">The quote postal code.</param>
        /// <param name="startPostalCodeRange">The start postal code range.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Obsolete("Don't use this method, it will be removed. Instead use the Contains() method on the PostalCode to asses the proper relation between zip codes")]
        public static bool CheckIfGBPostalCodeFormatsMatch(string quotePostalCode, string startPostalCodeRange)
        {
            return GBPostalCode.CheckIfGbPostalCodeFormatsMatch(quotePostalCode, startPostalCodeRange);
        }
    }
}