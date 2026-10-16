using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class CAPostalCode : AlphaNumericPostalCode
    {
        public CAPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public CAPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : this(postalCode, redundantCharacters, allowConvertToShort, true) {}

        internal CAPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort, bool throwOnInvalid) : base(_formats, redundantCharacters, postalCode, allowConvertToShort, throwOnInvalid) 
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
#if NET7_0_OR_GREATER
                RegexDefault = RegexDefault0(),
#else
                RegexDefault = new Regex("^[A-Z][0-9][A-Z][0-9][A-Z][0-9]$", RegexOptions.Compiled),
#endif
                OutputDefault = "xxx xxx",
            }
        };

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[A-Z][0-9][A-Z][0-9][A-Z][0-9]$")]
        private static partial Regex RegexDefault0();
#endif
    }
}
