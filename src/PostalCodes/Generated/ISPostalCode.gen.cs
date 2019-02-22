using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class ISPostalCode : AlphaNumericPostalCode
    {
        public ISPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public ISPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : base(_formats, redundantCharacters, postalCode, allowConvertToShort) 
        {
            _countryName = "IS";
        }
        
        protected override PostalCode CreatePostalCode(string code, bool allowConvertToShort)
        {
            return new ISPostalCode(code, " -", allowConvertToShort);
        }
        
        public override bool Equals (object obj)
        {
            var other = obj as ISPostalCode;
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
                Name = "4-Digits - 999",
                RegexDefault = new Regex("^(IS){0,1}[0-9]{3}$", RegexOptions.Compiled),
                OutputDefault = "xxx",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "0",
                ShortExpansionAsHighestInRange = "9",
                LeftPaddingCharacter = "0",
            }
        };
    }
}
