using System;
using System.Collections.Generic;

namespace PostalCodes
{
    /// <summary>
    /// Class PostalCodeRange.
    /// </summary>
    public class PostalCodeRange : IEquatable<PostalCodeRange>, IComparable<PostalCodeRange>, IComparable
    {
        /// <summary>
        /// The lazy default
        /// </summary>
        private static readonly Lazy<PostalCodeRange> LazyDefault = new Lazy<PostalCodeRange>(() => new PostalCodeRange(null, null));

        private PostalCode _start;
        private PostalCode _end;

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static PostalCodeRange Default
        {
            get { return LazyDefault.Value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostalCodeRange"/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public PostalCodeRange(PostalCode start, PostalCode end)
        {
            if (start != null && end != null && start.GetType() != end.GetType())
            {
                throw new ArgumentException(String.Format(
                    "The start and the end of the range are from incompatible types ('{0}' & '{1}')",
                    start.GetType(), end.GetType()));
            }

            if (end != null && start != null && start > end)
            {
                throw new ArgumentException(String.Format(
                    "PostalCodeRange end ({0}) can't be before start ({1})", end, start));
            }

            _start = start != null ? start.ExpandPostalCodeAsLowestInRange() : null;
            _end = end != null ? end.ExpandPostalCodeAsHighestInRange() : null;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is indefinite.
        /// </summary>
        /// <value><c>true</c> if this instance is indefinite; otherwise, <c>false</c>.</value>
        public bool IsIndefinite
        {
            get { return !StartDefined || !EndDefined; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public bool IsDefault
        {
            get { return !StartDefined && !EndDefined; }
        }

        /// <summary>
        /// Gets the start.
        /// </summary>
        /// <value>The start.</value>
        public PostalCode Start
        {
            get { return _start; }
            set { _start = value != null ? value.ExpandPostalCodeAsLowestInRange() : null; }
        }

        /// <summary>
        /// Gets the end.
        /// </summary>
        /// <value>The end.</value>
        public PostalCode End
        {
            get { return _end; }
            set { _end = value != null ? value.ExpandPostalCodeAsHighestInRange() : null; }
        }

        /// <summary>
        /// Gets the predecessor postal code.
        /// </summary>
        /// <value>The predecessor postal code.</value>
        public PostalCode PredecessorPostalCode
        {
            get { return StartDefined ? Start.Predecessor : null; }
        }

        /// <summary>
        /// Gets the successor postal code.
        /// </summary>
        /// <value>The successor postal code.</value>
        public PostalCode SuccessorPostalCode
        {
            get { return EndDefined ? End.Successor : null; }
        }

        /// <summary>
        /// Gets a value indicating whether [start defined].
        /// </summary>
        /// <value><c>true</c> if [start defined]; otherwise, <c>false</c>.</value>
        public bool StartDefined
        {
            get { return Start != null; }
        }

        /// <summary>
        /// Gets a value indicating whether [end defined].
        /// </summary>
        /// <value><c>true</c> if [end defined]; otherwise, <c>false</c>.</value>
        public bool EndDefined
        {
            get { return End != null; }
        }

        #region IComparable Members

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.</returns>
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
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.</returns>
        public int CompareTo(PostalCodeRange other)
        {
            // prefer (evaluate as smaller) the higher start
            if (other == null)
            {
                return 1;
            }

            if (Start == null && other.Start != null)
            {
                // Default range (Start=End=Null) must be before any other
                return -1;
            }

            if (Start != null)
            {
                // prefer (evaluate as smaller) the lower start
                var comparison = Start.CompareTo(other.Start);
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

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(PostalCodeRange other)
        {
            if (other == null)
            {
                return false;
            }

            return Start == other.Start && End == other.End;
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
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

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return (StartDefined ? Start.GetHashCode() * 467 : 0) + (EndDefined ? End.GetHashCode() * 487 : 0);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as PostalCodeRange);
        }

        /// <summary>
        /// Implements the &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(PostalCodeRange left, PostalCodeRange right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(PostalCodeRange left, PostalCodeRange right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(PostalCodeRange left, PostalCodeRange right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(PostalCodeRange left, PostalCodeRange right)
        {
            return left.CompareTo(right) >= 0;
        }

        #region Set Operations

        /// <summary>
        /// Ares the adjacent.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreAdjacent(PostalCodeRange left, PostalCodeRange right)
        {
            return PostalCode.AreAdjacent(left.Start, right.End) 
                || PostalCode.AreAdjacent(left.End, right.Start);
        }

        /// <summary>
        /// Intersectses the range.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool IntersectsRange(PostalCodeRange left, PostalCodeRange right)
        {
            return Contains(left, right.Start) || Contains(left, right.End) || Contains(right, left.Start) || Contains(right, left.End);
        }

        /// <summary>
        /// Ares the coincident.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Ares the overlapping.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreOverlapping(PostalCodeRange left, PostalCodeRange right)
        {
            return Contains(left, right) || Contains(right, left) || AreCoincident(left, right);
        }

        /// <summary>
        /// Determines whether [contains] [the specified range].
        /// </summary>
        /// <param name="range">The range.</param>
        /// <param name="specificCode">The specific code.</param>
        /// <returns><c>true</c> if [contains] [the specified range]; otherwise, <c>false</c>.</returns>
        public static bool Contains(PostalCodeRange range, PostalCode specificCode)
        {
            if (specificCode == null)
            {
                return range.IsDefault;
            }
            if ((range.Start != null && !range.Start.ValidateFormatCompatibility(specificCode)) 
                || (range.End != null && !range.End.ValidateFormatCompatibility(specificCode)))
            {
                return false;
            }

            return range.IsDefault ||
                   ((range.Start <= specificCode.LowestExpandedPostalCodeString) && (range.End >= specificCode.HighestExpandedPostalCodeString));
        }

        /// <summary>
        /// Determines whether [contains] [the specified outer].
        /// </summary>
        /// <param name="outer">The outer.</param>
        /// <param name="inner">The inner.</param>
        /// <returns><c>true</c> if [contains] [the specified outer]; otherwise, <c>false</c>.</returns>
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
            if ((outer.StartDefined && !outer.Start.ValidateFormatCompatibility(inner.Start)) 
                || (outer.EndDefined && !outer.End.ValidateFormatCompatibility(inner.End)))
            {
                return false;
            }

            if (outer.StartDefined && outer.EndDefined)
            {
                if (inner.StartDefined && inner.EndDefined)
                    return outer.Start.CompareTo(inner.Start) <= 0 && outer.End.CompareTo(inner.End) >= 0;
                return false;
            }

            if (outer.StartDefined)
                return inner.StartDefined && outer.Start.CompareTo(inner.Start) <= 0;
            
            if (outer.EndDefined)
                return inner.EndDefined && outer.End.CompareTo(inner.End) >= 0;

            // outer == default
            return true;
        }

        /// <summary>
        /// Determines whether the range contains substractFrom postal code.
        /// </summary>
        /// <param name="postalCode">The postal code.</param>
        /// <returns><c>true</c> if range contains the specified postal code; otherwise, <c>false</c>.</returns>
        public bool Contains(PostalCode postalCode)
        {
            return Contains(this, postalCode);
        }

        /// <summary>
        /// Implements set substraction operation (A-B = all elements existing in A but not in B)
        /// </summary>
        /// <param name="substractFrom"></param>
        /// <param name="substractWhat"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IEnumerable<PostalCodeRange> Substract(PostalCodeRange substractFrom, PostalCodeRange substractWhat)
        {
            if (Contains(substractWhat, substractFrom) || substractFrom.Equals(substractWhat))
            {
                // the result is empty range
            }
            else
            {
                var contains = Contains(substractFrom, substractWhat);
                var intersect = IntersectsRange(substractFrom, substractWhat);

                if (contains || intersect)
                {
                    // a1 B1 B2 a2 | a1 B1 a2 B2
                    if (substractWhat.StartDefined && (contains || (substractFrom.Start <= substractWhat.Start)))
                    {
                        var newEnd = substractWhat.Start.Predecessor;
                        if (newEnd == null)
                        {
                            throw new InvalidOperationException(String.Format("Breaking up range {0}, can't figure out postal code immediately after: {1}",
                                substractFrom, substractWhat.Start));
                        }
                        if (substractFrom.Start <= newEnd)
                        {
                            yield return new PostalCodeRange(substractFrom.Start, newEnd);
                        }
                    }

                    // a1 B1 B2 a2 |  B1 a1 B2 a2
                    if (substractWhat.EndDefined && (contains || (substractWhat.Start <= substractFrom.Start)))
                    {
                        var newStart = substractWhat.End.Successor;
                        if (newStart == null)
                        {
                            throw new InvalidOperationException(String.Format("Breaking up range {0}, can't figure out postal code immediately after: {1}",
                                substractFrom, substractWhat.End));
                        }

                        if ((substractFrom.EndDefined && (newStart <= substractFrom.End)) || (!substractFrom.EndDefined))
                        {
                            yield return new PostalCodeRange(newStart, substractFrom.End);
                        }
                    }
                }
                else
                {
                    yield return substractFrom;
                }
            }
        }

        /// <summary>
        /// Combines the with check.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>PostalCodeRange.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
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

        /// <summary>
        /// Combines the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>PostalCodeRange.</returns>
        public static PostalCodeRange Combine(PostalCodeRange left, PostalCodeRange right)
        {
            var newStart = left.Start < right.Start ? left.Start : right.Start;
            var newEnd = left.End > right.End ? left.End : right.End;
            return new PostalCodeRange(newStart, newEnd);
        }
        #endregion
    }
}