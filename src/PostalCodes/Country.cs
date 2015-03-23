namespace PostalCodes
{
    /// <summary>
    /// Representation of a country
    /// </summary>
    public sealed class Country
    {
        /// <summary>
        /// The _backing code
        /// </summary>
        private readonly string _backingCode;

        /// <summary>
        /// Gets the country code of the country
        /// </summary>
        /// <value>The code.</value>
        public string Code { get { return _backingCode; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Country"/> class.
        /// </summary>
        internal Country() : this("") {}

        /// <summary>
        /// Initializes a new instance of the <see cref="Country"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        internal Country(string code)
        {
            _backingCode = code;
        }

        #region Equals
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Implements the ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
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

        /// <summary>
        /// Implements the !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Country left, Country right)
        {
            return !(left == right);
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Code ?? base.ToString();
        }
    }
}
