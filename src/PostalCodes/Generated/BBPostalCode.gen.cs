using System;
using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class BBPostalCode : AlphaNumericPostalCode
    {
        public BBPostalCode(string postalCode) : this(postalCode, true) {}

        public BBPostalCode(string postalCode, bool allowConvertToShort) : base(_formats, postalCode, allowConvertToShort) 
        {
            _countryName = "Barbados";
        }

        private BBPostalCode(PostalCode other) : base(_formats, other.ToString()) {}
        
        protected override PostalCode PredecessorImpl
        {
            get 
            { 
                var b = GenerateSuccesorOrPredecessor(GetInternalValue(), false);
                if (b == null) 
                {
                    return null;
                }
                return new BBPostalCode (b, _allowConvertToShort);
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
                return new BBPostalCode (b, _allowConvertToShort);
            }
        }

        public override bool Equals (object obj)
        {
            var other = obj as BBPostalCode;
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
                    return new BBPostalCode (ToString () + _currentFormat.ShortExpansionAsLowestInRange, false);
                } else {
                    throw new ArgumentException ("Requested short postal code expansion but no expansion provided (lowest)");
                }
            }

            return new BBPostalCode(ToString());
        }

        public override PostalCode ExpandPostalCodeAsHighestInRange ()
        {
            if (_currentFormatType == FormatType.Short) {
                if (_currentFormat.ShortExpansionAsHighestInRange != null) {
                    return new BBPostalCode (ToString () + _currentFormat.ShortExpansionAsHighestInRange, false);
                } else {
                    throw new ArgumentException ("Requested short postal code expansion but no expansion provided (highest)");
                }
            }

            return new BBPostalCode(ToString());
        }

        private static PostalCodeFormat[] _formats = new [] {
            new PostalCodeFormat {
                Name = "BB : 99999",
                RegexDefault = new Regex("^[B]{0,2}[0-9]{5}$", RegexOptions.Compiled),
                OutputDefault = "xxxxx",
            }
        };
    }
}
