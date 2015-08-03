using System;
using System.Collections.Generic;
using NUnit.Framework;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes.UnitTests
{
    [TestFixture]
    internal class PostalCodeRangeTests
    {
        private static PostalCodeRange MakeRange(string start, string end)
        {
            var startPc = start != null ? new DefaultPostalCode(start) : null;
            var endPc = end != null ? new DefaultPostalCode(end) : null;
            return new PostalCodeRange(startPc, endPc);
        }

        private static PostalCodeRange MakeRangeUS(string start, string end)
        {
            var startPc = start != null ? new USPostalCode(start) : null;
            var endPc = end != null ? new USPostalCode(end) : null;
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
            var start = new DefaultPostalCode("12345");
            var end = new DefaultPostalCode("67890");

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
                yield return new TestCaseData(new DefaultPostalCode("ABCDE"), new DefaultPostalCode("FGHIJ"));
                yield return new TestCaseData(new DefaultPostalCode("02421"), new DefaultPostalCode("12958"));
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
                yield return new TestCaseData(range, PostalCodeRange.Default).Returns(1);
                yield return new TestCaseData( PostalCodeRange.Default, range).Returns(-1);
                yield return new TestCaseData(PostalCodeRange.Default, PostalCodeRange.Default).Returns(0);
                yield return new TestCaseData(range, MakeRange("A", "B")).Returns(1);
                yield return new TestCaseData(range, MakeRange("A", "C")).Returns(1);
                yield return new TestCaseData(range, MakeRange("A", "D")).Returns(1);
                yield return new TestCaseData(range, MakeRange("A", "E")).Returns(1);
                yield return new TestCaseData(range, MakeRange("A", "G")).Returns(1);
                yield return new TestCaseData(range, MakeRange("C", "D")).Returns(1);
                yield return new TestCaseData(range, MakeRange("C", "E")).Returns(0);
                yield return new TestCaseData(range, MakeRange("C", "G")).Returns(-1);
                yield return new TestCaseData(range, MakeRange("D", "E")).Returns(-1);
                yield return new TestCaseData(range, MakeRange("D", "G")).Returns(-1);
                yield return new TestCaseData(range, MakeRange("E", "G")).Returns(-1);
                yield return new TestCaseData(range, MakeRange("F", "G")).Returns(-1);
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
        public void IsIndefinite_Indefinite_ReturnsTrue()
        {
            var range = PostalCodeRange.Default;
            Assert.IsTrue(range.IsIndefinite);
        }

        [Test]
        public void IsIndefinite_NotIndefinite_ReturnsFalse()
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

        public static IEnumerable<ITestCaseData> Contains_WithPostalCode_TestCases
        {
            get
            {
                var rangeUS = MakeRangeUS("28000 0000", "28000 9999");
                yield return new TestCaseData(rangeUS, new USPostalCode("28000")).Returns(true);
                yield return new TestCaseData(rangeUS, new USPostalCode("28000 1235")).Returns(true);
            }
        }

        [Test]
        [TestCaseSource("Contains_WithPostalCode_TestCases")]
        public bool Contains_WithPostalCode_WhenContained_ReturnsTrue(
            PostalCodeRange range,
            PostalCode pc)
        {
            return PostalCodeRange.Contains(range, pc);
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


        public static PostalCodeRange[] _sortedPostalCodeRanges = {
            new PostalCodeRange(null, null), 
            new PostalCodeRange(new DefaultPostalCode("AAA"), new DefaultPostalCode("ZZZ")),
            new PostalCodeRange(new DefaultPostalCode("BBB"), new DefaultPostalCode("DDD")),
            new PostalCodeRange(new DefaultPostalCode("BBB"), new DefaultPostalCode("ZZZ")),
            new PostalCodeRange(new DefaultPostalCode("CCC"), new DefaultPostalCode("ZZZ")),
            new PostalCodeRange(new DefaultPostalCode("DDD"), new DefaultPostalCode("DDX")),
            new PostalCodeRange(new DefaultPostalCode("DDD"), new DefaultPostalCode("EEE")),
            new PostalCodeRange(new DefaultPostalCode("DDD"), new DefaultPostalCode("FFF"))
        };

        public static IEnumerable<ITestCaseData> Operator_Greater_TestCases
        {
            get
            {
                for(var i=0; i<_sortedPostalCodeRanges.Length; i++)
                    for (var j = i + 1; j < _sortedPostalCodeRanges.Length; j++)
                    {
                        yield return new TestCaseData(_sortedPostalCodeRanges[i], _sortedPostalCodeRanges[j]).Returns(false);
                        yield return new TestCaseData(_sortedPostalCodeRanges[j], _sortedPostalCodeRanges[i]).Returns(true);
                    }
            }
        }

        [Test]
        [TestCaseSource("Operator_Greater_TestCases")]
        public bool Operator_GreaterThan(PostalCodeRange left, PostalCodeRange right)
        {
            return left > right;
        }

        public static IEnumerable<ITestCaseData> Operator_GreaterEqual_TestCases
        {
            get
            {
                for (var i = 0; i < _sortedPostalCodeRanges.Length; i++)
                    for (var j = i + 1; j < _sortedPostalCodeRanges.Length; j++)
                    {
                        yield return new TestCaseData(_sortedPostalCodeRanges[i], _sortedPostalCodeRanges[j]).Returns(false);
                        yield return new TestCaseData(_sortedPostalCodeRanges[j], _sortedPostalCodeRanges[i]).Returns(true);
                    }

                for (var i = 0; i < _sortedPostalCodeRanges.Length; i++)
                {
                    yield return new TestCaseData(_sortedPostalCodeRanges[i], _sortedPostalCodeRanges[i]).Returns(true);
                }
            }
        }

        [Test]
        [TestCaseSource("Operator_GreaterEqual_TestCases")]
        public bool Operator_GreaterThanOrEqual(PostalCodeRange left, PostalCodeRange right)
        {
            return left >= right;
        }

        public static IEnumerable<ITestCaseData> Operator_Less_TestCases
        {
            get
            {
                for (var i = 0; i < _sortedPostalCodeRanges.Length; i++)
                    for (var j = i + 1; j < _sortedPostalCodeRanges.Length; j++)
                    {
                        yield return new TestCaseData(_sortedPostalCodeRanges[i], _sortedPostalCodeRanges[j]).Returns(true);
                        yield return new TestCaseData(_sortedPostalCodeRanges[j], _sortedPostalCodeRanges[i]).Returns(false);
                    }
            }
        }

        [Test]
        [TestCaseSource("Operator_Less_TestCases")]
        public bool Operator_LessThan(PostalCodeRange left, PostalCodeRange right)
        {
            return left < right;
        }

        public static IEnumerable<ITestCaseData> Operator_LessEqual_TestCases
        {
            get
            {
                for (var i = 0; i < _sortedPostalCodeRanges.Length; i++)
                    for (var j = i + 1; j < _sortedPostalCodeRanges.Length; j++)
                    {
                        yield return new TestCaseData(_sortedPostalCodeRanges[i], _sortedPostalCodeRanges[j]).Returns(true);
                        yield return new TestCaseData(_sortedPostalCodeRanges[j], _sortedPostalCodeRanges[i]).Returns(false);
                    }

                for (var i = 0; i < _sortedPostalCodeRanges.Length; i++)
                {
                    yield return new TestCaseData(_sortedPostalCodeRanges[i], _sortedPostalCodeRanges[i]).Returns(true);
                }
            }
        }

        [Test]
        [TestCaseSource("Operator_LessEqual_TestCases")]
        public bool Operator_LessThanOrEqual(PostalCodeRange left, PostalCodeRange right)
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
            var left = new PostalCodeRange(new DefaultPostalCode("1000"), new DefaultPostalCode("2000"));
            var right = new PostalCodeRange(new DefaultPostalCode("1500"), new DefaultPostalCode("2500"));

            Assert.IsTrue(PostalCodeRange.AreOverlapping(left, right));
        }

        public static IEnumerable<ITestCaseData> Substract_TestCases
        {
            get
            {
                var range = new PostalCodeRange(new DefaultPostalCode("003"), new DefaultPostalCode("005"));
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("001"), new DefaultPostalCode("002")), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("001"), new DefaultPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("001"), new DefaultPostalCode("003")), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("001"), new DefaultPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("001"), new DefaultPostalCode("004")), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("001"), new DefaultPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("001"), new DefaultPostalCode("005")), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("001"), new DefaultPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("001"), new DefaultPostalCode("006")), range,
                        new[]
                        {
                            new PostalCodeRange(new DefaultPostalCode("001"), new DefaultPostalCode("002")),
                            new PostalCodeRange(new DefaultPostalCode("006"), new DefaultPostalCode("006"))
                        });
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("001"), new DefaultPostalCode("007")), range,
                        new[]
                        {
                            new PostalCodeRange(new DefaultPostalCode("001"), new DefaultPostalCode("002")),
                            new PostalCodeRange(new DefaultPostalCode("006"), new DefaultPostalCode("007"))
                        });
                yield return new TestCaseData(new PostalCodeRange(new DefaultPostalCode("003"), new DefaultPostalCode("004")), range, new PostalCodeRange[0]);
                yield return new TestCaseData(new PostalCodeRange(new DefaultPostalCode("003"), new DefaultPostalCode("005")), range, new PostalCodeRange[0]);
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("003"), new DefaultPostalCode("007")), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("006"), new DefaultPostalCode("007"))});
                yield return new TestCaseData(new PostalCodeRange(new DefaultPostalCode("004"), new DefaultPostalCode("005")), range, new PostalCodeRange[0]);
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("004"), new DefaultPostalCode("007")), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("006"), new DefaultPostalCode("007"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("005"), new DefaultPostalCode("007")), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("006"), new DefaultPostalCode("007"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("006"), new DefaultPostalCode("007")), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("006"), new DefaultPostalCode("007"))});

                // Definite Range from Indefinite Range

                yield return
                    new TestCaseData(PostalCodeRange.Default, range,
                        new[] {new PostalCodeRange(null, new DefaultPostalCode("002")), new PostalCodeRange(new DefaultPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("001")), range,
                        new[] {new PostalCodeRange(null, new DefaultPostalCode("001"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("002")), range,
                        new[] {new PostalCodeRange(null, new DefaultPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("003")), range,
                        new[] {new PostalCodeRange(null, new DefaultPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("004")), range,
                        new[] {new PostalCodeRange(null, new DefaultPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("005")), range,
                        new[] {new PostalCodeRange(null, new DefaultPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("006")), range,
                        new[]
                        {
                            new PostalCodeRange(null, new DefaultPostalCode("002")),
                            new PostalCodeRange(new DefaultPostalCode("006"), new DefaultPostalCode("006"))
                        });
                yield return
                    new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("007")), range,
                        new[]
                        {
                            new PostalCodeRange(null, new DefaultPostalCode("002")),
                            new PostalCodeRange(new DefaultPostalCode("006"), new DefaultPostalCode("007"))
                        });
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("001"), null), range,
                        new[]
                        {
                            new PostalCodeRange(new DefaultPostalCode("001"), new DefaultPostalCode("002")),
                            new PostalCodeRange(new DefaultPostalCode("006"), null)
                        });
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("002"), null), range,
                        new[]
                        {
                            new PostalCodeRange(new DefaultPostalCode("002"), new DefaultPostalCode("002")),
                            new PostalCodeRange(new DefaultPostalCode("006"), null)
                        });
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("003"), null), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("004"), null), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("005"), null), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("006"), null), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("007"), null), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("007"), null)});

                // Indefinite from Indefinite

                range = new PostalCodeRange(new DefaultPostalCode("003"), null);

                yield return new TestCaseData(PostalCodeRange.Default, range, new[] {new PostalCodeRange(null, new DefaultPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("001")), range,
                        new[] {new PostalCodeRange(null, new DefaultPostalCode("001"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("002")), range,
                        new[] {new PostalCodeRange(null, new DefaultPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("003")), range,
                        new[] {new PostalCodeRange(null, new DefaultPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("004")), range,
                        new[] {new PostalCodeRange(null, new DefaultPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("001"), null), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("001"), new DefaultPostalCode("002"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("002"), null), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("002"), new DefaultPostalCode("002"))});
                yield return new TestCaseData(new PostalCodeRange(new DefaultPostalCode("003"), null), range, new PostalCodeRange[0]);
                yield return new TestCaseData(new PostalCodeRange(new DefaultPostalCode("004"), null), range, new PostalCodeRange[0]);

                range = new PostalCodeRange(null, new DefaultPostalCode("005"));

                yield return new TestCaseData(PostalCodeRange.Default, range, new[] {new PostalCodeRange(new DefaultPostalCode("006"), null)});
                yield return new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("004")), range, new PostalCodeRange[0]);
                yield return new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("005")), range, new PostalCodeRange[0]);
                yield return
                    new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("006")), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("006"), new DefaultPostalCode("006"))});
                yield return
                    new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("007")), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("006"), new DefaultPostalCode("007"))});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("004"), null), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("005"), null), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("006"), null), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("006"), null)});
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("007"), null), range,
                        new[] {new PostalCodeRange(new DefaultPostalCode("007"), null)});


                // Default

                yield return new TestCaseData(PostalCodeRange.Default, PostalCodeRange.Default, new PostalCodeRange[0]);
                yield return new TestCaseData(new PostalCodeRange(new DefaultPostalCode("004"), null), PostalCodeRange.Default, new PostalCodeRange[0]);
                yield return new TestCaseData(new PostalCodeRange(null, new DefaultPostalCode("004")), PostalCodeRange.Default, new PostalCodeRange[0]);
                yield return
                    new TestCaseData(new PostalCodeRange(new DefaultPostalCode("004"), new DefaultPostalCode("004")), PostalCodeRange.Default,
                        new PostalCodeRange[0]);
            }
        }

        [Test]
        [TestCaseSource("Substract_TestCases")]
        public void Substract_WorksForAllCases(
            PostalCodeRange baseRange,
            PostalCodeRange toSubstract,
            IEnumerable<PostalCodeRange> expected)
        {
            var actual = PostalCodeRange.Substract(baseRange, toSubstract);

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

