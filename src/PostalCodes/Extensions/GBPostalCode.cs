using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class GBPostalCode : AlphaNumericPostalCode
    {
        internal override bool ValidateFormatCompatibility(PostalCode other)
        {
            return HasSamePostalCodeFormat(other as GBPostalCode);
        }

        public static bool HasSamePostalCodeFormat(string quotePostalCode, string startPostalCodeRange)
        {
            var code = new GBPostalCode(quotePostalCode);
            var otherCode = new GBPostalCode(startPostalCodeRange);

            return code.HasSamePostalCodeFormat(otherCode);
        }

        public bool HasSamePostalCodeFormat(GBPostalCode other)
        {
            if ( other == null )
            {
                return false;
            }
            return _currentFormat == other._currentFormat;
        }
    }
}
