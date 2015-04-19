using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class PTPostalCode : AlphaNumericPostalCode
    {
        public PTPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public PTPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : base(_formats, redundantCharacters, postalCode, allowConvertToShort) 
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
                RegexDefault = new Regex("^[0-9]{7}$", RegexOptions.Compiled),
                RegexShort = new Regex("^[0-9]{4}$", RegexOptions.Compiled),
                OutputDefault = "xxxx xxx",
                OutputShort = "xxxx",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "000",
                ShortExpansionAsHighestInRange = "999",
                LeftPaddingCharacter = "0",
            }
        };
    }
}
