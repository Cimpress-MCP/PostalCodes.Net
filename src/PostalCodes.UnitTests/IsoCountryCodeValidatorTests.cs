using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests
{
    internal class IsoCountryCodeValidatorTests
    {
        private static readonly IIsoCountryCodeValidator IsoValidator = new IsoCountryCodeValidator();
 
        [Test]
        public void Validate_ValidCountryCode_ReturnsTrue()
        {
            Assert.IsTrue(IsoValidator.Validate("US"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("ZZ")]
        [TestCase("FFF")]
        public void Validate_InvalidCountryCode_ReturnsFalse(string countryCode)
        {
            Assert.IsFalse(IsoValidator.Validate(countryCode));
        }

        [Test]
        [TestCase("US", "US")]
        [TestCase(" US", "US")]
        [TestCase("US ", "US")]
        [TestCase(" US ", "US")]
        [TestCase("us", "US")]
        public void GetNormalizedCountryCode_ReturnsValidCountryCode(string input, string output)
        {
            Assert.AreEqual(output, IsoValidator.GetNormalizedCountryCode(input));
        }

        [Test]
        [TestCase(null, "Country code must not be null.")]
        public void GetNormalizedCountryCode_NullCountryCode_ThrowsInvalidOperationException(string input, string output)
        {
            Assert.Throws<InvalidOperationException>(() => IsoValidator.GetNormalizedCountryCode(input), output);
        }

        [Test]
        [TestCase("", "Country code must contain exactly two characters.")]
        [TestCase("U", "Country code must contain exactly two characters.")]
        [TestCase("USA", "Country code must contain exactly two characters.")]
        public void GetNormalizedCountryCode_InvalidNumberOfChars_ThrowsInvalidOperationException(string input, string output)
        {
            Assert.Throws<InvalidOperationException>(() => IsoValidator.GetNormalizedCountryCode(input), output);
        }

        [Test]
        [TestCase("UK", "GB")]
        [TestCase("AN", "CW")]
        public void GetNormalizedCountryCode_NonStandardCountryCode_ReturnsIsoCountryCode(string input, string output)
        {
            Assert.AreEqual(output, IsoValidator.GetNormalizedCountryCode(input));
        }
    }
}
