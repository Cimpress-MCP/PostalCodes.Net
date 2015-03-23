using System;
using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class PTPostalCode : AlphaNumericPostalCode
    {
        public PTPostalCode(string postalCode) : this(postalCode, true) {}

        public PTPostalCode(string postalCode, bool allowConvertToShort) : base(_formats, postalCode, allowConvertToShort) 
        {
            _countryName = "Portugal";
        }

        private PTPostalCode(PostalCode other) : base(_formats, other.ToString()) {}
        
        protected override PostalCode PredecessorImpl
        {
            get 
            { 
                var b = GenerateSuccesorOrPredecessor(GetInternalValue(), false);
                if (b == null) 
                {
                    return null;
                }
                return new PTPostalCode (b, _allowConvertToShort);
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
                return new PTPostalCode (b, _allowConvertToShort);
            }
        }

        public override bool Equals (object obj)
        {
            var other = obj as PTPostalCode;
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
                    return new PTPostalCode (ToString () + _currentFormat.ShortExpansionAsLowestInRange, false);
                } else {
                    throw new ArgumentException ("Requested short postal code expansion but no expansion provided (lowest)");
                }
            }

            return new PTPostalCode(ToString());
        }

        public override PostalCode ExpandPostalCodeAsHighestInRange ()
        {
            if (_currentFormatType == FormatType.Short) {
                if (_currentFormat.ShortExpansionAsHighestInRange != null) {
                    return new PTPostalCode (ToString () + _currentFormat.ShortExpansionAsHighestInRange, false);
                } else {
                    throw new ArgumentException ("Requested short postal code expansion but no expansion provided (highest)");
                }
            }

            return new PTPostalCode(ToString());
        }

        private static PostalCodeFormat[] _formats = new [] {
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
