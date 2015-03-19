namespace PostalCodes.CountrySpecificPostalCodes
{
    internal class CAPostalCode : PostalCode
    {
        private const int CanadianPostalCodeLength = 6;
        private const string FirstPossibleCanadianPostalCode = "A0A0A0";
        private const string LastPossibleCanadianPostalCode = "Z9Z9Z9";

        public CAPostalCode(string postalCode) : base(postalCode) {}

        protected override PostalCode PredecessorImpl
        {
            get { return GetCanadianPostalCodeInSequence(PostalCodeString, false); }
        }

        protected override PostalCode SuccessorImpl
        {
            get { return GetCanadianPostalCodeInSequence(PostalCodeString, true); }
        }

        private static PostalCode GetCanadianPostalCodeInSequence(string postalCode, bool getSuccessor)
        {
            var next = GetNextAlphanumeric(postalCode, getSuccessor, CanadianPostalCodeLength, FirstPossibleCanadianPostalCode, LastPossibleCanadianPostalCode);
            return next == null ? null : new CAPostalCode(next);
        }
    }
}
