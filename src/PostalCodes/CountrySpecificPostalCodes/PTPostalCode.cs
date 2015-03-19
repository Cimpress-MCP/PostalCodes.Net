using System.Text.RegularExpressions;

namespace PostalCodes.CountrySpecificPostalCodes
{
    internal class PTPostalCode : NumericPostalCode
    {
        internal PTPostalCode(string postalCode) : base(Normalize(postalCode, null)) {}

        internal PTPostalCode(string postalCode, bool start) : base(Normalize(postalCode, start)) {}

        protected override PostalCode PredecessorImpl
        {
            get
            {
                var basePostalCode = base.PredecessorImpl;
                return (basePostalCode == null) ? null : new PTPostalCode(basePostalCode.ToString());
            }
        }

        protected override PostalCode SuccessorImpl
        {
            get
            {
                var basePostalCode = base.SuccessorImpl;
                return (basePostalCode == null) ? null : new PTPostalCode(basePostalCode.ToString());
            }
        }

        private static string Normalize(string postalCode, bool? start)
        {
            // Make sure we dont have spaces and dashes (just a precaution)
            var normalizedCode = Regex.Replace(postalCode, "[ -]", "");

            // If we dont care for start/end range
            if (start == null)
            {
                return normalizedCode;
            }

            // Otherwise if we have partial range
            if (normalizedCode.Length <= 4)
            {
                return normalizedCode + (start == true ? "000" : "999");
            }

            return normalizedCode;
        }
    }
}
