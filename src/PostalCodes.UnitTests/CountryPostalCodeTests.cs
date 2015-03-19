using System;
using NUnit.Framework;

namespace PostalCodes.Contracts.UnitTests
{
    [TestFixture]
    internal class CountryPostalCodeTests
    {
        private static readonly object[] DataSourceForEqualOperator =
        {
            new object[] {"BG", "1000", "BG", "1000", true}, new object[] {"BG", "1000", "BG", null, false},
            new object[] {"BG", "1000", null, "1000", false}, new object[] {"BG", null, "BG", "1000", false}, new object[] {null, "1000", "BG", "1000", false},
            new object[] {null, null, "BG", "1000", false}, new object[] {null, "1000", "BG", null, false}, new object[] {"BG", null, null, "1000", false},
            new object[] {"BG", null, "BG", null, true}, new object[] {"BG", null, null, null, false}, new object[] {null, null, null, null, true},
        };

        private static readonly object[] DataSourceForCompareTo =
        {
            new object[] {"BG", "1000", "BG", "1002", -1}, new object[] {"BG", "1000", "BG", "1000", 0},
            new object[] {"BG", "1002", "BG", "1000", 1}, new object[] {"BG", "1000", "GB", "1000", -1}, new object[] {"US", "1000", "BG", "1000", 1},
            new object[] {"BG", "1000", "GB", null, -1}, new object[] {"US", "1000", "BG", null, 1}, new object[] {"BG", null, "GB", null, -1},
            new object[] {"US", null, "BG", null, 1}, new object[] {"BG", null, "GB", null, -1}, new object[] {"BG", null, "BG", null, 0},
            new object[] {"US", null, "BG", null, 1}, new object[] {"US", "1000", "US", null, 1}, new object[] {"US", null, "US", "1000", -1},
        };

        [Test, TestCaseSource("DataSourceForEqualOperator")]
        public void OperatorEquals(string country1, string code1, string country2, string code2, bool expectedResult)
        {
            var cp1 = new CountryPostalCode(country1, code1);
            var cp2 = new CountryPostalCode(country2, code2);
            Assert.AreEqual(expectedResult, cp1 == cp2);
        }

        [Test, TestCaseSource("DataSourceForEqualOperator")]
        public void Equals(string country1, string code1, string country2, string code2, bool expectedResult)
        {
            var cp1 = new CountryPostalCode(country1, code1);
            var cp2 = new CountryPostalCode(country2, code2);
            Assert.AreEqual(expectedResult, cp1.Equals(cp2));
        }

        [Test]
        public void Equals_HandlesTypesAndNulls()
        {
            var cp1 = new CountryPostalCode("BG", "1000");
            var cp2 = new CountryPostalCode("BG", "1000");
            var x = new object();

            Assert.AreEqual(true, cp1.Equals((object)cp2));
            Assert.AreNotEqual(true, cp1.Equals(x));
            Assert.AreNotEqual(true, cp1.Equals(null));
        }

        [Test]
        public void CompareTo()
        {
            var cp1 = new CountryPostalCode("BG", "1000");
            var cp2 = new CountryPostalCode("BG", "1000");

            Assert.AreEqual(1, cp1.CompareTo(null));
            Assert.AreEqual(0, cp1.CompareTo(cp2));
        }

        [Test, TestCaseSource("DataSourceForEqualOperator")]
        public void OperatorNotEquals(string country1, string code1, string country2, string code2, bool expectedResult)
        {
            var cp1 = new CountryPostalCode(country1, code1);
            var cp2 = new CountryPostalCode(country2, code2);
            Assert.AreNotEqual(expectedResult, cp1 != cp2);
        }

        [Test, TestCaseSource("DataSourceForCompareTo")]
        public void CompareTo(string country1, string code1, string country2, string code2, int expectedResult)
        {
            var cp1 = new CountryPostalCode(country1, code1);
            var cp2 = new CountryPostalCode(country2, code2);
            var received = cp1.CompareTo(cp2);
            if (received != 0)
            {
                received = received / Math.Abs(received);
            }

            Assert.AreEqual(expectedResult, received);
        }

