using System.Text.RegularExpressions;

namespace PostalCodes.CountrySpecificPostalCodes
{
    internal class NLPostalCode : NumericPostalCode
    {
        internal NLPostalCode(string postalCode) : base(string.Intern(Normalize(postalCode))) {}

        protected override PostalCode PredecessorImpl
        {
            get
            {
                var basePostalCode = base.PredecessorImpl;
                return (basePostalCode == null) ? null : new NLPostalCode(basePostalCode.ToString());
            }
        }

        protected override PostalCode SuccessorImpl
        {
            get
            {
                var basePostalCode = base.SuccessorImpl;
                return (basePostalCode == null) ? null : new NLPostalCode(basePostalCode.ToString());
            }
        }

        private static string Normalize(string postalCode)
        {
            // Make sure we dont have spaces and dashes (just a precaution)
            var normalizedCode = Regex.Replace(postalCode, "[ -]", "");

            // the full format is: 9999 ZZ, we care only for the digits
            return (normalizedCode.Length > 4) ? normalizedCode.Substring(0, 4) : normalizedCode;
        }
    }
}
