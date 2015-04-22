using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class RUPostalCode : AlphaNumericPostalCode
    {
        public RUPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public RUPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : base(_formats, redundantCharacters, postalCode, allowConvertToShort) 
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
                RegexDefault = new Regex("^[0-9]{6}$", RegexOptions.Compiled),
                RegexShort = new Regex("^[0-9]{3}$", RegexOptions.Compiled),
                OutputDefault = "xxxxxx",
                OutputShort = "xxx",
                ShortExpansionAsLowestInRange = "000",
                ShortExpansionAsHighestInRange = "999",
                LeftPaddingCharacter = "0",
            }
        };
    }
}
