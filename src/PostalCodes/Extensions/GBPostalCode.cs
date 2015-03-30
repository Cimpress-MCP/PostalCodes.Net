using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class GBPostalCode : AlphaNumericPostalCode
    {
        public static bool PostalCodeFormatsMatch(string quotePostalCode, string startPostalCodeRange)
        {
            var code = new GBPostalCode(quotePostalCode);

            return code.PostalCodeFormatsMatch(startPostalCodeRange);
        }

        public bool PostalCodeFormatsMatch(string quotePostalCode)
        {
            var code = new GBPostalCode(quotePostalCode);

            return code._currentFormat == _currentFormat;
        }
    }
}
