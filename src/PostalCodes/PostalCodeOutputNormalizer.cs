namespace PostalCodes
{
    /// <summary>
    /// Class PostalCodeOutputNormalizer.
    /// </summary>
    public class PostalCodeOutputNormalizer
    {
        /// <summary>
        /// Normalizes the specified country.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="start">if set to <c>true</c> [start].</param>
        /// <returns>System.String.</returns>
        public string Normalize(string country, string postalCode, bool start)
        {
            if (country == "GB")
            {
                // Put a space before the "1AA" at the end
                return postalCode.Insert(postalCode.Length - 3, " ");
            }
            if (country == "PL")
            {
                // 12-345
                return postalCode.Insert(2, "-");
            }
            if (country == "NL" && !start)
            {
                // Create range that allows for matching two-character suffix
                return string.Format("{0} ZZ", postalCode);
            }
            if (country == "PT")
            {
                // 1234-567
                if (start && postalCode.Substring(4, 3) == "000")
                {
                    // If we are something like 1234-000, drop the -000 so we pick up shipments to 1234 that don't
                    // include an extension.
                    return postalCode.Substring(0, 4);
                }
                return postalCode.Insert(4, "-");
            }
            if (country == "SE" || country == "SK" || country == "CA")
            {
                // 123 45
                return postalCode.Insert(3, " ");
            }
            return postalCode;
        }
    }
}