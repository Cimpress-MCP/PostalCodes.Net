using NUnit.Framework;

namespace PostalCodes.UnitTests
{
    [TestFixture]
    internal class PostalCodeOutputNormalizerTests
    {
        [TestCase("PT", "1234567", "1234-567", true)]
        [TestCase("PT", "1234567", "1234-567", false)]
        [TestCase("PT", "1234000", "1234", true)]
        [TestCase("PT", "1234000", "1234-000", false)]
        [TestCase("GB", "DD111", "DD 111", true)]
        [TestCase("PL", "00001", "00-001", true)]
        [TestCase("NL", "1141","1141", true)]
        [TestCase("NL", "0102", "0102 ZZ", false)]
        [TestCase("SE", "11130", "111 30", true)]
        [TestCase("SK", "34230", "342 30", true)]
        [TestCase("CA", "G0X1L0", "G0X 1L0", true)]
        public void Normalize_ReturnsCorectPostalCode(string countryCode, string postalCodeInput,string postalCodeOutput, bool isPostalCodeStart)
        {
            var n = new PostalCodeOutputNormalizer();
            var result = n.Normalize(countryCode, postalCodeInput, isPostalCodeStart);
            Assert.AreEqual(postalCodeOutput, result);
        }

    }
}