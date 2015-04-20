using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class NLPostalCode : AlphaNumericPostalCode
    {
        public NLPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public NLPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : base(_formats, redundantCharacters, postalCode, allowConvertToShort) 
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
                RegexDefault = new Regex("^[0-9]{4}[A-Z]{2}$", RegexOptions.Compiled),
                RegexShort = new Regex("^[0-9]{4}$", RegexOptions.Compiled),
                OutputDefault = "xxxx xx",
                OutputShort = "xxxx",
                AutoConvertToShort = true,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
                LeftPaddingCharacter = "0",
            }
        };
    }
}
