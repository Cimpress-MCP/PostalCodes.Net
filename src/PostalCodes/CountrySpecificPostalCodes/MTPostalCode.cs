using System;
using System.Text.RegularExpressions;

namespace PostalCodes.CountrySpecificPostalCodes
{
    internal class MTPostalCode : PostalCode
    {
        private static readonly Regex Malta = new Regex("^[A-Z]{3}[0-9]{4}$", RegexOptions.Compiled);

        private const int MaltesePostalCodeLength = 7;
        private const string FirstPossibleMaltesePostalCode = "AAA0000";
        private const string LastPossibleMaltesePostalCode = "ZZZ9999";

        internal MTPostalCode(string postalCode) : base(Validate(Normalize(postalCode))) { }

        private static string Normalize(string postalCode)
        {
            return Regex.Replace(postalCode, "[ -]", "");
        }

        private static string Validate(string postalCode)
        {
            if (!Malta.Match(postalCode).Success)
            {
                throw new ArgumentException(String.Format("Postal code {0} is not a valid MT postal code", postalCode));
            }
            return postalCode;
        }

        protected override PostalCode PredecessorImpl
        {
            get { return GetMaltesePostalCodeInSequence(PostalCodeString, false); }
        }

        protected override PostalCode SuccessorImpl
        {
            get { return GetMaltesePostalCodeInSequence(PostalCodeString, true); }
        }

        private static PostalCode GetMaltesePostalCodeInSequence(string postalCode, bool getSuccessor)
        {
            var next = GetNextAlphanumeric(postalCode, getSuccessor, MaltesePostalCodeLength, FirstPossibleMaltesePostalCode, LastPossibleMaltesePostalCode);
            return next == null ? null : new MTPostalCode(next);
        }
    }
}
