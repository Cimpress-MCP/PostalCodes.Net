using System;
using System.Runtime.Serialization;

namespace PostalCodes.Contracts
{
    /// <summary>
    /// Represents a particular country postal code
    /// </summary>
    [Serializable]
    [DataContract]
    public class CountryPostalCode : IEquatable<CountryPostalCode>, IComparable<CountryPostalCode>
    {
        // These consts can be used to construct a 'minimum' or 'maximum' CountryPostalCode for a particular
        // country (i.e. for use in lower or upper bound ranges using VP.VPSystem.Range<CountryPostalCode>
        public const string MinPostalCode = "";
        // http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters
        public const string MaxPostalCode = "~~~~~~~~"; //'~' is the highest ASCII printable character

        [DataMember]
        public string CountryCode { get;  private set; }

        [DataMember]
        public string PostalCode { get; private set; }

        public CountryPostalCode(string countryCode, string postalCode)
        {
            CountryCode = countryCode;
            PostalCode = postalCode;
        }

        #region IEquality

        public override bool Equals(object obj)
        {
            return Equals(obj as CountryPostalCode);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(CountryCode, PostalCode).GetHashCode();
        }

        public bool Equals(CountryPostalCode other)
        {
            return this == other;
        }

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

        public static bool operator !=(CountryPostalCode left, CountryPostalCode right)
        {
            return !(left == right);
        }

        #endregion

        #region IComparable<CountryPostalCode> Members

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

        public override string ToString()
        {
            return string.Format("{0}:{1}", CountryCode ?? "null", PostalCode ?? "null");
        }
    }
}