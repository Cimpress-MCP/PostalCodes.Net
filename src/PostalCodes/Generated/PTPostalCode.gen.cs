using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class PTPostalCode : AlphaNumericPostalCode
    {
        public PTPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public PTPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : this(postalCode, redundantCharacters, allowConvertToShort, true) {}

        internal PTPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort, bool throwOnInvalid) : base(_formats, redundantCharacters, postalCode, allowConvertToShort, throwOnInvalid) 
        {
            _countryName = "Portugal";
        }
        
        protected override PostalCode CreatePostalCode(string code, bool allowConvertToShort)
        {
            return new PTPostalCode(code, " -", allowConvertToShort);
        }
        
        public override bool Equals (object obj)
        {
            var other = obj as PTPostalCode;
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
                Name = "PT : 9999 999",
#if NET7_0_OR_GREATER
                RegexDefault = RegexDefault0(),
#else
                RegexDefault = new Regex("^[0-9]{7}$", RegexOptions.Compiled),
#endif
#if NET7_0_OR_GREATER
                RegexShort = RegexShort0(),
#else
                RegexShort = new Regex("^[0-9]{4}$", RegexOptions.Compiled),
#endif
                OutputDefault = "xxxx xxx",
                OutputShort = "xxxx",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "000",
                ShortExpansionAsHighestInRange = "999",
                LeftPaddingCharacter = "0",
            }
        };

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[0-9]{7}$")]
        private static partial Regex RegexDefault0();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[0-9]{4}$")]
        private static partial Regex RegexShort0();
#endif
    }
}
