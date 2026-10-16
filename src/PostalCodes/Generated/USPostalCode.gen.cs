using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class USPostalCode : AlphaNumericPostalCode
    {
        public USPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public USPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : this(postalCode, redundantCharacters, allowConvertToShort, true) {}

        internal USPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort, bool throwOnInvalid) : base(_formats, redundantCharacters, postalCode, allowConvertToShort, throwOnInvalid) 
        {
            _countryName = "United States of America";
        }
        
        protected override PostalCode CreatePostalCode(string code, bool allowConvertToShort)
        {
            return new USPostalCode(code, " -", allowConvertToShort);
        }
        
        public override bool Equals (object obj)
        {
            var other = obj as USPostalCode;
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
                Name = "US : 99999-9999",
#if NET7_0_OR_GREATER
                RegexDefault = RegexDefault0(),
#else
                RegexDefault = new Regex("^[0-9]{9}$", RegexOptions.Compiled),
#endif
#if NET7_0_OR_GREATER
                RegexShort = RegexShort0(),
#else
                RegexShort = new Regex("^[0-9]{5}$", RegexOptions.Compiled),
#endif
                OutputDefault = "xxxxx-xxxx",
                OutputShort = "xxxxx",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "0000",
                ShortExpansionAsHighestInRange = "9999",
                LeftPaddingCharacter = "0",
            }
        };

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[0-9]{9}$")]
        private static partial Regex RegexDefault0();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[0-9]{5}$")]
        private static partial Regex RegexShort0();
#endif
    }
}
