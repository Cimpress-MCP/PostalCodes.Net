using System.Text.RegularExpressions;
using System;

namespace PostalCodes
{
    /// <summary>
    /// Postal code format.
    /// </summary>
    public sealed class PostalCodeFormat : IEquatable<PostalCodeFormat>
    {
		
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets or sets the regex default.
        /// </summary>
        /// <value>The regex default.</value>
        public Regex RegexDefault { get; internal set; }

        /// <summary>
        /// Gets or sets the output default.
        /// </summary>
        /// <value>The output default.</value>
        public string OutputDefault { get; internal set; }

        /// <summary>
        /// Gets or sets the regex short.
        /// </summary>
        /// <value>The regex short.</value>
        public Regex RegexShort { get; internal set; }

        /// <summary>
        /// Gets or sets the output short.
        /// </summary>
        /// <value>The output short.</value>
        public string OutputShort { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PostalCodes.PostalCodeFormat"/> auto convert to short.
        /// </summary>
        /// <value><c>true</c> if auto convert to short; otherwise, <c>false</c>.</value>
        public bool AutoConvertToShort { get; set; }

        /// <summary>
        /// Gets or sets the short expansion as lowest in range.
        /// </summary>
        /// <value>The short expansion as lowest in range.</value>
        public string ShortExpansionAsLowestInRange { get; internal set; }

        /// <summary>
        /// Gets or sets the short expansion as highest in range.
        /// </summary>
        /// <value>The short expansion as highest in range.</value>
        public string ShortExpansionAsHighestInRange { get; internal set; }

        /// <summary>
        /// Gets or sets the pad left character.
        /// </summary>
        /// <value>The pad left character.</value>
        public string LeftPaddingCharacter { get; internal set; }

        #region Equal implementation

        /// <summary>
        /// Determines whether the specified <see cref="PostalCodes.PostalCodeFormat"/> is equal to the current <see cref="PostalCodes.PostalCodeFormat"/>.
        /// </summary>
        /// <param name="other">The <see cref="PostalCodes.PostalCodeFormat"/> to compare with the current <see cref="PostalCodes.PostalCodeFormat"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="PostalCodes.PostalCodeFormat"/> is equal to the current
        /// <see cref="PostalCodes.PostalCodeFormat"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(PostalCodeFormat other)
        {
            if ( null == other )
            {
                return false; 
            }

            if ( (RegexDefault == null && other.RegexDefault != null)
                 || (RegexDefault != null && other.RegexDefault == null)
                 || (RegexDefault != null && other.RegexDefault != null
                 && !RegexDefault.ToString().Equals(other.RegexDefault.ToString())) )
            {
                return false;
            }

            if ( (RegexShort == null && other.RegexShort != null)
                 || (RegexShort != null && other.RegexShort == null)
                 || (RegexShort != null && other.RegexShort != null
                 && !RegexShort.ToString().Equals(other.RegexShort.ToString())) )
            {
                return false;
            }

            if ( !String.Equals(Name, other.Name) )
                return false;
            if ( !String.Equals(OutputDefault, other.OutputDefault) )
                return false;
            if ( !String.Equals(OutputShort, other.OutputShort) )
                return false;
            if ( AutoConvertToShort != other.AutoConvertToShort )
                return false;
            if ( !String.Equals(ShortExpansionAsLowestInRange, other.ShortExpansionAsLowestInRange) )
                return false;
            if ( !String.Equals(ShortExpansionAsHighestInRange, other.ShortExpansionAsHighestInRange) )
                return false;
            if ( !String.Equals(LeftPaddingCharacter, other.LeftPaddingCharacter) )
                return false;

            return true;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="PostalCodes.PostalCodeFormat"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
              
            var t1 = new {
                Name, 
                RegexDefault, 
                OutputDefault, 
                RegexShort, 
                OutputShort, 
                AutoConvertToShort, 
                ShortExpansionAsLowestInRange, 
                ShortExpansionAsHighestInRange, 
                LeftPaddingCharacter
            };
            return t1.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="PostalCodes.PostalCodeFormat"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="PostalCodes.PostalCodeFormat"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
        /// <see cref="PostalCodes.PostalCodeFormat"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as PostalCodeFormat);
        }

        #endregion
    }
}

