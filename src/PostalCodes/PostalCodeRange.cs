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
			if (start != null && end != null && start.GetType() != end.GetType()) {
				throw new ArgumentException(String.Format(
					"The star and the end of the range are from incompatible types ('{0}' & '{1}')",
					start.GetType(), end.GetType()));
			}

            if (end != null && start != null && start > end)
            {
                throw new ArgumentException(String.Format(
					"PostalCodeRange end ({0}) can't be before start ({1})", end, start));
            }

            Start = start;
            End = end;
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
        public PostalCode Start { get; private set; }

        /// <summary>
        /// Gets the end.
        /// </summary>
        /// <value>The end.</value>
        public PostalCode End { get; private set; }

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

            var eqStart = ((Start != null) && Start.Equals(other.Start)) || ((Start == null) && (other.Start == null));
            var eqEnd = ((End != null) && End.Equals(other.End)) || ((End == null) && (other.End == null));

            return eqStart && eqEnd;
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
                return range.IsDefault;

            return range.IsDefault 
                || ( (range.Start <= specificCode) && ((specificCode <= range.End) || (range.End == null))) ||
                   (((range.Start <= specificCode) || (range.End == null)) && (specificCode <= range.End));
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
            return outer.Start.CompareTo(inner.Start) <= 0 && outer.End.CompareTo(inner.End) >= 0;
        }

        /// <summary>
        /// Resects the specified range.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <param name="toResect">To resect.</param>
        /// <returns>IEnumerable&lt;PostalCodeRange&gt;.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// </exception>
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