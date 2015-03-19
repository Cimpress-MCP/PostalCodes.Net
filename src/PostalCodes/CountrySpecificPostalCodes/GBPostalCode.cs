using System.Text.RegularExpressions;

namespace PostalCodes.CountrySpecificPostalCodes
{
    internal class GBPostalCode : PostalCode
    {
        private static readonly Regex GreatBritain = new Regex("^[A-Z]{1,2}[0-9R][0-9A-Z]?[0-9]$", RegexOptions.Compiled);
        private static readonly Regex GreatBritainFull = new Regex("^[A-Z]{1,2}[0-9R][0-9A-Z]?[0-9][ABD-HJLNP-UW-Z]{2}$", RegexOptions.Compiled);

        public GBPostalCode(string postalCode) : base(Normalize(postalCode)) {}

        protected override PostalCode PredecessorImpl
        {
            get { return GetGbPostalCodeInSequence(PostalCodeString, false); }
        }

        protected override PostalCode SuccessorImpl
        {
            get { return GetGbPostalCodeInSequence(PostalCodeString, true); }
        }

        private static string Normalize(string postalCode)
        {
            // Make sure we dont have spaces and dashes (just a precaution)
            var normalizedCode = Regex.Replace(postalCode, "[ -]", "");

            if (GreatBritain.Match(normalizedCode).Success)
            {
                return normalizedCode;
            }

            if (GreatBritainFull.Match(normalizedCode).Success)
            {
                return normalizedCode.Substring(0, normalizedCode.Length - 2);
            }

            return normalizedCode;
        }

        private static PostalCode GetGbPostalCodeInSequence(string postalCode, bool getSuccessor)
        {
            // Valid formats (http://en.wikipedia.org/wiki/Postcodes_in_the_United_Kingdom)
            //  AA9A 9AA
            //   A9A 9AA
            //    A9 9AA
            //   A99 9AA
            //   AA9 9AA
            //  AA99 9AA

            var nextTriggerNumber = getSuccessor ? '9' : '0';
            var nextTriggerLetter = getSuccessor ? 'Z' : 'A';

            var radix = postalCode.Length - 1;
            var suffix = "";
            while (radix >= 0 && (postalCode[radix] == nextTriggerNumber || postalCode[radix] == nextTriggerLetter))
            {
                if (postalCode[radix] == nextTriggerLetter)
                {
                    suffix = (getSuccessor ? 'A' : 'Z') + suffix;
                }
                else
                {
                    suffix = (getSuccessor ? '0' : '9') + suffix;
                }
                --radix;
            }

            if (radix < 0)
            {
                return null;
            }

            var newChar = (char) (postalCode[radix] + (getSuccessor ? 1 : -1));
            var nextPostalCode = postalCode.Substring(0, radix) + newChar + suffix;
            return new GBPostalCode(nextPostalCode);
        }
    }
}
