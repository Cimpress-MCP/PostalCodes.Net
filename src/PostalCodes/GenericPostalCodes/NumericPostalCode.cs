using System;

namespace PostalCodes
{
    internal class NumericPostalCode : PostalCode
    {
        private readonly int PostalCodeInt;
        private readonly int MaxPostalCodeInt;
        private readonly int PostalCodeLength;

        public NumericPostalCode(PostalCodeFormat[] formats, string postalCode) : base(formats, postalCode)
        {
        }

        protected override PostalCode PredecessorImpl
        {
            get
            {
                var prev = GenerateSuccesorOrPredecessor (GetInternalValue (), false);
                return (prev == null) ? null : new NumericPostalCode (prev.Value, PostalCodeString.Length, MaxPostalCodeInt);
            }
        }

        protected override PostalCode SuccessorImpl
        {
            get
            {
                var next = GenerateSuccesorOrPredecessor (GetInternalValue (), true);
                return (next == null) ? null : new NumericPostalCode (next.Value, PostalCodeString.Length, MaxPostalCodeInt);
            }
        }

        protected int GetInternalValue() {
            return PostalCodeInt;
        }

        protected int? GenerateSuccesorOrPredecessor(int postalCode, bool getSuccesor) {
            if (getSuccesor) {
                return PostalCodeInt >= MaxPostalCodeInt ? null : PostalCodeInt + 1;
            } else {
                return PostalCodeInt <= 0 ? null : PostalCodeInt - 1;
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
