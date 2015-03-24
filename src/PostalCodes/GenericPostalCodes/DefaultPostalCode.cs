using System;
using System.Text.RegularExpressions;

namespace PostalCodes.GenericPostalCodes
{
    internal class DefaultPostalCode : AlphaNumericPostalCode {

        public DefaultPostalCode(string postalCode) : base(_formats, postalCode, true)
        {
        }

        public DefaultPostalCode(string postalCode, bool allowConvertToShort) : base(_formats, postalCode, allowConvertToShort)
        {
        }

        protected override PostalCode PredecessorImpl
        {
            get
            {
                var b = GenerateSuccesorOrPredecessor(GetInternalValue(), false);
                if (b == null)
                {
                    return null;
                }
                return new DefaultPostalCode (b);
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
                return new DefaultPostalCode (b);
            }
        }
        public override PostalCode ExpandPostalCodeAsLowestInRange ()
        {
            if (_currentFormatType == FormatType.Short) {
                if (_currentFormat.ShortExpansionAsLowestInRange != null) {
                    return new DefaultPostalCode (ToString () + _currentFormat.ShortExpansionAsLowestInRange);
                }
                throw new ArgumentException ("Requested short postal code expansion but no expansion provided (lowest)");
            }

            return new PTPostalCode(ToString());
        }

        public override PostalCode ExpandPostalCodeAsHighestInRange ()
        {
            if (_currentFormatType == FormatType.Short) {
                if (_currentFormat.ShortExpansionAsHighestInRange != null) {
                    return new DefaultPostalCode (ToString () + _currentFormat.ShortExpansionAsHighestInRange);
                }
                throw new ArgumentException ("Requested short postal code expansion but no expansion provided (highest)");
            }

            return new PTPostalCode(ToString());
        }

        private static readonly PostalCodeFormat[] _formats =
        {
            new PostalCodeFormat {
                Name = "Default Alpha Numeric Format",
                OutputDefault = "xxxxxxxx",
                RegexDefault = new Regex("^[A-Z0-9]{1,15}$", RegexOptions.Compiled),
            }
        };
    }
    
}