        [TestCase("BG", "1000", "1000")]
        [TestCase("CA", "A9A9A9", "A9A9A9")]
        [TestCase("US", "50000", "50000")]
        public void PostalCode(string countryToConstruct, string postalCodeToConstruct, string expectedPostalCode)
        {
            Assert.AreEqual(expectedPostalCode, (new CountryPostalCode(countryToConstruct, postalCodeToConstruct).PostalCode));
        }

        [TestCase("BG", "1000", "BG")]
        [TestCase("CA", "A9A 9A9", "CA")]
        [TestCase("US", "50000", "US")]
        public void CountryCode(string countryToConstruct, string postalCodeToConstruct, string expectedCountryCode)
        {
            Assert.AreEqual(expectedCountryCode, (new CountryPostalCode(countryToConstruct, postalCodeToConstruct).CountryCode));
        }

        [TestCase("BG", "1000", "BG:1000")]
        [TestCase("US", "50000", "US:50000")]
        [TestCase("GB", "AA9AAA", "GB:AA9AAA")]
        [TestCase(null, "AA9AAA", "null:AA9AAA")]
        [TestCase("US", null, "US:null")]
        public void ToString(string countryToConstruct, string postalCodeToConstruct, string expectedToString)
        {
            Assert.AreEqual(expectedToString, new CountryPostalCode(countryToConstruct, postalCodeToConstruct).ToString());
        }

        [Test]
        public void GetHashCode_ReturnsDifferentResultForDifferentObjects()
        {
            var cp1 = new CountryPostalCode("BG", "1000");
            var cp2 = new CountryPostalCode("BG", "1001");

            Assert.AreNotEqual(cp1.GetHashCode(), cp2.GetHashCode());
        }

        [Test]
        public void GetHashCode_ReturnsSameHashForIdenticalObjects()
        {
            var cp1 = new CountryPostalCode("BG", "1000");
            var cp2 = new CountryPostalCode("BG", "1000");

            Assert.AreEqual(cp1.GetHashCode(), cp2.GetHashCode());
        }

        [Test]
        public void Equals_MaxPostalCodeSameCountry_AreEqual()
        {
            var cp1 = new CountryPostalCode("SE", CountryPostalCode.MaxPostalCode);
            var cp2 = new CountryPostalCode("SE", CountryPostalCode.MaxPostalCode);

            Assert.AreEqual(cp1, cp2);
        }

        [Test]
        public void CompareTo_MaxPostalCodeForOne_MaxSortsAfterOthers()
        {
            var cp1 = new CountryPostalCode("SE", "12345");
            var cp2 = new CountryPostalCode("SE", CountryPostalCode.MaxPostalCode);

            Assert.True(cp1.CompareTo(cp2) < 0);
            Assert.True(cp2.CompareTo(cp1) > 0);
            Assert.True(cp2.CompareTo(cp2) == 0);
        }

        [Test]
        public void Equals_MinPostalCodeSameCountry_AreEqual()
        {
            var cp1 = new CountryPostalCode("ES", CountryPostalCode.MinPostalCode);
            var cp2 = new CountryPostalCode("ES", CountryPostalCode.MinPostalCode);

            Assert.AreEqual(cp1, cp2);
        }

        [Test]
        public void CompareTo_MinPostalCodeForOne_MinSortsBeforeOthers()
        {
            var cp1 = new CountryPostalCode("SE", "12345");
            var cp2 = new CountryPostalCode("SE", CountryPostalCode.MinPostalCode);

            Assert.True(cp1.CompareTo(cp2) > 0);
            Assert.True(cp2.CompareTo(cp1) < 0);
            Assert.True(cp2.CompareTo(cp2) == 0);
        }
    }
}
