using System;
using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class CYPostalCode : AlphaNumericPostalCode
    {
        public CYPostalCode(string postalCode) : this(postalCode, true) {}

        public CYPostalCode(string postalCode, bool allowConvertToShort) : base(_formats, postalCode, allowConvertToShort) 
        {
            _countryName = "CY";
        }

        private CYPostalCode(PostalCode other) : base(_formats, other.ToString()) {}
        
        protected override PostalCode PredecessorImpl
        {
            get 
            { 
                var b = GenerateSuccesorOrPredecessor(GetInternalValue(), false);
                if (b == null) 
                {
                    return null;
                }
                return new CYPostalCode (b, _allowConvertToShort);
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
                return new CYPostalCode (b, _allowConvertToShort);
            }
        }

        public override bool Equals (object obj)
        {
            var other = obj as CYPostalCode;
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
                    return new CYPostalCode (ToString () + _currentFormat.ShortExpansionAsLowestInRange, false);
                } else {
                    throw new ArgumentException ("Requested short postal code expansion but no expansion provided (lowest)");
                }
            }

            return new CYPostalCode(ToString());
        }

        public override PostalCode ExpandPostalCodeAsHighestInRange ()
        {
            if (_currentFormatType == FormatType.Short) {
                if (_currentFormat.ShortExpansionAsHighestInRange != null) {
                    return new CYPostalCode (ToString () + _currentFormat.ShortExpansionAsHighestInRange, false);
                } else {
                    throw new ArgumentException ("Requested short postal code expansion but no expansion provided (highest)");
                }
            }

            return new CYPostalCode(ToString());
        }

        private static PostalCodeFormat[] _formats = new [] {
            new PostalCodeFormat {
                Name = "4-Digits - 9999",
                RegexDefault = new Regex("^[0-9]{4}$", RegexOptions.Compiled),
                OutputDefault = "xxxx",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "0",
                ShortExpansionAsHighestInRange = "9",
                LeftPaddingCharacter = "0",
            }
        };
    }
}
