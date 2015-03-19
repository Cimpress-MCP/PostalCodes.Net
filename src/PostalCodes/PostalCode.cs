using System;

namespace PostalCodes
{
    public class PostalCode : IComparable<PostalCode>, IEquatable<PostalCode>, IComparable
    {
        private readonly string _backingPostalCode;
        protected string PostalCodeString
        {
            get { return _backingPostalCode; }
        }

        internal PostalCode(string postalCode)
        {
            _backingPostalCode = postalCode;
        }

        public bool IsAdjacentTo(PostalCode other)
        {
            return other != null && Predecessor == other || Successor == other;
        }

        public PostalCode Predecessor
        {
            get { return PredecessorImpl; }
        }

        public PostalCode Successor
        {
            get { return SuccessorImpl; }
        }

        protected virtual PostalCode PredecessorImpl
        {
            get { return null; }
        }

        protected virtual PostalCode SuccessorImpl
        {
            get { return null; }
        }

        #region Implementation of IComparable<in PostalCode>

        public virtual int CompareTo(PostalCode other)
        {
            return other == null ? 1 : PostalCodeStringComparer.Default.Compare(PostalCodeString, other.PostalCodeString);
        }

        #endregion

        #region Implementation of IEquatable<PostalCode>

        public bool Equals(PostalCode other)
        {
            return other != null && PostalCodeStringComparer.Default.Equals(PostalCodeString, other.PostalCodeString);
        }

        public override bool Equals(object obj)
        {
            return this == obj as PostalCode;
        }

        public override int GetHashCode()
        {
            return PostalCodeStringComparer.Default.GetHashCode(PostalCodeString);
        }

        #endregion

        #region Implementation of IComparable

        public int CompareTo(object obj)
        {
            return CompareTo(obj as PostalCode);
        }

        #endregion

        #region Equality Operator Overloads

        public static bool operator ==(PostalCode left, PostalCode right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            {
                return true;
            }
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(PostalCode left, PostalCode right)
        {
            return !(left == right);
        }

        public static bool operator <(PostalCode left, PostalCode right)
        {
            if (left == null)
            {
                return right != null;
            }
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(PostalCode left, PostalCode right)
        {
            if (left == null)
            {
                return right != null;
            }
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(PostalCode left, PostalCode right)
        {
            if (left == null)
            {
                return true;
            }
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(PostalCode left, PostalCode right)
        {
            if (left == null)
            {
                return right == null;
            }
            return left.CompareTo(right) >= 0;
        }

        #endregion

        public override string ToString()
        {
            return PostalCodeString;
        }

        public static bool AreAdjacent(PostalCode left, PostalCode right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            return left.IsAdjacentTo(right) && right.IsAdjacentTo(left);
        }

        protected static string GetNextAlphanumeric(string postalCode, bool getSuccessor, int expectedLength, string firstPossible, string lastPossible)
        {
            var nextTriggerNumber = getSuccessor ? '9' : '0';
            var nextTriggerLetter = getSuccessor ? 'Z' : 'A';

            var radix = expectedLength - 1;
            while (radix >= 0 && (postalCode[radix] == nextTriggerNumber || postalCode[radix] == nextTriggerLetter))
            {
                --radix;
            }

            if (radix < 0)
            {
                return null;
            }

            var newChar = (char) (postalCode[radix] + (getSuccessor ? 1 : -1));
            return postalCode.Substring(0, radix) + newChar + (getSuccessor ? firstPossible : lastPossible).Substring(radix + 1);
        }
    }
}
