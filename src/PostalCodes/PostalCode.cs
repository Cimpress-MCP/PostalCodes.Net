using System;

namespace PostalCodes
{
    /// <summary>
    /// Class PostalCode.
    /// </summary>
    public class PostalCode : IComparable<PostalCode>, IEquatable<PostalCode>, IComparable
    {
        /// <summary>
        /// The _backing postal code
        /// </summary>
        private readonly string _backingPostalCode;
        /// <summary>
        /// Gets the postal code string.
        /// </summary>
        /// <value>The postal code string.</value>
        protected string PostalCodeString
        {
            get { return _backingPostalCode; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostalCode"/> class.
        /// </summary>
        /// <param name="postalCode">The postal code.</param>
        internal PostalCode(string postalCode)
        {
            _backingPostalCode = postalCode;
        }

        /// <summary>
        /// Determines whether [is adjacent to] [the specified other].
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if [is adjacent to] [the specified other]; otherwise, <c>false</c>.</returns>
        public bool IsAdjacentTo(PostalCode other)
        {
            return other != null && Predecessor == other || Successor == other;
        }

        /// <summary>
        /// Gets the predecessor.
        /// </summary>
        /// <value>The predecessor.</value>
        public PostalCode Predecessor
        {
            get { return PredecessorImpl; }
        }

        /// <summary>
        /// Gets the successor.
        /// </summary>
        /// <value>The successor.</value>
        public PostalCode Successor
        {
            get { return SuccessorImpl; }
        }

        /// <summary>
        /// Gets the predecessor implementation.
        /// </summary>
        /// <value>The predecessor implementation.</value>
        protected virtual PostalCode PredecessorImpl
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the successor implementation.
        /// </summary>
        /// <value>The successor implementation.</value>
        protected virtual PostalCode SuccessorImpl
        {
            get { return null; }
        }

        #region Implementation of IComparable<in PostalCode>

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.</returns>
        public virtual int CompareTo(PostalCode other)
        {
            return other == null ? 1 : PostalCodeStringComparer.Default.Compare(PostalCodeString, other.PostalCodeString);
        }

        #endregion

        #region Implementation of IEquatable<PostalCode>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(PostalCode other)
        {
            return other != null && PostalCodeStringComparer.Default.Equals(PostalCodeString, other.PostalCodeString);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return this == obj as PostalCode;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return PostalCodeStringComparer.Default.GetHashCode(PostalCodeString);
        }

        #endregion

        #region Implementation of IComparable

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.</returns>
        public int CompareTo(object obj)
        {
            return CompareTo(obj as PostalCode);
        }

        #endregion

        #region Equality Operator Overloads

        /// <summary>
        /// Implements the ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(PostalCode left, PostalCode right)
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
        public static bool operator !=(PostalCode left, PostalCode right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(PostalCode left, PostalCode right)
        {
            if (left == null)
            {
                return right != null;
            }
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(PostalCode left, PostalCode right)
        {
            if (left == null)
            {
                return right != null;
            }
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(PostalCode left, PostalCode right)
        {
            if (left == null)
            {
                return true;
            }
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(PostalCode left, PostalCode right)
        {
            if (left == null)
            {
                return right == null;
            }
            return left.CompareTo(right) >= 0;
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return PostalCodeString;
        }

        /// <summary>
        /// Ares the adjacent.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreAdjacent(PostalCode left, PostalCode right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            return left.IsAdjacentTo(right) && right.IsAdjacentTo(left);
        }

        /// <summary>
        /// Gets the next alphanumeric.
        /// </summary>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="getSuccessor">if set to <c>true</c> [get successor].</param>
        /// <param name="expectedLength">The expected length.</param>
        /// <param name="firstPossible">The first possible.</param>
        /// <param name="lastPossible">The last possible.</param>
        /// <returns>System.String.</returns>
        protected static string GetNextAlphanumeric(string postalCode, bool getSuccessor, int expectedLength, string firstPossible, string lastPossible)
        {
            var nextTriggerNumber = getSuccessor ? '9' : '0';
            var nextTriggerLetter = getSuccessor ? 'Z' : 'A';

            var radix = expectedLength - 1;
            while (radix >= 0 && (postalCode[radix] == nextTriggerNumber || postalCode[radix] == nextTriggerLetter))
            {
                --radix;
            }

            if (radix < 0)
            {
                return null;
            }

            var newChar = (char) (postalCode[radix] + (getSuccessor ? 1 : -1));
            return postalCode.Substring(0, radix) + newChar + (getSuccessor ? firstPossible : lastPossible).Substring(radix + 1);
        }
    }
}
