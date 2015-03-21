using System;
using System.Text.RegularExpressions;

namespace PostalCodes
{
    /// <summary>
    /// Class PostalCodeInputNormalizer.
    /// </summary>
    public class PostalCodeInputNormalizer
    {
        /// <summary>
        /// The great britain
        /// </summary>
        private static readonly Regex GreatBritain = new Regex("^[A-Z]{1,2}[0-9R][0-9A-Z]?[0-9]$", RegexOptions.Compiled);
        
        // <returns> false if the GB postalCodeA is not the same format as postalCodeB
        /// <summary>
        /// Checks if gb postal code formats match.
        /// </summary>
        /// <param name="quotePostalCode">The quote postal code.</param>
        /// <param name="startPostalCodeRange">The start postal code range.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CheckIfGBPostalCodeFormatsMatch(string quotePostalCode, string startPostalCodeRange)
        {
            var quotePostalCodeLength = quotePostalCode.Length;

            if (!GreatBritain.Match(quotePostalCode).Success || !GreatBritain.Match(startPostalCodeRange).Success)
            {
                return false;
            }

            if (quotePostalCodeLength != startPostalCodeRange.Length)
            {
                return false;
            }

            // The "In Code" is allways the same, so we don't need to compare it, only the "Out Code"
            var outCodeLength = quotePostalCodeLength - 1;

            // The first digit in all formats is a char so it can be skipped
            for (var i = 1; i < outCodeLength; ++i)
            {
                if (Char.IsDigit(quotePostalCode, i) != Char.IsDigit(startPostalCodeRange, i))
                {
                    return false;
                }
            }

            return true;
        }
    }
}