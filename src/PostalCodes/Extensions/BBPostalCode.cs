using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class BBPostalCode : AlphaNumericPostalCode
    {
        protected override string Normalize(string code, FormatType matchedRegex)
        {
            if (!code.StartsWith("BB"))
            {
                return base.Normalize(code, matchedRegex);
            }

            var withoutPrefix = code.Substring(2);
            return base.Normalize(withoutPrefix, MatchCurrentFormat(withoutPrefix));
        }
    }
}
