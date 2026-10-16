using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class DKPostalCode : AlphaNumericPostalCode
    {
        public DKPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public DKPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : this(postalCode, redundantCharacters, allowConvertToShort, true) {}

        internal DKPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort, bool throwOnInvalid) : base(_formats, redundantCharacters, postalCode, allowConvertToShort, throwOnInvalid) 
        {
            _countryName = "DK";
        }
        
        protected override PostalCode CreatePostalCode(string code, bool allowConvertToShort)
        {
            return new DKPostalCode(code, " -", allowConvertToShort);
        }
        
        public override bool Equals (object obj)
        {
            var other = obj as DKPostalCode;
            if (other == null) 
            {
                return false;
            }

            return PostalCodeString.Equals (other.PostalCodeString);
        }

        public override int GetHashCode ()
        {
            return PostalCodeString.GetHashCode ();
        }

        private static PostalCodeFormat[] _formats = {
            new PostalCodeFormat {
                Name = "4-Digits - 9999",
#if NET7_0_OR_GREATER
                RegexDefault = RegexDefault0(),
#else
                RegexDefault = new Regex("^(DK){0,1}[0-9]{4}$", RegexOptions.Compiled),
#endif
                OutputDefault = "xxxx",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "0",
                ShortExpansionAsHighestInRange = "9",
                LeftPaddingCharacter = "0",
            }
        };

#if NET7_0_OR_GREATER
        [GeneratedRegex("^(DK){0,1}[0-9]{4}$")]
        private static partial Regex RegexDefault0();
#endif
    }
}
