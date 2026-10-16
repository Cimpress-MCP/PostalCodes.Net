using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class RUPostalCode : AlphaNumericPostalCode
    {
        public RUPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public RUPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : this(postalCode, redundantCharacters, allowConvertToShort, true) {}

        internal RUPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort, bool throwOnInvalid) : base(_formats, redundantCharacters, postalCode, allowConvertToShort, throwOnInvalid) 
        {
            _countryName = "Russia";
        }
        
        protected override PostalCode CreatePostalCode(string code, bool allowConvertToShort)
        {
            return new RUPostalCode(code, " -", allowConvertToShort);
        }
        
        public override bool Equals (object obj)
        {
            var other = obj as RUPostalCode;
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
                Name = "RU : 999999",
#if NET7_0_OR_GREATER
                RegexDefault = RegexDefault0(),
#else
                RegexDefault = new Regex("^[0-9]{6}$", RegexOptions.Compiled),
#endif
#if NET7_0_OR_GREATER
                RegexShort = RegexShort0(),
#else
                RegexShort = new Regex("^[0-9]{3}$", RegexOptions.Compiled),
#endif
                OutputDefault = "xxxxxx",
                OutputShort = "xxx",
                ShortExpansionAsLowestInRange = "000",
                ShortExpansionAsHighestInRange = "999",
                LeftPaddingCharacter = "0",
            }
        };

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[0-9]{6}$")]
        private static partial Regex RegexDefault0();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[0-9]{3}$")]
        private static partial Regex RegexShort0();
#endif
    }
}
