using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class CAPostalCode : AlphaNumericPostalCode
    {
        public CAPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public CAPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : base(_formats, redundantCharacters, postalCode, allowConvertToShort) 
        {
            _countryName = "Canada";
        }
        
        protected override PostalCode CreatePostalCode(string code, bool allowConvertToShort)
        {
            return new CAPostalCode(code, " -", allowConvertToShort);
        }
        
        public override bool Equals (object obj)
        {
            var other = obj as CAPostalCode;
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
                Name = "CA : A0A 0A0",
                RegexDefault = new Regex("^[A-Z][0-9][A-Z][0-9][A-Z][0-9]$", RegexOptions.Compiled),
                OutputDefault = "xxx xxx",
            }
        };
    }
}
