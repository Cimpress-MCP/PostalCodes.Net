using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    internal partial class GBPostalCode : AlphaNumericPostalCode
    {
        public GBPostalCode(string postalCode) : this(postalCode, " -", true) {}

        public GBPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort) : this(postalCode, redundantCharacters, allowConvertToShort, true) {}

        internal GBPostalCode(string postalCode, string redundantCharacters, bool allowConvertToShort, bool throwOnInvalid) : base(_formats, redundantCharacters, postalCode, allowConvertToShort, throwOnInvalid) 
        {
            _countryName = "Great Britain";
        }
        
        protected override PostalCode CreatePostalCode(string code, bool allowConvertToShort)
        {
            return new GBPostalCode(code, " -", allowConvertToShort);
        }
        
        public override bool Equals (object obj)
        {
            var other = obj as GBPostalCode;
            if (other == null) 
            {
                return false;
            }

            return PostalCodeString.Equals (other.PostalCodeString);
        }

        public override int GetHashCode ()
        {
            return PostalCodeString.GetHashCode ();
        }

        private static PostalCodeFormat[] _formats = {
            new PostalCodeFormat {
                Name = "UK : AA9A 9[AA]",
#if NET7_0_OR_GREATER
                RegexDefault = RegexDefault0(),
#else
                RegexDefault = new Regex("^[A-Z][A-Z][0-9][A-Z][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
#endif
#if NET7_0_OR_GREATER
                RegexShort = RegexShort0(),
#else
                RegexShort = new Regex("^[A-Z][A-Z][0-9][A-Z][0-9]$", RegexOptions.Compiled),
#endif
                OutputDefault = "xxxx xxx",
                OutputShort = "xxxx x",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : A9A 9[AA]",
#if NET7_0_OR_GREATER
                RegexDefault = RegexDefault1(),
#else
                RegexDefault = new Regex("^[A-Z][0-9][A-Z][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
#endif
#if NET7_0_OR_GREATER
                RegexShort = RegexShort1(),
#else
                RegexShort = new Regex("^[A-Z][0-9][A-Z][0-9]$", RegexOptions.Compiled),
#endif
                OutputDefault = "xxx xxx",
                OutputShort = "xxx x",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : A9 9[AA]",
#if NET7_0_OR_GREATER
                RegexDefault = RegexDefault2(),
#else
                RegexDefault = new Regex("^[A-Z][0-9][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
#endif
#if NET7_0_OR_GREATER
                RegexShort = RegexShort2(),
#else
                RegexShort = new Regex("^[A-Z][0-9][0-9]$", RegexOptions.Compiled),
#endif
                OutputDefault = "xx xxx",
                OutputShort = "xx x",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : A99 9[AA]",
#if NET7_0_OR_GREATER
                RegexDefault = RegexDefault3(),
#else
                RegexDefault = new Regex("^[A-Z][0-9][0-9][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
#endif
#if NET7_0_OR_GREATER
                RegexShort = RegexShort3(),
#else
                RegexShort = new Regex("^[A-Z][0-9][0-9][0-9]$", RegexOptions.Compiled),
#endif
                OutputDefault = "xxx xxx",
                OutputShort = "xxx x",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : AA9 9[AA]",
#if NET7_0_OR_GREATER
                RegexDefault = RegexDefault4(),
#else
                RegexDefault = new Regex("^[A-Z][A-Z][0-9][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
#endif
#if NET7_0_OR_GREATER
                RegexShort = RegexShort4(),
#else
                RegexShort = new Regex("^[A-Z][A-Z][0-9][0-9]$", RegexOptions.Compiled),
#endif
                OutputDefault = "xxx xxx",
                OutputShort = "xxx x",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            },
            new PostalCodeFormat {
                Name = "UK : AA99 9[AA]",
#if NET7_0_OR_GREATER
                RegexDefault = RegexDefault5(),
#else
                RegexDefault = new Regex("^[A-Z][A-Z][0-9][0-9][0-9][A-Z][A-Z]$", RegexOptions.Compiled),
#endif
#if NET7_0_OR_GREATER
                RegexShort = RegexShort5(),
#else
                RegexShort = new Regex("^[A-Z][A-Z][0-9][0-9][0-9]$", RegexOptions.Compiled),
#endif
                OutputDefault = "xxxx xxx",
                OutputShort = "xxxx x",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "AA",
                ShortExpansionAsHighestInRange = "ZZ",
            }
        };

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[A-Z][A-Z][0-9][A-Z][0-9][A-Z][A-Z]$")]
        private static partial Regex RegexDefault0();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[A-Z][A-Z][0-9][A-Z][0-9]$")]
        private static partial Regex RegexShort0();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[A-Z][0-9][A-Z][0-9][A-Z][A-Z]$")]
        private static partial Regex RegexDefault1();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[A-Z][0-9][A-Z][0-9]$")]
        private static partial Regex RegexShort1();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[A-Z][0-9][0-9][A-Z][A-Z]$")]
        private static partial Regex RegexDefault2();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[A-Z][0-9][0-9]$")]
        private static partial Regex RegexShort2();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[A-Z][0-9][0-9][0-9][A-Z][A-Z]$")]
        private static partial Regex RegexDefault3();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[A-Z][0-9][0-9][0-9]$")]
        private static partial Regex RegexShort3();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[A-Z][A-Z][0-9][0-9][A-Z][A-Z]$")]
        private static partial Regex RegexDefault4();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[A-Z][A-Z][0-9][0-9]$")]
        private static partial Regex RegexShort4();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[A-Z][A-Z][0-9][0-9][0-9][A-Z][A-Z]$")]
        private static partial Regex RegexDefault5();
#endif

#if NET7_0_OR_GREATER
        [GeneratedRegex("^[A-Z][A-Z][0-9][0-9][0-9]$")]
        private static partial Regex RegexShort5();
#endif
    }
}
