using System.Text.RegularExpressions;

namespace PostalCodes
{
    /// <summary>
    /// Deprecated. Netherlands postal code validated with the pre-3.1 rules: any four digits (including a leading
    /// zero), any two letters, and left padding with zeros. Created by <see cref="PostalCodeFactory"/> when
    /// <see cref="PostalCodeFactoryOptions.UseLegacyNetherlandsFormat"/> is set.
    /// </summary>
    internal partial class NLLegacyPostalCode : NLPostalCode
    {
        internal NLLegacyPostalCode(string postalCode, bool allowConvertToShort, bool throwOnInvalid) : base(_legacyFormats, postalCode, " -", allowConvertToShort, throwOnInvalid)
        {
        }

        protected override PostalCode CreatePostalCode(string code, bool allowConvertToShort)
        {
            return new NLLegacyPostalCode(code, allowConvertToShort, true);
        }

        // Same Name as the strict format, so consumers keying on PostalCodeFormat.Name see one NL format.
        private static PostalCodeFormat[] _legacyFormats = {
            new PostalCodeFormat {
                Name = "NL : 9999 ZZ",
#if NET7_0_OR_GREATER
                RegexDefault = RegexDefault0(),
                RegexShort = RegexShort0(),
#else
                RegexDefault = new Regex("^[0-9]{4}[A-Z]{2}$", RegexOptions.Compiled),
                RegexShort = new Regex("^[0-9]{4}$", RegexOptions.Compiled),
#endif
                OutputDefault = "xxxx xx",
                OutputShort = "xxxx",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
                LeftPaddingCharacter = "0",
            }
        };

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[0-9]{4}[A-Z]{2}$")]
        private static partial Regex RegexDefault0();

        [GeneratedRegex("^[0-9]{4}$")]
        private static partial Regex RegexShort0();
#endif
    }

    internal partial class NLPostalCode
    {
        internal NLPostalCode(PostalCodeFormat[] formats, string postalCode, string redundantCharacters, bool allowConvertToShort, bool throwOnInvalid) : base(formats, redundantCharacters, postalCode, allowConvertToShort, throwOnInvalid)
        {
            _countryName = "Netherlands";
        }
    }
}
