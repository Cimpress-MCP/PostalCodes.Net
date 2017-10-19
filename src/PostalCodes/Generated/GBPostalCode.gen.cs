using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class GBPostalCode : AlphaNumericPostalCode
    {
        public GBPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public GBPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : base(_formats, redundantCharacters, postalCode, allowConvertToShort) 
        {
            _countryName = "Great Britain";
        }
        
        protected override PostalCode CreatePostalCode(string code, bool allowConvertToShort)
        {
            return new GBPostalCode(code, " -", allowConvertToShort);
        }
        
        public override bool Equals (object obj)
        {
            var other = obj as GBPostalCode;
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
                Name = "UK : AA9A 9[AA]",
                RegexDefault = new Regex("^[A-Z][A-Z][0-9][A-Z][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
                RegexShort = new Regex("^[A-Z][A-Z][0-9][A-Z][0-9]$", RegexOptions.Compiled),
                OutputDefault = "xxxx xxx",
                OutputShort = "xxxx x",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : A9A 9[AA]",
                RegexDefault = new Regex("^[A-Z][0-9][A-Z][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
                RegexShort = new Regex("^[A-Z][0-9][A-Z][0-9]$", RegexOptions.Compiled),
                OutputDefault = "xxx xxx",
                OutputShort = "xxx x",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : A9 9[AA]",
                RegexDefault = new Regex("^[A-Z][0-9][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
                RegexShort = new Regex("^[A-Z][0-9][0-9]$", RegexOptions.Compiled),
                OutputDefault = "xx xxx",
                OutputShort = "xx x",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : A99 9[AA]",
                RegexDefault = new Regex("^[A-Z][0-9][0-9][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
                RegexShort = new Regex("^[A-Z][0-9][0-9][0-9]$", RegexOptions.Compiled),
                OutputDefault = "xxx xxx",
                OutputShort = "xxx x",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : AA9 9[AA]",
                RegexDefault = new Regex("^[A-Z][A-Z][0-9][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
                RegexShort = new Regex("^[A-Z][A-Z][0-9][0-9]$", RegexOptions.Compiled),
                OutputDefault = "xxx xxx",
                OutputShort = "xxx x",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : AA99 9[AA]",
                RegexDefault = new Regex("^[A-Z][A-Z][0-9][0-9][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
                RegexShort = new Regex("^[A-Z][A-Z][0-9][0-9][0-9]$", RegexOptions.Compiled),
                OutputDefault = "xxxx xxx",
                OutputShort = "xxxx x",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            }
        };
    }
}
