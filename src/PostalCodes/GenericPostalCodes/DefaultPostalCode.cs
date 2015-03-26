using System.Text.RegularExpressions;

namespace PostalCodes.GenericPostalCodes
{
    internal class DefaultPostalCode : AlphaNumericPostalCode {

        public DefaultPostalCode(string postalCode)
            : base(_formats, postalCode, true)
        {
        }

        public DefaultPostalCode(string postalCode, bool allowConvertToShort)
            : base(_formats, postalCode, allowConvertToShort)
        {
        }

        /// <summary>
        /// Gets the predecessor implementation.
        /// </summary>
        /// <value>The predecessor implementation.</value>
        protected override PostalCode PredecessorImpl
        {
            get
            {
                var b = GenerateSuccesorOrPredecessor(GetInternalValue(), false);
                if (b == null)
                {
                    return null;
                }
                return CreatePostalCode(b, _allowConvertToShort);
            }
        }

        /// <summary>
        /// Gets the successor implementation.
        /// </summary>
        /// <value>The successor implementation.</value>
        protected override PostalCode SuccessorImpl
        {
            get
            {
                var b = GenerateSuccesorOrPredecessor(GetInternalValue(), true);
                if (b == null)
                {
                    return null;
                }
                return CreatePostalCode(b, _allowConvertToShort);
            }
        }

        protected override PostalCode CreatePostalCode(string code, bool allowConvertToShort)
        {
            return new DefaultPostalCode(code, allowConvertToShort);
        }

        public override string ToHumanReadableString()
        {
            return ToString();
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
