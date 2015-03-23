using System;
using System.Linq;

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
        protected string PostalCodeString
        {
            get { return _backingPostalCode; }
        }

		/// <summary>
		/// The current format.
		/// </summary>
		protected PostalCodeFormat _currentFormat = null;

		/// <summary>
		/// The type of the current format.
		/// </summary>
		protected FormatType _currentFormatType = FormatType.Default;

		/// <summary>
		/// The white space characters.
		/// </summary>
		protected string _whiteSpaceCharacters = " -";

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
		/// <param name="postalCode">Postal code.</param>
		internal PostalCode(PostalCodeFormat[] formats, string postalCode)
			: this(formats, postalCode, true) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PostalCodes.PostalCode"/> class.
		/// </summary>
		/// <param name="formats">Formats.</param>
		/// <param name="postalCode">Postal code.</param>
		/// <param name="allowConvertToShort">If set to <c>true</c> allow convert to short format.</param>
		internal PostalCode(PostalCodeFormat[] formats, string postalCode, bool allowConvertToShort)
        {
			_allowConvertToShort = allowConvertToShort;

			var nonWhiteSpaceCode = ClearWhiteSpaces (postalCode);

			string paddedPostalCode;
			SetMatchingFormat(formats, nonWhiteSpaceCode, out paddedPostalCode);
			if (paddedPostalCode != null) {
				nonWhiteSpaceCode = paddedPostalCode;
			}

			_backingPostalCode = Normalize(nonWhiteSpaceCode);
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
		/// To the human readable string.
		/// </summary>
		/// <returns>The human readable string.</returns>
		public virtual string ToHumanReadableString() 
		{
			return ToString ();
		}

		/// <summary>
		/// Expands the postal code as lowest in range.
		/// </summary>
		/// <returns>The postal code as lowest in range.</returns>
		public abstract PostalCode ExpandPostalCodeAsLowestInRange();

		/// <summary>
		/// Expands the postal code as highest in range.
		/// </summary>
		/// <returns>The postal code as highest in range.</returns>
		public abstract PostalCode ExpandPostalCodeAsHighestInRange();

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
		protected virtual string ToHumanReadableString(string outputFormat) {
			var result = "";

			var normalized = PostalCodeString;
			var normalizedIndex = 0;
			for (var i = 0; i < outputFormat.Length && normalizedIndex < PostalCodeString.Length; i++) 
			{
				result += outputFormat [i] == 'x' ? normalized [normalizedIndex++] : outputFormat [i];
			}

			return result;
		}

		private void SetMatchingFormat(PostalCodeFormat[] formats, string postalCode, out string outPaddedPostalCode) {

			outPaddedPostalCode = null;

			foreach (var fmt in formats) {
				if (fmt.RegexDefault.IsMatch (postalCode)) {
					_currentFormatType = FormatType.Default;
					_currentFormat = fmt;
					if (fmt.IgnoreLeftSubstring != null && postalCode.StartsWith(fmt.IgnoreLeftSubstring)) {
						outPaddedPostalCode = postalCode.Substring (fmt.IgnoreLeftSubstring.Length);
					}
					return;
				}
				if (fmt.RegexShort != null) {
					if (fmt.RegexShort.IsMatch (postalCode)) {
						_currentFormatType = FormatType.Short;
						_currentFormat = fmt;
						if (fmt.IgnoreLeftSubstring != null && postalCode.StartsWith(fmt.IgnoreLeftSubstring)) {
							outPaddedPostalCode = postalCode.Substring (fmt.IgnoreLeftSubstring.Length);
						}
						return;
					}
				}
			}

			// Let's give it a try by left padding...
			foreach (var fmt in formats) {
				if (fmt.LeftPaddingCharacter == null) {
					continue;
				}

				var expectedLength = fmt.OutputDefault.ToCharArray ().Count (a => a == 'x');
				if (expectedLength < postalCode.Length) {
					continue;
				}

				var paddedPostalCode = postalCode.PadLeft (expectedLength, fmt.LeftPaddingCharacter[0]);

				if (fmt.RegexDefault.IsMatch (paddedPostalCode)) {
					_currentFormatType = FormatType.Default;
					_currentFormat = fmt;
					outPaddedPostalCode = paddedPostalCode;
					if (fmt.IgnoreLeftSubstring != null && postalCode.StartsWith(fmt.IgnoreLeftSubstring)) {
						outPaddedPostalCode = paddedPostalCode.Substring (fmt.IgnoreLeftSubstring.Length);
					}
					return;
				}
				if (fmt.RegexShort != null) {
					if (fmt.RegexShort.IsMatch (paddedPostalCode)) {
						_currentFormatType = FormatType.Short;
						_currentFormat = fmt;
						outPaddedPostalCode = paddedPostalCode;
						if (fmt.IgnoreLeftSubstring != null && postalCode.StartsWith(fmt.IgnoreLeftSubstring)) {
							outPaddedPostalCode = paddedPostalCode.Substring (fmt.IgnoreLeftSubstring.Length);
						}
						return;
					}
				}
			}

			throw new ArgumentException ("The input postal code doesn't match any format");
		}

		private string ClearWhiteSpaces(string code) {
			var normalized = code;
			foreach (var c in _whiteSpaceCharacters.ToCharArray()) {
				normalized = normalized.Replace (c + "", "");
			}
			return normalized;
		}

		private string Normalize(string code)
		{
			var normalized = code;
			if (_currentFormat.RegexDefault.IsMatch (normalized)) {

				if (_allowConvertToShort && _currentFormat.AutoConvertToShort) {
						var charsInShortFormat = 0;
					for (var j = 0; j < _currentFormat.OutputShort.Length; j++) {
						if (_currentFormat.OutputShort [j] == 'x') {
							charsInShortFormat++;
						}
					}
					normalized = normalized.Substring (0, charsInShortFormat);
				}
				return normalized;
			}

			if (_currentFormat.RegexShort != null) {
				if (_currentFormat.RegexShort.IsMatch (normalized)) {
					return normalized;
				}
			}
				
			throw new ArgumentException (string.Format ("Failed to normalize postal code '{0}'", code));
		}
    }
}
