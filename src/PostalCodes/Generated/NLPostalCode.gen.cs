using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class NLPostalCode : AlphaNumericPostalCode
    {
        public NLPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public NLPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : this(postalCode, redundantCharacters, allowConvertToShort, true) {}

        internal NLPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort, bool throwOnInvalid) : base(_formats, redundantCharacters, postalCode, allowConvertToShort, throwOnInvalid) 
        {
            _countryName = "Netherlands";
        }
        
        protected override PostalCode CreatePostalCode(string code, bool allowConvertToShort)
        {
            return new NLPostalCode(code, " -", allowConvertToShort);
        }
        
        public override bool Equals (object obj)
        {
            var other = obj as NLPostalCode;
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
                Name = "NL : 9999 ZZ",
#if NET7_0_OR_GREATER
                RegexDefault = RegexDefault0(),
#else
                RegexDefault = new Regex("^[1-9][0-9]{3}(?!SA|SD|SS)[A-Z]{2}$", RegexOptions.Compiled),
#endif
#if NET7_0_OR_GREATER
                RegexShort = RegexShort0(),
#else
                RegexShort = new Regex("^[1-9][0-9]{3}$", RegexOptions.Compiled),
#endif
                OutputDefault = "xxxx xx",
                OutputShort = "xxxx",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            }
        };

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[1-9][0-9]{3}(?!SA|SD|SS)[A-Z]{2}$")]
        private static partial Regex RegexDefault0();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[1-9][0-9]{3}$")]
        private static partial Regex RegexShort0();
#endif
    }
}
