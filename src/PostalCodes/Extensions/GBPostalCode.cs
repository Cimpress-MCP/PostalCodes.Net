using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class GBPostalCode : AlphaNumericPostalCode
    {
        internal override bool ValidateFormatCompatibility(PostalCode other)
        {
            return HasSameFormat(other as GBPostalCode);
        }

        public static bool HasSameFormat(string quotePostalCode, string startPostalCodeRange)
        {
            var code = new GBPostalCode(quotePostalCode);
            var otherCode = new GBPostalCode(startPostalCodeRange);

            return code.HasSameFormat(otherCode);
        }

        public bool HasSameFormat(GBPostalCode other)
        {
            if ( other == null )
            {
                return false;
            }
            return _currentFormat == other._currentFormat;
        }
    }
}
