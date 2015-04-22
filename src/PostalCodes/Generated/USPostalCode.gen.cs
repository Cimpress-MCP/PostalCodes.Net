using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class USPostalCode : AlphaNumericPostalCode
    {
        public USPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public USPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : base(_formats, redundantCharacters, postalCode, allowConvertToShort) 
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
                RegexDefault = new Regex("^[0-9]{9}$", RegexOptions.Compiled),
                RegexShort = new Regex("^[0-9]{5}$", RegexOptions.Compiled),
                OutputDefault = "xxxxx-xxxx",
                OutputShort = "xxxxx",
                AutoConvertToShort = true,
                ShortExpansionAsLowestInRange = "0000",
                ShortExpansionAsHighestInRange = "9999",
                LeftPaddingCharacter = "0",
            }
        };
    }
}
