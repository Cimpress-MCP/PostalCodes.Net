using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class BTPostalCode : AlphaNumericPostalCode
    {
        public BTPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public BTPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : this(postalCode, redundantCharacters, allowConvertToShort, true) {}

        internal BTPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort, bool throwOnInvalid) : base(_formats, redundantCharacters, postalCode, allowConvertToShort, throwOnInvalid) 
        {
            _countryName = "BT";
        }
        
        protected override PostalCode CreatePostalCode(string code, bool allowConvertToShort)
        {
            return new BTPostalCode(code, " -", allowConvertToShort);
        }
        
        public override bool Equals (object obj)
        {
            var other = obj as BTPostalCode;
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
                Name = "5-Digits - 99999",
#if NET7_0_OR_GREATER
                RegexDefault = RegexDefault0(),
#else
                RegexDefault = new Regex("^[0-9]{5}$", RegexOptions.Compiled),
#endif
                OutputDefault = "xxxxx",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "0",
                ShortExpansionAsHighestInRange = "9",
                LeftPaddingCharacter = "0",
            }
        };

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[0-9]{5}$")]
        private static partial Regex RegexDefault0();
#endif
    }
}
