using System;
using System.Collections.Generic;

namespace PostalCodes
{
    public class PostalCodeRange : IEquatable<PostalCodeRange>, IComparable<PostalCodeRange>, IComparable
    {
        private static readonly Lazy<PostalCodeRange> LazyDefault = new Lazy<PostalCodeRange>(() => new PostalCodeRange(null, null)); 
        public static PostalCodeRange Default
        {
            get { return LazyDefault.Value; }
        }

        public PostalCodeRange(PostalCode start, PostalCode end)
        {
            if (end != null && start != null && start > end)
            {
                throw new ArgumentException(String.Format("PostalCodeRange end ({0}) can't be before start ({1})", end, start));
            }

            Start = start;
            End = end;
        }

        public bool IsIndefinite
        {
            get { return !StartDefined || !EndDefined; }
        }
        
        public bool IsDefault
        {
            get { return !StartDefined && !EndDefined; }
        }

        public PostalCode Start { get; private set; }
        public PostalCode End { get; private set; }

        public PostalCode PredecessorPostalCode
        {
            get { return StartDefined ? Start.Predecessor : null; }
        }

        public PostalCode SuccessorPostalCode
        {
            get { return EndDefined ? End.Successor : null; }
        }

        public bool StartDefined
        {
            get { return Start != null; }
        }

        public bool EndDefined
        {
            get { return End != null; }
        }

        #region IComparable Members

        int IComparable.CompareTo(object obj)
        {
            return CompareTo(obj as PostalCodeRange);
        }

        #endregion

        #region IComparable<PostalCodeRange> Members

        /// <summary>
        /// This comparer is designed to prefer (i.e. evaluate as smaller) more narrow subset ranges,
        /// where there is no overlap or a partial intersection, the one with the higher start is
        /// preferred.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(PostalCodeRange other)
        {
            // prefer (evaluate as smaller) the higher start
            if (other == null)
            {
                return 1;
            }

            if (Start == null && other.Start != null)
            {
                return 1;
            }

            if (Start != null)
            {
                var comparison = -Start.CompareTo(other.Start);
                if (comparison != 0)
                {
                    return comparison;
                }
            }

            if (End == null && other.End != null)
            {
                return -1;
            }

            // prefer (evaluate as smaller) the lower end
            return End != null ? End.CompareTo(other.End) : 0;
        }

        #endregion

        #region IEquatable<PostalCodeRange> Members

        public bool Equals(PostalCodeRange other)
        {
            if (other == null)
            {
                return false;
            }

            var eqStart = ((Start != null) && Start.Equals(other.Start)) || ((Start == null) && (other.Start == null));
            var eqEnd = ((End != null) && End.Equals(other.End)) || ((End == null) && (other.End == null));

            return eqStart && eqEnd;
        }

        #endregion

        public override string ToString()
        {
            if (IsDefault)
            {
                return "<*>";
            }
            if (Start == null)
            {
                return "<*-" + End + ">";
            }
            if (End == null)
            {
                return "<" + Start + "-*>";
            }
            return "<" + Start + "-" + End + ">";
        }

        public override int GetHashCode()
        {
            return (StartDefined ? Start.GetHashCode() * 467 : 0) + (EndDefined ? End.GetHashCode() * 487 : 0);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }
            return Equals(obj as PostalCodeRange);
        }

        public static bool operator <(PostalCodeRange left, PostalCodeRange right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(PostalCodeRange left, PostalCodeRange right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(PostalCodeRange left, PostalCodeRange right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(PostalCodeRange left, PostalCodeRange right)
        {
            return left.CompareTo(right) >= 0;
        }

        #region Set Operations

        public static bool AreAdjacent(PostalCodeRange left, PostalCodeRange right)
        {
            return PostalCode.AreAdjacent(left.Start, right.End) 
                || PostalCode.AreAdjacent(left.End, right.Start);
        }

        public static bool IntersectsRange(PostalCodeRange left, PostalCodeRange right)
        {
            return Contains(left, right.Start) || Contains(left, right.End) || Contains(right, left.Start) || Contains(right, left.End);
        }

        public static bool AreCoincident(PostalCodeRange left, PostalCodeRange right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            if (left.IsDefault || right.IsDefault)
            {
                return true;
            }
            return IntersectsRange(left, right);
        }

        public static bool AreOverlapping(PostalCodeRange left, PostalCodeRange right)
        {
            return Contains(left, right) || Contains(right, left) || AreCoincident(left, right);
        }

        public static bool Contains(PostalCodeRange range, PostalCode specificCode)
        {
            if (specificCode == null)
                return range.IsDefault;

            return range.IsDefault 
                || ( (range.Start <= specificCode) && ((specificCode <= range.End) || (range.End == null))) ||
                   (((range.Start <= specificCode) || (range.End == null)) && (specificCode <= range.End));
        }

        public static bool Contains(PostalCodeRange outer, PostalCodeRange inner)
        {
            if (outer == null || inner == null)
            {
                return false;
            }
            if (outer.IsDefault)
            {
                return true;
            }
            if (inner.IsDefault)
            {
                return false;
            }
            return outer.Start.CompareTo(inner.Start) <= 0 && outer.End.CompareTo(inner.End) >= 0;
        }

        public static IEnumerable<PostalCodeRange> Resect(PostalCodeRange range, PostalCodeRange toResect)
        {
            if (Equals(toResect, range))
            {
                // If they are equal, then there is no range that doesn't contain the portion to resect.
            }
            else if (!AreCoincident(toResect, range))
            {
                // If the range to resect is not coincident with the base range, then there is nothing to resect.
                yield return range;
            }
            else
            {
                if (toResect.StartDefined && (!range.StartDefined || range.Start < toResect.Start))
                {
                    // If the section to resect starts after the base range, carve out a new range from
                    // the start of the base range to just before the start of the range to resect.
                    var newEnd = toResect.PredecessorPostalCode;
                    if (newEnd == null)
                    {
                        throw new InvalidOperationException(
                            String.Format("Breaking up range {0}, can't figure out postal code immediately before range {1}"
                                ,range, toResect));
                    }
                    yield return new PostalCodeRange(range.Start, newEnd);
                }
                if (toResect.EndDefined && (!range.EndDefined || range.End > toResect.End))
                {
                    // If the section to resect ends before the base range, carve out a new range from
                    // just after the end of the range to resect to the end of the base range.
                    var newStart = toResect.SuccessorPostalCode;
                    if (newStart == null)
                    {
                        throw new InvalidOperationException(
                            String.Format("Breaking up range {0}, can't figure out postal code immediately after range {1}",range, toResect));
                    }
                    yield return new PostalCodeRange(newStart, range.End);
                }
            }
        }

        public static PostalCodeRange CombineWithCheck(PostalCodeRange left, PostalCodeRange right)
        {
            if (!(PostalCode.AreAdjacent(left.Start, right.End) || PostalCode.AreAdjacent(right.Start, left.End))
                && !AreCoincident(left, right))
            {
                throw new InvalidOperationException(
                    String.Format("Can't combine non-adjacent PostalCodeRanges {0} and {1}",left, right));
            }
            return Combine(left, right);
        }

        public static PostalCodeRange Combine(PostalCodeRange left, PostalCodeRange right)
        {
            var newStart = left.Start < right.Start ? left.Start : right.Start;
            var newEnd = left.End > right.End ? left.End : right.End;
            return new PostalCodeRange(newStart, newEnd);

        }
        #endregion
    }
}