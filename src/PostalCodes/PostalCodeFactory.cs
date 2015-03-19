using System;
using System.Text.RegularExpressions;
using PostalCodes.CountrySpecificPostalCodes;

namespace PostalCodes
{
    /// <summary>
    /// Factory implementation for PostalCode
    /// </summary>
    public class PostalCodeFactory
    {
        private static readonly Lazy<PostalCodeFactory> LazyFactory = new Lazy<PostalCodeFactory>(() => new PostalCodeFactory());

        internal PostalCodeFactory()
        {
        }

        /// <summary>
        /// Gets an instance of PostalCodeFactory
        /// </summary>
        public static PostalCodeFactory Instance
        {
            get { return LazyFactory.Value; }
        }

        /// <summary>
        /// Creates a PostalCode object using the provided Country and postal code
        /// </summary>
        /// <param name="country">Country associated with the provided postal code</param>
        /// <param name="postalCode">Postal code value</param>
        /// <returns>A PostalCode representing the provided values</returns>
        public PostalCode CreatePostalCode(Country country, string postalCode)
        {
            if (postalCode == null)
            {
                return null;
            }

            var normalized = postalCode.Replace(" ", "").Replace("-", "");

            // List in this switch only the exceptions
            if (country.Equals(Country.Canada))
            {
                return new CAPostalCode(normalized);
            }
            if (country.Equals(Country.GreatBritain))
            {
                return new GBPostalCode(normalized);
            }
            if (country.Equals(Country.Netherlands))
            {
                return new NLPostalCode(normalized);
            }
            if (country.Equals(Country.Portugal))
            {
                return new PTPostalCode(postalCode);
            }
            if (country.Equals(Country.Malta))
            {
                return new MTPostalCode(postalCode);
            }

            // Default behavior so far was to just return NumericPostalCode
            return new NumericPostalCode(normalized);
        }
    }
}
