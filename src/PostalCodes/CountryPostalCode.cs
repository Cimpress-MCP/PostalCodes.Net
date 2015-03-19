using System;

namespace PostalCodes
{
    /// <summary>
    /// Represents a particular country postal code
    /// </summary>
    public class CountryPostalCode : IEquatable<CountryPostalCode>, IComparable<CountryPostalCode>
    {
        // These consts can be used to construct a 'minimum' or 'maximum' CountryPostalCode for a particular
        // country (i.e. for use in lower or upper bound ranges using VP.VPSystem.Range<CountryPostalCode>
        /// <summary>
        /// The minimum postal code
        /// </summary>
        public const string MinPostalCode = "";
        
        /// <summary>
        /// The maximum postal code. 
        /// http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters
        /// </summary>
        public const string MaxPostalCode = "~~~~~~~~"; //'~' is the highest ASCII printable character

        /// <summary>
        /// Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string CountryCode { get;  private set; }

        /// <summary>
        /// Gets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryPostalCode"/> class.
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <param name="postalCode">The postal code.</param>
        public CountryPostalCode(string countryCode, string postalCode)
        {
            CountryCode = countryCode;
            PostalCode = postalCode;
        }

        #region IEquality

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as CountryPostalCode);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return Tuple.Create(CountryCode, PostalCode).GetHashCode();
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(CountryPostalCode other)
        {
            return this == other;
        }

        /// <summary>
        /// Implements the ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(CountryPostalCode first, CountryPostalCode second)
        {
            if (ReferenceEquals(first, null) && ReferenceEquals(second, null))
            {
                return true;
            }
            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }
            
            return (first.CountryCode == second.CountryCode) && (first.PostalCode == second.PostalCode);
        }

        /// <summary>
        /// Implements the !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(CountryPostalCode left, CountryPostalCode right)
        {
            return !(left == right);
        }

        #endregion

        #region IComparable<CountryPostalCode> Members

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.</returns>
        public int CompareTo(CountryPostalCode other)
        {
            if (other == null)
            {
                return 1;
            }

            var comparison = StringComparer.OrdinalIgnoreCase.Compare(CountryCode, other.CountryCode);

            if (comparison != 0)
            {
                return comparison;
            }

            comparison = StringComparer.OrdinalIgnoreCase.Compare(PostalCode, other.PostalCode);

            return comparison;
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("{0}:{1}", CountryCode ?? "null", PostalCode ?? "null");
        }
    }
}