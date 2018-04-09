using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PostalCodes
{
    /// <summary>
    /// Class PostalCode.
    /// </summary>
    public abstract class PostalCode : IComparable<PostalCode>, IEquatable<PostalCode>, IComparable
    {
        /// <summary>
        /// Format type.
        /// </summary>
        protected enum FormatType {

            /// <summary>
            /// The default.
            /// </summary>
            Default,

            /// <summary>
            /// The short.
            /// </summary>
            Short
        }

        /// <summary>
        /// The _backing postal code
        /// </summary>
        private readonly string _backingPostalCode;
        
        /// <summary>
        /// Gets the postal code string.
        /// </summary>
        /// <value>The postal code string.</value>
        protected internal string PostalCodeString
        {
            get { return _backingPostalCode; }
        }

        /// <summary>
        /// The lowest-expanded postal code (if the postal code is short)
        /// </summary>
        private readonly string _lowestExpandedPostalCode;

        /// <summary>
        /// Gets the lowest-expanded postal code string.
        /// </summary>
        /// <value>The lowest-expanded postal code string.</value>
        protected internal string LowestExpandedPostalCodeString
        {
            get { return _lowestExpandedPostalCode; }
        }

        /// <summary>
        /// The highest-expanded postal code (if the postal code is short)
        /// </summary>
        private readonly string _highestExpandedPostalCode;

        /// <summary>
        /// Gets the highest-expanded postal code string.
        /// </summary>
        /// <value>The highest-expanded postal code string.</value>
        protected internal string HighestExpandedPostalCodeString
        {
            get { return _highestExpandedPostalCode; }
        }

        /// <summary>
        /// The current format.
        /// </summary>
        protected PostalCodeFormat _currentFormat = null;
        
        /// <summary>
        /// Gets the PostalCodeFormat.
        /// </summary>
        /// <value>The PostalCodeFormat.</value>
        public PostalCodeFormat currentFormat
        {
            get { return _currentFormat; }
        }

        /// <summary>
        /// The type of the current format.
        /// </summary>
        protected FormatType _currentFormatType = FormatType.Default;

        /// <summary>
        /// The white space characters.
        /// </summary>
        protected string _redundantCharacters = " -";

        /// <summary>
        /// The name of the country.
        /// </summary>
        protected string _countryName = "Default";

        /// <summary>
        /// The allow convert to short.
        /// </summary>
        protected bool _allowConvertToShort = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostalCodes.PostalCode"/> class.
        /// </summary>
        /// <param name="formats">Formats.</param>
        /// <param name="redundantCharacters">Characters that are considered insignificant for the meaning of the postal code</param>
        /// <param name="postalCode">Postal code.</param>
        internal PostalCode(PostalCodeFormat[] formats, string redundantCharacters, string postalCode)
            : this(formats, redundantCharacters, postalCode, true) 
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostalCodes.PostalCode"/> class.
        /// </summary>
        /// <param name="formats">Formats.</param>
        /// <param name="redundantCharacters">Characters that are considered insignificant for the meaning of the postal code</param>
        /// <param name="postalCode">Postal code.</param>
        /// <param name="allowConvertToShort">If set to <c>true</c> allow convert to short format.</param>
        internal PostalCode(PostalCodeFormat[] formats, string redundantCharacters, string postalCode, bool allowConvertToShort)
        {
            _allowConvertToShort = allowConvertToShort;
            _redundantCharacters = redundantCharacters;

            var nonWhiteSpaceCode = ClearWhiteSpaces (postalCode);

            string paddedPostalCode = SetMatchingFormat(formats, nonWhiteSpaceCode);
            if (paddedPostalCode != null)
            {
                nonWhiteSpaceCode = paddedPostalCode;
            }

            _backingPostalCode = String.Intern(Normalize(nonWhiteSpaceCode));

            _lowestExpandedPostalCode = ExpandLowest();
            _highestExpandedPostalCode = ExpandHighest();
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
        protected abstract PostalCode PredecessorImpl { get; }
        
        /// <summary>
        /// Gets the successor implementation.
        /// </summary>
        /// <value>The successor implementation.</value>
        protected abstract PostalCode SuccessorImpl { get; }

        /// <summary>
        /// Creates new postal code
        /// </summary>
        /// <param name="code">The postal code to create</param>
        /// <param name="allowConvertToShort">Shows wether converting to short format is allowed</param>
        /// <returns></returns>
        protected abstract PostalCode CreatePostalCode(string code, bool allowConvertToShort);

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

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.</returns>
        public virtual int CompareTo(string other)
        {
            return other == null ? 1 : PostalCodeStringComparer.Default.Compare(PostalCodeString, other);
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
            return Equals(obj as PostalCode);
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
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
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
        /// Implements the &lt;=.
        /// </summary>
        /// <param name="left">The left (as postal code).</param>
        /// <param name="right">The right (as string).</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(PostalCode left, string right)
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
        /// <param name="left">The left (as postal code).</param>
        /// <param name="right">The right (as string).</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(PostalCode left, PostalCode right)
        {
            if (left == null)
            {
                return right == null;
            }
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Implements the &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(PostalCode left, string right)
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
        /// To the human readable string.
        /// </summary>
        /// <returns>The human readable string.</returns>
        public virtual string ToHumanReadableString()
        {
            var outputFormat = _currentFormat.OutputDefault;
            if (_currentFormatType == FormatType.Short)
            {
                if (_currentFormat.OutputShort != null)
                {
                    outputFormat = _currentFormat.OutputShort;
                }
            }

            return ToHumanReadableString(outputFormat);
        }

        private string ExpandLowest()
        {
            if (_currentFormatType == FormatType.Short && _currentFormat.ShortExpansionAsLowestInRange != null)
            {
                return _backingPostalCode + _currentFormat.ShortExpansionAsLowestInRange;
            }

            return _backingPostalCode;
        }

        private string ExpandHighest()
        {
            if (_currentFormatType == FormatType.Short && _currentFormat.ShortExpansionAsHighestInRange != null)
            {
                return _backingPostalCode + _currentFormat.ShortExpansionAsHighestInRange;
            }

            return _backingPostalCode;
        }

        /// <summary>
        /// Expands the postal code as lowest in range.
        /// </summary>
        /// <returns>The postal code as lowest in range.</returns>
        public PostalCode ExpandPostalCodeAsLowestInRange()
        {
            if (_currentFormatType == FormatType.Short)
            {
                if (_currentFormat.ShortExpansionAsLowestInRange != null)
                {
                    return CreatePostalCode(ToString() + _currentFormat.ShortExpansionAsLowestInRange, false);
                }
                else
                {
                    throw new ArgumentException("Requested short postal code expansion but no expansion provided (lowest)");
                }
            }

            return this;
        }

        /// <summary>
        /// Expands the postal code as highest in range.
        /// </summary>
        /// <returns>The postal code as highest in range.</returns>
        public PostalCode ExpandPostalCodeAsHighestInRange()
        {
            if (_currentFormatType == FormatType.Short)
            {
                if (_currentFormat.ShortExpansionAsHighestInRange != null)
                {
                    return CreatePostalCode(ToString() + _currentFormat.ShortExpansionAsHighestInRange, false);
                }
                else
                {
                    throw new ArgumentException("Requested short postal code expansion but no expansion provided (highest)");
                }
            }

            return this;
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
        /// To the human readable string.
        /// Each 'x' will be replaced by postal code char, the rest will be preserved.
        /// Example: Code: A123, Format x x-xx => Result: A 1-23
        /// </summary>
        /// <returns>The human readable string.</returns>
        /// <param name="outputFormat">Output format. </param>
        protected virtual string ToHumanReadableString(string outputFormat)
        {
            var result = "";

            var normalized = PostalCodeString;
            var normalizedIndex = 0;
            for (var i = 0; i < outputFormat.Length && normalizedIndex < PostalCodeString.Length; i++)
            {
                result += outputFormat [i] == 'x' ? normalized [normalizedIndex++] : outputFormat [i];
            }

            return result;
        }

        private string SetMatchingFormat(PostalCodeFormat[] formats, string postalCode)
        {
            foreach (var fmt in formats)
            {
                if (fmt.RegexDefault.IsMatch (postalCode))
                {
                    _currentFormatType = FormatType.Default;
                    _currentFormat = fmt;
                    return null;
                }
                if (fmt.RegexShort != null)
                {
                    if (fmt.RegexShort.IsMatch (postalCode))
                    {
                        _currentFormatType = FormatType.Short;
                        _currentFormat = fmt;
                        return null;
                    }
                }
            }

            // Let's give it a try by left padding...
            foreach (var fmt in formats)
            {
                if (fmt.LeftPaddingCharacter == null)
                {
                    continue;
                }

                string paddedPostalCode;
                
                if (fmt.RegexShort != null && TryOutPadding(postalCode, fmt.OutputShort.ToCharArray().Count(a => a == 'x'), fmt.LeftPaddingCharacter[0], fmt.RegexShort, out paddedPostalCode))
                {
                    _currentFormatType = FormatType.Short;
                    _currentFormat = fmt;
                    return paddedPostalCode;
                }
                
                if (TryOutPadding(postalCode, fmt.OutputDefault.ToCharArray().Count(a => a == 'x'), fmt.LeftPaddingCharacter[0], fmt.RegexDefault, out paddedPostalCode))
                {
                    _currentFormatType = FormatType.Default;
                    _currentFormat = fmt;
                    return paddedPostalCode;
                }
            }

            throw new ArgumentException ("The input postal code doesn't match any format");
        }

        private bool TryOutPadding(string postalCode, int expectedLength, char leftPaddingCharacter, Regex regex, out string paddedPostalCode)
        {
            paddedPostalCode = null;
            if (expectedLength > postalCode.Length)
            {
                var paddedPostalCodeInternal = postalCode.PadLeft(expectedLength, leftPaddingCharacter);
                if (regex.IsMatch(paddedPostalCodeInternal))
                {
                    paddedPostalCode = paddedPostalCodeInternal;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Clears the white spaces.
        /// </summary>
        /// <returns>The white spaces.</returns>
        /// <param name="code">Code.</param>
        protected string ClearWhiteSpaces(string code)
        {
            var normalized = code;
            foreach (var c in _redundantCharacters.ToCharArray())
            {
                normalized = normalized.Replace (c + "", "");
            }
            return normalized;
        }

        /// <summary>
        /// Normalize the specified code.
        /// </summary>
        /// <param name="code">Code.</param>
        protected virtual string Normalize(string code)
        {
            var normalized = code;
            if (_currentFormat.RegexDefault.IsMatch (normalized))
            {

                if (_allowConvertToShort && _currentFormat.AutoConvertToShort)
                {
                    var charsInShortFormat = _currentFormat.OutputShort.Count(c => c == 'x');
                    normalized = normalized.Substring (0, charsInShortFormat);
                }
                return normalized;
            }

            if (_currentFormat.RegexShort != null)
            {
                if (_currentFormat.RegexShort.IsMatch (normalized))
                {
                    return normalized;
                }
            }

            throw new ArgumentException (string.Format ("Failed to normalize postal code '{0}'", code));
        }

        /// <summary>
        /// Validates the format compatibility for comparing purposes
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if the formats can be compared, <c>false</c> otherwise.</returns>
        internal virtual bool ValidateFormatCompatibility(PostalCode other)
        {
            return true;
        }
    }
}
