using System;

namespace PostalCodes
{
    class NumericPostalCode : PostalCode
    {
        private readonly int PostalCodeInt;
        private readonly int MaxPostalCodeInt;
        private readonly int PostalCodeLength;

        public NumericPostalCode(string postalCode) : base(postalCode)
        {
            PostalCodeInt = int.Parse(postalCode);
            MaxPostalCodeInt = ((int)Math.Pow(10,postalCode.Length)) - 1;
            PostalCodeLength = postalCode.Length;
        }

        internal NumericPostalCode(int postalCode, int paddedLength, int maximumPostalCode) : base(postalCode.ToString("D" + paddedLength))
        {
            PostalCodeInt = postalCode;
            MaxPostalCodeInt = maximumPostalCode;
            PostalCodeLength = String.Format("{0}", MaxPostalCodeInt).Length;
        }

        internal NumericPostalCode(int postalCode, int paddedLength) : base(postalCode.ToString("D" + paddedLength))
        {
            PostalCodeInt = postalCode;
            MaxPostalCodeInt = ((int)Math.Pow(10, paddedLength)) - 1;
            PostalCodeLength = paddedLength;
        }

        protected override PostalCode PredecessorImpl
        {
            get
            {
                return PostalCodeInt <= 0 ? null : new NumericPostalCode(PostalCodeInt - 1, PostalCodeLength, MaxPostalCodeInt);
            }
        }

        protected override PostalCode SuccessorImpl
        {
            get
            {
                return PostalCodeInt >= MaxPostalCodeInt ? null : new NumericPostalCode(PostalCodeInt + 1, PostalCodeLength, MaxPostalCodeInt);
            }
        }

        public override int CompareTo(PostalCode other)
        {
            var code = other as NumericPostalCode;
            return code != null ? CompareTo(code) : base.CompareTo(other);
        }

        public int CompareTo(NumericPostalCode other)
        {
            return other == null ? 1 : PostalCodeInt.CompareTo(other.PostalCodeInt);
        }
    }
}
