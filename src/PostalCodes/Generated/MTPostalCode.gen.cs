using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class MTPostalCode : AlphaNumericPostalCode
    {
        public MTPostalCode(string postalCode) : this(postalCode, true) {}

        public MTPostalCode(string postalCode, bool allowConvertToShort) : base(_formats, postalCode, allowConvertToShort) 
        {
            _countryName = "Malta";
        }
        
        protected override PostalCode CreatePostalCode(string code, bool allowConvertToShort)
        {
            return new MTPostalCode(code, allowConvertToShort);
        }
        
        public override bool Equals (object obj)
        {
            var other = obj as MTPostalCode;
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
                Name = "MA : AAA 1234",
                RegexDefault = new Regex("^[A-Z]{3}[0-9]{4}$", RegexOptions.Compiled),
                OutputDefault = "xxx xxxx",
            }
        };
    }
}
