using System;
using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class GBPostalCode : AlphaNumericPostalCode
    {
        public GBPostalCode(string postalCode) : this(postalCode, true) {}

        public GBPostalCode(string postalCode, bool allowConvertToShort) : base(_formats, postalCode, allowConvertToShort) 
        {
            _countryName = "Great Britain";
        }

        private GBPostalCode(PostalCode other) : base(_formats, other.ToString()) {}
        
        protected override PostalCode PredecessorImpl
        {
            get 
            { 
                var b = GenerateSuccesorOrPredecessor(GetInternalValue(), false);
                if (b == null) 
                {
                    return null;
                }
                return new GBPostalCode (b, _allowConvertToShort);
            }
        }

        protected override PostalCode SuccessorImpl
        {
            get 
            { 
                var b = GenerateSuccesorOrPredecessor(GetInternalValue(), true);
                if (b == null) 
                {
                    return null;
                }
                return new GBPostalCode (b, _allowConvertToShort);
            }
        }

        public override bool Equals (object obj)
        {
            var other = obj as GBPostalCode;
            if (other == null)
                return false;

            return PostalCodeString.Equals (other.PostalCodeString);
        }

        public override int GetHashCode ()
        {
            return PostalCodeString.GetHashCode ();
        }

        public override string ToHumanReadableString ()
        {
            var outputFormat = _currentFormat.OutputDefault;
            if (_currentFormatType == FormatType.Short) 
            {
                if (_currentFormat.OutputShort != null) 
                {
                    outputFormat = _currentFormat.OutputShort;
                }
            }

            return ToHumanReadableString(outputFormat);
        }
        
        public override PostalCode ExpandPostalCodeAsLowestInRange ()
        {
            if (_currentFormatType == FormatType.Short) {
                if (_currentFormat.ShortExpansionAsLowestInRange != null) {
                    return new GBPostalCode (ToString () + _currentFormat.ShortExpansionAsLowestInRange, false);
                } else {
                    throw new ArgumentException ("Requested short postal code expansion but no expansion provided (lowest)");
                }
            }

            return new GBPostalCode(ToString());
        }

        public override PostalCode ExpandPostalCodeAsHighestInRange ()
        {
            if (_currentFormatType == FormatType.Short) {
                if (_currentFormat.ShortExpansionAsHighestInRange != null) {
                    return new GBPostalCode (ToString () + _currentFormat.ShortExpansionAsHighestInRange, false);
                } else {
                    throw new ArgumentException ("Requested short postal code expansion but no expansion provided (highest)");
                }
            }

            return new GBPostalCode(ToString());
        }

        private static PostalCodeFormat[] _formats = new [] {
            new PostalCodeFormat {
                Name = "UK : AA9A 9[AA]",
                RegexDefault = new Regex("^[A-Z][A-Z][0-9][A-Z][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
                RegexShort = new Regex("^[A-Z][A-Z][0-9][A-Z][0-9]$", RegexOptions.Compiled),
                OutputDefault = "xxxx xxx",
                OutputShort = "xxxx x",
                AutoConvertToShort = true,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : A9A 9[AA]",
                RegexDefault = new Regex("^[A-Z][0-9][A-Z][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
                RegexShort = new Regex("^[A-Z][0-9][A-Z][0-9]$", RegexOptions.Compiled),
                OutputDefault = "xxx xxx",
                OutputShort = "xxx x",
                AutoConvertToShort = true,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : A9 9[AA]",
                RegexDefault = new Regex("^[A-Z][0-9][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
                RegexShort = new Regex("^[A-Z][0-9][0-9]$", RegexOptions.Compiled),
                OutputDefault = "xx xxx",
                OutputShort = "xx x",
                AutoConvertToShort = true,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : A99 9[AA]",
                RegexDefault = new Regex("^[A-Z][0-9][0-9][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
                RegexShort = new Regex("^[A-Z][0-9][0-9][0-9]$", RegexOptions.Compiled),
                OutputDefault = "xxx xxx",
                OutputShort = "xxx x",
                AutoConvertToShort = true,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : AA9 9[AA]",
                RegexDefault = new Regex("^[A-Z][A-Z][0-9][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
                RegexShort = new Regex("^[A-Z][A-Z][0-9][0-9]$", RegexOptions.Compiled),
                OutputDefault = "xxx xxx",
                OutputShort = "xxx x",
                AutoConvertToShort = true,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : AA99 9[AA]",
                RegexDefault = new Regex("^[A-Z][A-Z][0-9][0-9][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
                RegexShort = new Regex("^[A-Z][A-Z][0-9][0-9][0-9]$", RegexOptions.Compiled),
                OutputDefault = "xxxx xxx",
                OutputShort = "xxxx x",
                AutoConvertToShort = true,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            }
        };
    }
}
