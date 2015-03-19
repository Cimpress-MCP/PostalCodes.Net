using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests
{
    [TestFixture]
    internal class CountryTests
    {
        private static readonly object[] DataSourceForEqualOperator =
        {
            new object[] {"BG", "BG",  true}, 
            new object[] {"BG", null, false}, 
            new object[] {null, "BG", false}, 
            new object[] {null, null, true},
        };
        
        [Test, TestCaseSource("DataSourceForEqualOperator")]
        public void Country_ValidObject_OperatorEqualsReturnsCorrectResult(string country1, string country2, bool expectedResult)
        {
            var cp1 = new Country(country1);
            var cp2 = new Country(country2);
            Assert.AreEqual(expectedResult, cp1 == cp2);
        }

        [Test, TestCaseSource("DataSourceForEqualOperator")]
        public void Country_Equals_ReturnsCorrectResult(string country1, string country2, bool expectedResult)
        {
            var cp1 = new Country(country1);
            var cp2 = new Country(country2);
            Assert.AreEqual(expectedResult, cp1.Equals(cp2));
        }

        [Test]
        public void Country_Equals_ReturnsCorrectResult()
        {
            Country nullCountry = null;
            // ReSharper disable ConditionIsAlwaysTrueOrFalse            
            Assert.AreEqual(true, nullCountry == null);
            Assert.AreEqual(false, nullCountry == new Country("BG"));
            Assert.AreEqual(false, new Country("BG") == nullCountry);
            // ReSharper enable ConditionIsAlwaysTrueOrFalse
        }
        

        [Test, TestCaseSource("DataSourceForEqualOperator")]
        public void Country_ValidObject_OperatorInequalsReturnsCorrectResult(string country1, string country2, bool expectedResult)
        {
            var cp1 = new Country(country1);
            var cp2 = new Country(country2);
            Assert.AreNotEqual(expectedResult, cp1 != cp2);
        }

        [Test]
        [TestCase("BG")]
        [TestCase("CA")]
        [TestCase("US")]
        public void Country_PostalCode_ReturnsCorrect(string country)
        {
            Assert.AreEqual(country, (new Country(country).Code));
        }

        [Test]
        [TestCase("BG")]
        [TestCase("CA")]
        [TestCase("US")]
        public void Country_CountryCode_ReturnsCorrect(string country)
        {
            Assert.AreEqual(country, (new Country(country).Code));
        }

        [Test]
        [TestCase("BG")]
        [TestCase("US")]
        [TestCase("GB")]
        public void Country_ToString_ReturnsMeaningfulString(string country)
        {
            var cp = new Country(country).ToString();
            Assert.IsNotNull(cp);
            Assert.IsTrue(cp.IndexOf(country, StringComparison.Ordinal) != -1);
        }

        [Test]
        public void Country_GetHashCode_ReturnsDifferentHash()
        {
            var c1 = new Country("BG");
            var c2 = new Country();
            var c3 = new Country("CH");

            Assert.AreNotEqual(c1.GetHashCode(), c2.GetHashCode());
            Assert.AreNotEqual(c2.GetHashCode(), c3.GetHashCode());
            Assert.AreNotEqual(c1.GetHashCode(), c3.GetHashCode());
        }

        [Test]
        public void Country_GetHashCode_ReturnsSameHashForSameCountries()
        {
            var c3 = new Country("CH");
            var c3a = new Country("CH");
            Assert.AreEqual(c3.GetHashCode(), c3a.GetHashCode());
        }
    }
}
