using System;

namespace PostalCodes
{
    /// <summary>
    /// Representation of a country
    /// </summary>
    public sealed class Country
    {
        public static readonly Country Canada = new Country("CA");
        public static readonly Country GreatBritain = new Country("GB");
        public static readonly Country Portugal = new Country("PT");
        public static readonly Country Netherlands = new Country("NL");
        public static readonly Country Malta = new Country("MT");

        private readonly string _backingCode;

        /// <summary>
        /// Gets the country code of the country
        /// </summary>
        public string Code { get { return _backingCode; } }

        internal Country() : this("") {}

        internal Country(string code)
        {
            _backingCode = code;
        }

        #region Equals
        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Country;
            if (other == null)
            {
                return false;
            }
            if (Code == null && other.Code == null)
            {
                return true;
            }
            if (Code == null || other.Code == null)
            {
                return false;
            }
            return Code.Equals(other.Code);
        }

        public static bool operator ==(Country left, Country right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            {
                return true;
            }
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(Country left, Country right)
        {
            return !(left == right);
        }

        #endregion

        public override string ToString()
        {
            return Code ?? base.ToString();
        }
    }
}
