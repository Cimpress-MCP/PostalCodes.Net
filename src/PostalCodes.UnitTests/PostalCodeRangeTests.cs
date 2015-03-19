using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace PostalCodes.UnitTests
{
    [TestFixture]
    internal class PostalCodeRangeTests
    {
        private static PostalCodeRange MakeRange(string start, string end)
        {
            var startPc = start != null ? new PostalCode(start) : null;
            var endPc = end != null ? new PostalCode(end) : null;
            return new PostalCodeRange(startPc, endPc);
        }

        public static IEnumerable<ITestCaseData> CoincidesWithTestCases
        {
            get
            {
                var range = MakeRange("C", "E");
                yield return new TestCaseData(range, PostalCodeRange.Default).Returns(true);
                yield return new TestCaseData(range, MakeRange("A", "B")).Returns(false);
                yield return new TestCaseData(range, MakeRange("A", "C")).Returns(true);
                yield return new TestCaseData(range, MakeRange("A", "D")).Returns(true);
                yield return new TestCaseData(range, MakeRange("A", "E")).Returns(true);
                yield return new TestCaseData(range, MakeRange("A", "G")).Returns(true);
                yield return new TestCaseData(range, MakeRange("C", "D")).Returns(true);
                yield return new TestCaseData(range, MakeRange("C", "E")).Returns(true);
                yield return new TestCaseData(range, MakeRange("C", "G")).Returns(true);
                yield return new TestCaseData(range, MakeRange("D", "E")).Returns(true);
                yield return new TestCaseData(range, MakeRange("D", "G")).Returns(true);
                yield return new TestCaseData(range, MakeRange("E", "G")).Returns(true);
                yield return new TestCaseData(range, MakeRange("F", "G")).Returns(false);
            }
        }

        // ReSharper disable InconsistentNaming

        [Test]
        [TestCaseSource("CoincidesWithTestCases")]
        public bool CoincidesWith_compares_full_range(PostalCodeRange left, PostalCodeRange right)
        {
            return PostalCodeRange.AreCoincident(left, right);
        }

        [Test]
        public void Ctor_throws_when_start_after_end()
        {
            Assert.Throws<ArgumentException>(() => MakeRange("67891", "67890"));
        }

        [Test]
        public void Ctor_with_args_sets_start_and_end()
        {
            var start = new PostalCode("12345");
            var end = new PostalCode("67890");

            var range = new PostalCodeRange(start, end);

            Assert.AreEqual(start, range.Start);
            Assert.AreEqual(end, range.End);
        }

        [Test]
        public void Ctor_with_empty_string_makes_full_range()
        {
            var range = new PostalCodeRange(null, null);
            Assert.AreEqual(null, range.Start);
            Assert.AreEqual(null, range.End);
        }

        public static IEnumerable<ITestCaseData> GetHashCodeTestCases
        {
            get
            {
                yield return new TestCaseData(null, null);
                yield return new TestCaseData(new PostalCode("ABCDE"), new PostalCode("FGHIJ"));
                yield return new TestCaseData(new PostalCode("02421"), new PostalCode("12958"));
            }
        }

        [Test]
        [TestCaseSource("GetHashCodeTestCases")]
        public void GetHashCode_returns_equal_for_Equal_PostalCodeRanges(
            PostalCode start,
            PostalCode end)
        {
            var r1 = new PostalCodeRange(start, end);
            var r2 = new PostalCodeRange(start, end);
            Assert.AreEqual(r1.GetHashCode(), r2.GetHashCode());
        }

        public static IEnumerable<ITestCaseData> CompareTo_TestCases
        {
            get
            {
                var range = MakeRange("C", "E");
                yield return new TestCaseData(range, null).Returns(1);
                yield return new TestCaseData(range, PostalCodeRange.Default).Returns(-1);
                yield return new TestCaseData(range, MakeRange("A", "B")).Returns(-1);
                yield return new TestCaseData(range, MakeRange("A", "C")).Returns(-1);
                yield return new TestCaseData(range, MakeRange("A", "D")).Returns(-1);
                yield return new TestCaseData(range, MakeRange("A", "E")).Returns(-1);
                yield return new TestCaseData(range, MakeRange("A", "G")).Returns(-1);
                yield return new TestCaseData(range, MakeRange("C", "D")).Returns(1);
                yield return new TestCaseData(range, MakeRange("C", "E")).Returns(0);
                yield return new TestCaseData(range, MakeRange("C", "G")).Returns(-1);
                yield return new TestCaseData(range, MakeRange("D", "E")).Returns(1);
                yield return new TestCaseData(range, MakeRange("D", "G")).Returns(1);
                yield return new TestCaseData(range, MakeRange("E", "G")).Returns(1);
                yield return new TestCaseData(range, MakeRange("F", "G")).Returns(1);
            }
        }

        [Test]
        [TestCaseSource("CompareTo_TestCases")]
        public int IComparable_CompareTo_compares_starts_then_ends(
            PostalCodeRange range,
            PostalCodeRange other)
        {
            return Math.Sign((range as IComparable).CompareTo(other));
        }

        public static IEnumerable<ITestCaseData> Equals_TestCases
        {
            get
            {
                var range = MakeRange("C", "E");
                yield return new TestCaseData(range, null).Returns(false);
                yield return new TestCaseData(range, PostalCodeRange.Default).Returns(false);
                yield return new TestCaseData(range, MakeRange("A", "B")).Returns(false);
                yield return new TestCaseData(range, MakeRange("A", "C")).Returns(false);
                yield return new TestCaseData(range, MakeRange("A", "D")).Returns(false);
                yield return new TestCaseData(range, MakeRange("A", "E")).Returns(false);
                yield return new TestCaseData(range, MakeRange("A", "G")).Returns(false);
                yield return new TestCaseData(range, MakeRange("C", "D")).Returns(false);
                yield return new TestCaseData(range, MakeRange("C", "E")).Returns(true);
                yield return new TestCaseData(range, MakeRange("C", "G")).Returns(false);
                yield return new TestCaseData(range, MakeRange("D", "E")).Returns(false);
                yield return new TestCaseData(range, MakeRange("D", "G")).Returns(false);
                yield return new TestCaseData(range, MakeRange("E", "G")).Returns(false);
                yield return new TestCaseData(range, MakeRange("F", "G")).Returns(false);
            }
        }

        [Test]
        [TestCaseSource("Equals_TestCases")]
        public bool IEquatable_PostalCodeRange_Equals_uses_value_semantics(
            PostalCodeRange left,
            PostalCodeRange right)
        {
            return left.Equals(right);
        }

        [Test]
        public void IsAllPostalCodes_AllPostalCodes_ReturnsTrue()
        {
            var range = PostalCodeRange.Default;
            Assert.IsTrue(range.IsIndefinite);
        }

        [Test]
        public void IsAllPostalCodes_NotAllPostalCodes_ReturnsFalse()
        {
            var range = MakeRange("34", "35");
            Assert.IsFalse(range.IsIndefinite);
        }

        public static IEnumerable<ITestCaseData> Contains_TestCases
        {
            get
            {
                var range = MakeRange("C", "E");
                yield return new TestCaseData(range, PostalCodeRange.Default).Returns(false);
                yield return new TestCaseData(range, MakeRange("A", "B")).Returns(false);
                yield return new TestCaseData(range, MakeRange("A", "C")).Returns(false);
                yield return new TestCaseData(range, MakeRange("A", "D")).Returns(false);
                yield return new TestCaseData(range, MakeRange("A", "E")).Returns(false);
                yield return new TestCaseData(range, MakeRange("A", "G")).Returns(false);
                yield return new TestCaseData(range, MakeRange("C", "D")).Returns(true);
                yield return new TestCaseData(range, MakeRange("C", "E")).Returns(true);
                yield return new TestCaseData(range, MakeRange("C", "G")).Returns(false);
                yield return new TestCaseData(range, MakeRange("D", "E")).Returns(true);
                yield return new TestCaseData(range, MakeRange("D", "G")).Returns(false);
                yield return new TestCaseData(range, MakeRange("E", "G")).Returns(false);
                yield return new TestCaseData(range, MakeRange("F", "G")).Returns(false);
                yield return new TestCaseData(PostalCodeRange.Default, range).Returns(true);
                yield return new TestCaseData(MakeRange("A", "B"), range).Returns(false);
                yield return new TestCaseData(MakeRange("A", "C"), range).Returns(false);
                yield return new TestCaseData(MakeRange("A", "D"), range).Returns(false);
                yield return new TestCaseData(MakeRange("A", "E"), range).Returns(true);
                yield return new TestCaseData(MakeRange("A", "G"), range).Returns(true);
                yield return new TestCaseData(MakeRange("C", "D"), range).Returns(false);
                yield return new TestCaseData(MakeRange("C", "E"), range).Returns(true);
                yield return new TestCaseData(MakeRange("C", "G"), range).Returns(true);
                yield return new TestCaseData(MakeRange("D", "E"), range).Returns(false);
                yield return new TestCaseData(MakeRange("D", "G"), range).Returns(false);
                yield return new TestCaseData(MakeRange("E", "G"), range).Returns(false);
                yield return new TestCaseData(MakeRange("F", "G"), range).Returns(false);
            }
        }

        [Test]
        [TestCaseSource("Contains_TestCases")]
        public bool Contains_WhenContained_ReturnsTrue(
            PostalCodeRange outer,
            PostalCodeRange inner)
        {
            return PostalCodeRange.Contains(outer, inner);
        }

        public static IEnumerable<ITestCaseData> Object_Equals_TestCases
        {
            get
            {
                var range = MakeRange("C", "E");
                yield return new TestCaseData(range, null).Returns(false);
                yield return new TestCaseData(range, range).Returns(true);
                yield return new TestCaseData(range, string.Empty).Returns(false);
                yield return new TestCaseData(range, MakeRange("C", "E")).Returns(true);
            }
        }

        [Test]
        [TestCaseSource("Equals_TestCases")]
        [TestCaseSource("Object_Equals_TestCases")]
        public bool Object_Equals_uses_value_semantics(
            PostalCodeRange left,
            object right)
        {
            return left.Equals(right);
        }

        public static IEnumerable<ITestCaseData> Operator_Greater_TestCases
        {
            get
            {
                var r1 = MakeRange("06", "10");
                var r2 = MakeRange("05", "10");
                var r3 = MakeRange("05", "11");
                var r4 = PostalCodeRange.Default;

                yield return new TestCaseData(r1, r1).Returns(false);
                yield return new TestCaseData(r2, r2).Returns(false);
                yield return new TestCaseData(r3, r3).Returns(false);
                yield return new TestCaseData(r4, r4).Returns(false);

                yield return new TestCaseData(r1, r2).Returns(false);
                yield return new TestCaseData(r1, r3).Returns(false);
                yield return new TestCaseData(r1, r4).Returns(false);
                yield return new TestCaseData(r2, r3).Returns(false);
                yield return new TestCaseData(r2, r4).Returns(false);
                yield return new TestCaseData(r3, r4).Returns(false);

                yield return new TestCaseData(r2, r1).Returns(true);
                yield return new TestCaseData(r3, r1).Returns(true);
                yield return new TestCaseData(r4, r1).Returns(true);
                yield return new TestCaseData(r3, r2).Returns(true);
                yield return new TestCaseData(r4, r2).Returns(true);
                yield return new TestCaseData(r4, r3).Returns(true);
            }
        }

        public static IEnumerable<ITestCaseData> Operator_GreaterEqual_TestCases
        {
            get
            {
                var r1 = MakeRange("06", "10");
                var r2 = MakeRange("05", "10");
                var r3 = MakeRange("05", "11");
                var r4 = PostalCodeRange.Default;

                yield return new TestCaseData(r1, r1).Returns(true);
                yield return new TestCaseData(r2, r2).Returns(true);
                yield return new TestCaseData(r3, r3).Returns(true);
                yield return new TestCaseData(r4, r4).Returns(true);

                yield return new TestCaseData(r1, r2).Returns(false);
                yield return new TestCaseData(r1, r3).Returns(false);
                yield return new TestCaseData(r1, r4).Returns(false);
                yield return new TestCaseData(r2, r3).Returns(false);
                yield return new TestCaseData(r2, r4).Returns(false);
                yield return new TestCaseData(r3, r4).Returns(false);

                yield return new TestCaseData(r2, r1).Returns(true);
                yield return new TestCaseData(r3, r1).Returns(true);
                yield return new TestCaseData(r4, r1).Returns(true);
                yield return new TestCaseData(r3, r2).Returns(true);
                yield return new TestCaseData(r4, r2).Returns(true);
                yield return new TestCaseData(r4, r3).Returns(true);
            }
        }

        public static IEnumerable<ITestCaseData> Operator_Less_TestCases
        {
            get
            {
                var r1 = MakeRange("06", "10");
                var r2 = MakeRange("05", "10");
                var r3 = MakeRange("05", "11");
                var r4 = PostalCodeRange.Default;

                yield return new TestCaseData(r1, r1).Returns(false);
                yield return new TestCaseData(r2, r2).Returns(false);
                yield return new TestCaseData(r3, r3).Returns(false);
                yield return new TestCaseData(r4, r4).Returns(false);

                yield return new TestCaseData(r1, r2).Returns(true);
                yield return new TestCaseData(r1, r3).Returns(true);
                yield return new TestCaseData(r1, r4).Returns(true);
                yield return new TestCaseData(r2, r3).Returns(true);
                yield return new TestCaseData(r2, r4).Returns(true);
                yield return new TestCaseData(r3, r4).Returns(true);

                yield return new TestCaseData(r2, r1).Returns(false);
                yield return new TestCaseData(r3, r1).Returns(false);
                yield return new TestCaseData(r4, r1).Returns(false);
                yield return new TestCaseData(r3, r2).Returns(false);
                yield return new TestCaseData(r4, r2).Returns(false);
                yield return new TestCaseData(r4, r3).Returns(false);
            }
        }

        public static IEnumerable<ITestCaseData> Operator_LessEqual_TestCases
        {
            get
            {
                var r1 = MakeRange("06", "10");
                var r2 = MakeRange("05", "10");
                var r3 = MakeRange("05", "11");
                var r4 = PostalCodeRange.Default;

                yield return new TestCaseData(r1, r1).Returns(true);
                yield return new TestCaseData(r2, r2).Returns(true);
                yield return new TestCaseData(r3, r3).Returns(true);
                yield return new TestCaseData(r4, r4).Returns(true);

                yield return new TestCaseData(r1, r2).Returns(true);
                yield return new TestCaseData(r1, r3).Returns(true);
                yield return new TestCaseData(r1, r4).Returns(true);
                yield return new TestCaseData(r2, r3).Returns(true);
                yield return new TestCaseData(r2, r4).Returns(true);
                yield return new TestCaseData(r3, r4).Returns(true);

                yield return new TestCaseData(r2, r1).Returns(false);
                yield return new TestCaseData(r3, r1).Returns(false);
                yield return new TestCaseData(r4, r1).Returns(false);
                yield return new TestCaseData(r3, r2).Returns(false);
                yield return new TestCaseData(r4, r2).Returns(false);
                yield return new TestCaseData(r4, r3).Returns(false);
            }
        }

        [Test]
        [TestCaseSource("Operator_Greater_TestCases")]
        public bool Operator_GreaterThan_ComparesInsideRangesAsSmaller(
            PostalCodeRange left,
            PostalCodeRange right)
        {
            return left > right;
        }

        [Test]
        [TestCaseSource("Operator_GreaterEqual_TestCases")]
        public bool Operator_GreaterThanOrEqual_ComparesInsideRangesAsSmaller(
            PostalCodeRange left,
            PostalCodeRange right)
        {
            return left >= right;
        }

        [Test]
        [TestCaseSource("Operator_Less_TestCases")]
        public bool Operator_LessThan_ComparesInsideRangesAsSmaller(
            PostalCodeRange left,
            PostalCodeRange right)
        {
            return left < right;
        }

        [Test]
        [TestCaseSource("Operator_LessEqual_TestCases")]
        public bool Operator_LessThanOrEqual_ComparesInsideRangesAsSmaller(
            PostalCodeRange left,
            PostalCodeRange right)
        {
            return left <= right;
        }

        public static IEnumerable<ITestCaseData> Overlapping_TestCases
        {
            get
            {
                var range = MakeRange("C", "E");
                yield return new TestCaseData(range, null).Returns(false);
                yield return new TestCaseData(range, PostalCodeRange.Default).Returns(true);
                yield return new TestCaseData(range, MakeRange("A", "B")).Returns(false);
                yield return new TestCaseData(range, MakeRange("A", "C")).Returns(true);
                yield return new TestCaseData(range, MakeRange("A", "D")).Returns(true);
                yield return new TestCaseData(range, MakeRange("A", "E")).Returns(true);
                yield return new TestCaseData(range, MakeRange("A", "G")).Returns(true);
                yield return new TestCaseData(range, MakeRange("C", "D")).Returns(true);
                yield return new TestCaseData(range, MakeRange("C", "E")).Returns(true);
                yield return new TestCaseData(range, MakeRange("C", "G")).Returns(true);
                yield return new TestCaseData(range, MakeRange("D", "E")).Returns(true);
                yield return new TestCaseData(range, MakeRange("D", "G")).Returns(true);
                yield return new TestCaseData(range, MakeRange("E", "G")).Returns(true);
                yield return new TestCaseData(range, MakeRange("F", "G")).Returns(false);
            }
        }

        [Test]
        [TestCaseSource("Overlapping_TestCases")]
        public bool AreOverlapping_compares_full_range(
            PostalCodeRange left,
            PostalCodeRange right)
        {
            return PostalCodeRange.AreOverlapping(left, right);
        }

        [Test]
        public void AreOverlapping_1()
        {
            var left = new PostalCodeRange(new PostalCode("1000"), new PostalCode("2000"));
            var right = new PostalCodeRange(new PostalCode("1500"), new PostalCode("2500"));

            Assert.IsTrue(PostalCodeRange.AreOverlapping(left, right));
        }

        public static IEnumerable<ITestCaseData> Resect_TestCases
        {
            get
            {
                var range = new PostalCodeRange(new NumericPostalCode("003"), new NumericPostalCode("005"));
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("001"), new NumericPostalCode("002")), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("001"), new NumericPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("001"), new NumericPostalCode("003")), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("001"), new NumericPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("001"), new NumericPostalCode("004")), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("001"), new NumericPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("001"), new NumericPostalCode("005")), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("001"), new NumericPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("001"), new NumericPostalCode("006")), range,
                        new[]
                        {
                            new PostalCodeRange(new NumericPostalCode("001"), new NumericPostalCode("002")),
                            new PostalCodeRange(new NumericPostalCode("006"), new NumericPostalCode("006"))
                        });
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("001"), new NumericPostalCode("007")), range,
                        new[]
                        {
                            new PostalCodeRange(new NumericPostalCode("001"), new NumericPostalCode("002")),
                            new PostalCodeRange(new NumericPostalCode("006"), new NumericPostalCode("007"))
                        });
                yield return new TestCaseData(new PostalCodeRange(new NumericPostalCode("003"), new NumericPostalCode("004")), range, new PostalCodeRange[0]);
                yield return new TestCaseData(new PostalCodeRange(new NumericPostalCode("003"), new NumericPostalCode("005")), range, new PostalCodeRange[0]);
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("003"), new NumericPostalCode("007")), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("006"), new NumericPostalCode("007"))});
                yield return new TestCaseData(new PostalCodeRange(new NumericPostalCode("004"), new NumericPostalCode("005")), range, new PostalCodeRange[0]);
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("004"), new NumericPostalCode("007")), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("006"), new NumericPostalCode("007"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("005"), new NumericPostalCode("007")), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("006"), new NumericPostalCode("007"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("006"), new NumericPostalCode("007")), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("006"), new NumericPostalCode("007"))});

                // Definite Range from Indefinite Range

                yield return
                    new TestCaseData(PostalCodeRange.Default, range,
                        new[] {new PostalCodeRange(null, new NumericPostalCode("002")), new PostalCodeRange(new NumericPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("001")), range,
                        new[] {new PostalCodeRange(null, new NumericPostalCode("001"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("002")), range,
                        new[] {new PostalCodeRange(null, new NumericPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("003")), range,
                        new[] {new PostalCodeRange(null, new NumericPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("004")), range,
                        new[] {new PostalCodeRange(null, new NumericPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("005")), range,
                        new[] {new PostalCodeRange(null, new NumericPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("006")), range,
                        new[]
                        {
                            new PostalCodeRange(null, new NumericPostalCode("002")),
                            new PostalCodeRange(new NumericPostalCode("006"), new NumericPostalCode("006"))
                        });
                yield return
                    new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("007")), range,
                        new[]
                        {
                            new PostalCodeRange(null, new NumericPostalCode("002")),
                            new PostalCodeRange(new NumericPostalCode("006"), new NumericPostalCode("007"))
                        });
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("001"), null), range,
                        new[]
                        {
                            new PostalCodeRange(new NumericPostalCode("001"), new NumericPostalCode("002")),
                            new PostalCodeRange(new NumericPostalCode("006"), null)
                        });
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("002"), null), range,
                        new[]
                        {
                            new PostalCodeRange(new NumericPostalCode("002"), new NumericPostalCode("002")),
                            new PostalCodeRange(new NumericPostalCode("006"), null)
                        });
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("003"), null), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("004"), null), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("005"), null), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("006"), null), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("007"), null), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("007"), null)});

                // Indefinite from Indefinite

                range = new PostalCodeRange(new NumericPostalCode("003"), null);

                yield return new TestCaseData(PostalCodeRange.Default, range, new[] {new PostalCodeRange(null, new NumericPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("001")), range,
                        new[] {new PostalCodeRange(null, new NumericPostalCode("001"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("002")), range,
                        new[] {new PostalCodeRange(null, new NumericPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("003")), range,
                        new[] {new PostalCodeRange(null, new NumericPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("004")), range,
                        new[] {new PostalCodeRange(null, new NumericPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("001"), null), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("001"), new NumericPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("002"), null), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("002"), new NumericPostalCode("002"))});
                yield return new TestCaseData(new PostalCodeRange(new NumericPostalCode("003"), null), range, new PostalCodeRange[0]);
                yield return new TestCaseData(new PostalCodeRange(new NumericPostalCode("004"), null), range, new PostalCodeRange[0]);

                range = new PostalCodeRange(null, new NumericPostalCode("005"));

                yield return new TestCaseData(PostalCodeRange.Default, range, new[] {new PostalCodeRange(new NumericPostalCode("006"), null)});
                yield return new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("004")), range, new PostalCodeRange[0]);
                yield return new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("005")), range, new PostalCodeRange[0]);
                yield return
                    new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("006")), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("006"), new NumericPostalCode("006"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("007")), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("006"), new NumericPostalCode("007"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("004"), null), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("005"), null), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("006"), null), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("007"), null), range,
                        new[] {new PostalCodeRange(new NumericPostalCode("007"), null)});


                // Default

                yield return new TestCaseData(PostalCodeRange.Default, PostalCodeRange.Default, new PostalCodeRange[0]);
                yield return new TestCaseData(new PostalCodeRange(new NumericPostalCode("004"), null), PostalCodeRange.Default, new PostalCodeRange[0]);
                yield return new TestCaseData(new PostalCodeRange(null, new NumericPostalCode("004")), PostalCodeRange.Default, new PostalCodeRange[0]);
                yield return
                    new TestCaseData(new PostalCodeRange(new NumericPostalCode("004"), new NumericPostalCode("004")), PostalCodeRange.Default,
                        new PostalCodeRange[0]);
            }
        }

        [Test]
        [TestCaseSource("Resect_TestCases")]
        public void Resect_WorksForAllCases(
            PostalCodeRange baseRange,
            PostalCodeRange toResect,
            IEnumerable<PostalCodeRange> expected)
        {
            var actual = PostalCodeRange.Resect(baseRange, toResect);

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        public static IEnumerable<PostalCodeRange> PostalCodeRanges
        {
            get
            {
                yield return PostalCodeRange.Default;
                yield return MakeRange(null, null);
                yield return MakeRange(null, "END");
                yield return MakeRange("START", null);
                yield return MakeRange("BEGIN", "END");
            }
        }

        [Test]
        [TestCaseSource("PostalCodeRanges")]
        public void ToString_succeeds(PostalCodeRange range)
        {
            Assert.NotNull(range.ToString());
        }

        public static IEnumerable<PostalCodeRange> DefaultPostalCodeRangeEquivalencyClass
        {
            get
            {
                yield return PostalCodeRange.Default;
                yield return new PostalCodeRange(null, null);
            }
        }

        [Test]
        public void EquivalentDefaults_AreEqual(
            [ValueSource("DefaultPostalCodeRangeEquivalencyClass")] PostalCodeRange left,
            [ValueSource("DefaultPostalCodeRangeEquivalencyClass")] PostalCodeRange right)
        {
            Assert.That(left, Is.EqualTo(right));
        }


        [Test]
        public void EquivalentDefaults_CompareEqual(
            [ValueSource("DefaultPostalCodeRangeEquivalencyClass")] PostalCodeRange left,
            [ValueSource("DefaultPostalCodeRangeEquivalencyClass")] PostalCodeRange right)
        {
            var actual = left.CompareTo(right);
            Assert.That(actual, Is.EqualTo(0));
        }

        [Test]
        public void EquivalentDefaults_HaveSameHashCode(
            [ValueSource("DefaultPostalCodeRangeEquivalencyClass")] PostalCodeRange left,
            [ValueSource("DefaultPostalCodeRangeEquivalencyClass")] PostalCodeRange right)
        {
            var leftHc = left.GetHashCode();
            var rightHc = right.GetHashCode();

            Assert.That(leftHc, Is.EqualTo(rightHc));
        }

        [Test]
        public void EquivalentDefaults_HaveIsDefaultSet(
            [ValueSource("DefaultPostalCodeRangeEquivalencyClass")] PostalCodeRange range)
        {
            Assert.That(range.IsDefault, Is.True);
        }
    }
}

