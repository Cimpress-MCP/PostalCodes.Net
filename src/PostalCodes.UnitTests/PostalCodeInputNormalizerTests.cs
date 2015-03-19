using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests
{
    internal class PostalCodeInputNormalizerTests
    {
        private readonly IPostalCodeInputNormalizer Normalizer = new PostalCodeInputNormalizer();

        [TestCase("AT")]
        [TestCase("AU")]
        [TestCase("BG")]
        [TestCase("CH")]
        [TestCase("DK")]
        [TestCase("HU")]
        [TestCase("NL")]
        [TestCase("NO")]
        [TestCase("SI")]
        [TestCase("NZ")]
        [TestCase("BE")]
        [TestCase("CY")]
        public void Normalize_ValidFourDigits_ReturnsValidPostalCode(string country)
        {
            Assert.AreEqual("0234", Normalizer.Normalize(CountryFactory.Instance.CreateCountry(country), "234", true));
            Assert.AreEqual("0234", Normalizer.Normalize(CountryFactory.Instance.CreateCountry(country), "234", false));
        }

        [TestCase("DE")]
        [TestCase("CZ")]
        [TestCase("EE")]
        [TestCase("ES")]
        [TestCase("FI")]
        [TestCase("FR")]
        [TestCase("GR")]
        [TestCase("IT")]
        [TestCase("PL")]
        [TestCase("SE")]
        [TestCase("SK")]
        [TestCase("TR")]
        [TestCase("US")]
        [TestCase("MY")]
        [TestCase("HR")]
        [TestCase("MX")]
        public void Normalize_ValidFiveDigits_ReturnsValidPostalCode(string country)
        {
            Assert.AreEqual("01234", Normalizer.Normalize(CountryFactory.Instance.CreateCountry(country), "1234", true));
            Assert.AreEqual("01234", Normalizer.Normalize(CountryFactory.Instance.CreateCountry(country), "1234", false));
        }

        [TestCase("IN")]
        [TestCase("SG")]
        public void Normalize_ValidSixDigits_ReturnsValidPostalCode(string country)
        {
            Assert.AreEqual("001234", Normalizer.Normalize(CountryFactory.Instance.CreateCountry(country), "1234", true));
            Assert.AreEqual("001234", Normalizer.Normalize(CountryFactory.Instance.CreateCountry(country), "1234", false));
        }

        [TestCase("JP")]
        public void Normalize_ValidSevenDigits_ReturnsValidPostalCode(string country)
        {
            Assert.AreEqual("0001234", Normalizer.Normalize(CountryFactory.Instance.CreateCountry(country), "1234", true));
            Assert.AreEqual("0001234", Normalizer.Normalize(CountryFactory.Instance.CreateCountry(country), "1234", false));
        }

        [Test]
        public void Normalize_ValidPortugalPostalCode_ReturnsValidPostalCode()
        {
            Assert.AreEqual("1234000", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("PT"), "1234", true));
            Assert.AreEqual("1234999", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("PT"), "1234", false));
            Assert.AreEqual("0123000", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("PT"), "123", true));
            Assert.AreEqual("0123999", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("PT"), "123", false));
            Assert.AreEqual("0999000", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("PT"), "999000", true));
            Assert.AreEqual("0999999", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("PT"), "999999", false));
        }

        [Test]
        public void Normalize_ValidRussiaPostalCode_ReturnsValidPostalCode()
        {
            Assert.AreEqual("123000", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("RU"), "123", true));
            Assert.AreEqual("123999", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("RU"), "123", false));
            Assert.AreEqual("012000", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("RU"), "12", true));
            Assert.AreEqual("012999", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("RU"), "12", false));
            Assert.AreEqual("012000", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("RU"), "12000", true));
            Assert.AreEqual("012999", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("RU"), "12999", false));
        }

        [Test]
        public void Normalize_ValidGreatBritainPostalCode_ReturnsValidPostalCode()
        {
            Assert.AreEqual("AB1C2", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("GB"), "AB1C2", true));
            Assert.AreEqual("AB1C2", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("GB"), "AB 1C2", true));
        }

        [Test]
        public void Normalize_ValidCanadaPostalCode_ReturnsValidPostalCode()
        {
            Assert.AreEqual("A1B2C3", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("CA"), "A1B2C3", true));
            Assert.AreEqual("A1B2C3", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("CA"), "A1B2C3", false));
            Assert.AreEqual("A1B2C3", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("CA"), "A1B 2C3", true));
            Assert.AreEqual("A1B2C3", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("CA"), "A1B 2C3", false));
            Assert.AreEqual("A1B2C3", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("CA"), "A1B   2C3", true));
            Assert.AreEqual("A1B2C3", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("CA"), "A1B   2C3", false));
        }

        [Test]
        public void Normalize_ValidBarbadosPostalCode_ReturnsValidPostalCode()
        {
            Assert.AreEqual("23024", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("BB"), "23024", true));
            // The prefix 'BB' is optionally supported (but normalizes without it.)
            Assert.AreEqual("23024", Normalizer.Normalize(CountryFactory.Instance.CreateCountry("BB"), "BB23024", true));
        }

        [TestCase("PLA1572", "PLA1572")]
        [TestCase("PLA 1572", "PLA1572")]
        [TestCase("PLA   1572", "PLA1572")]
        public void Normalize_ValidMaltaPostalCode_ReturnsValidPostalCode(string input, string output)
        {
            Assert.AreEqual(output, Normalizer.Normalize(CountryFactory.Instance.CreateCountry("MT"), input, true));
            Assert.AreEqual(output, Normalizer.Normalize(CountryFactory.Instance.CreateCountry("MT"), input, false));
        }

        [TestCase("IE")]
        [TestCase("AN")]
        public void Normalize_NoNormalizationFormatDefined_ReturnsInput(string country)
        {
            Assert.AreEqual("23024", Normalizer.Normalize(CountryFactory.Instance.CreateCountry(country), "23024", true));
            Assert.AreEqual("23024", Normalizer.Normalize(CountryFactory.Instance.CreateCountry(country), "23024", false));
            Assert.AreEqual("ABC123", Normalizer.Normalize(CountryFactory.Instance.CreateCountry(country), "ABC123", true));
            Assert.AreEqual("ABC123", Normalizer.Normalize(CountryFactory.Instance.CreateCountry(country), "ABC123", false));
        }

        [TestCase("12345")]
        [TestCase("123A")]
        public void Normalize_InvalidFourDigits_ThrowsInvalidOperationException(string postalCode)
        {
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("AT"), postalCode, true), Throws.InstanceOf<InvalidOperationException>());
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("AT"), postalCode, false), Throws.InstanceOf<InvalidOperationException>());
        }

        [TestCase("123456")]
        [TestCase("1234A")]
        public void Normalize_InvalidFiveDigits_ThrowsInvalidOperationException(string postalCode)
        {
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("DE"), postalCode, true), Throws.InstanceOf<InvalidOperationException>());
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("DE"), postalCode, false), Throws.InstanceOf<InvalidOperationException>());
        }

        [TestCase("1234567")]
        [TestCase("1234A6")]
        public void Normalize_InvalidSixDigits_ThrowsInvalidOperationException(string postalCode)
        {
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("IN"), postalCode, true), Throws.InstanceOf<InvalidOperationException>());
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("IN"), postalCode, false), Throws.InstanceOf<InvalidOperationException>());
        }

        [Test]
        [TestCase("123A")]
        public void Normalize_InvalidPortugalPostalCode_ThrowsInvalidOperationException(string postalCode)
        {
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("PT"), postalCode, true), Throws.InstanceOf<InvalidOperationException>());
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("PT"), postalCode, false), Throws.InstanceOf<InvalidOperationException>());
        }

        [TestCase("123B")]
        public void Normalize_InvalidRussiaPostalCode_ThrowsInvalidOperationException(string postalCode)
        {
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("RU"), postalCode, true), Throws.InstanceOf<InvalidOperationException>());
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("RU"), postalCode, false), Throws.InstanceOf<InvalidOperationException>());
        }

        [TestCase("ABCD1")]
        [TestCase("AB CD1")]
        [TestCase("AB12C")]
        [TestCase("AB140AA")]
        public void Normalize_InvalidUnitedKingdomPostalCode_ThrowsInvalidOperationException(string postalCode)
        {
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("GB"), postalCode, true), Throws.InstanceOf<InvalidOperationException>());
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("GB"), postalCode, false), Throws.InstanceOf<InvalidOperationException>());
        }

        [TestCase("A1B2CC")]
        [TestCase("AAB2C3")]
        [TestCase("11B2C3")]
        public void Normalize_InvalidCanadaPostalCode_ThrowsInvalidOperationException(string postalCode)
        {
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("CA"), postalCode, true), Throws.InstanceOf<InvalidOperationException>());
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("CA"), postalCode, false), Throws.InstanceOf<InvalidOperationException>());
        }

        [TestCase("B23034")]
        [TestCase("BBB23034")]
        public void Normalize_InvalidBarbadosPostalCode_ThrowsInvalidOperationException(string postalCode)
        {
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("BB"), postalCode, true), Throws.InstanceOf<InvalidOperationException>());
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("BB"), postalCode, false), Throws.InstanceOf<InvalidOperationException>());
        }

        [TestCase("PL1234")]
        [TestCase("PL 1234")]
        [TestCase("PLA12")]
        [TestCase("PLA123")]
        [TestCase("1234")]
        public void Normalize_InvalidMaltaPostalCode_ThrowsInvalidOperationException(string postalCode)
        {
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("MT"), postalCode, true), Throws.InstanceOf<InvalidOperationException>());
            Assert.That(() => Normalizer.Normalize(CountryFactory.Instance.CreateCountry("MT"), postalCode, false), Throws.InstanceOf<InvalidOperationException>());
        }

        [TestCase("PL", "12-345", "12345")]
        [TestCase("PL", "12345", "12345")]
        [TestCase("PL", "1234", "01234")]
        [TestCase("PL", "12 345", "12345")]
        [TestCase("CA", "A0A 0A0", "A0A0A0")]
        [TestCase("CA", "A0A0A0", "A0A0A0")]
        [TestCase("CA", "A0A", "A0A")]
        [TestCase("SE", "123 45", "12345")]
        [TestCase("SE", "12345", "12345")]
        [TestCase("SE", "1234", "01234")]
        [TestCase("NL", "1234 AA", "1234")]
        [TestCase("NL", "1234A", "1234")]
        [TestCase("NL", "1234AA", "1234")]
        [TestCase("NL", "123 AA", "0123")]
        [TestCase("US", "12345-6789", "12345")]
        [TestCase("US", "12345 6789", "12345")]
        [TestCase("US", "1234-5678", "01234")]
        [TestCase("US", "1234 6789", "12346")]
        [TestCase("US", "A12345", "A1234")]
        [TestCase("JP", "1234567", "1234567")]
        [TestCase("JP", "123-4567", "1234567")]
        [TestCase("JP", "1234567AA", "1234567")]
        [TestCase("JP", "12345AA", "12345AA")]
        [TestCase("JP", "12345", "0012345")]
        [TestCase("JP", "123 4567", "1234567")]
        [TestCase("BB", "BB12345", "12345")]
        [TestCase("BB", "BB123 45", "12345")]
        [TestCase("BB", "123 45", "12345")]
        [TestCase("BB", "BB12345AA", "12345")]
        [TestCase("BB", "bb12345AA", "12345")]
        [TestCase("BB", "bb1234B", "1234B")]
        [TestCase("BB", "bB12345", "12345")]
        [TestCase("BB", "Bb123", "00123")]
        [TestCase("BB", "1234AA", "1234A")]
        [TestCase("BB", "W1", "W1")]
        [TestCase("BB", "BB", "")]
        [TestCase("BB", "G03", "G03")]
        [TestCase("BE", "1234", "1234")]
        [TestCase("BE", "123 4", "1234")]
        [TestCase("BE", "12-34", "1234")]
        [TestCase("HR", "12345", "12345")]
        [TestCase("HR", "1 2345", "12345")]
        [TestCase("HR", "123-45", "12345")]
        [TestCase("CY", "1234", "1234")]
        [TestCase("CY", "123 4", "1234")]
        [TestCase("CY", "12-34", "1234")]
        [TestCase("MX", "12345", "12345")]
        [TestCase("MX", "1 2345", "12345")]
        [TestCase("MX", "123-45", "12345")]
        [TestCase("MT", "PLA 1234", "PLA1234")]
        [TestCase("MT", "PLA 12", "PLA12")]
        [TestCase("MT", "PLA1234", "PLA1234")]
        [TestCase("MT", "Pla1234", "PLA1234")]
        [TestCase("MT", "Pla-1234", "PLA1234")]
        [TestCase("MT", "PLA-1234", "PLA1234")]
        [TestCase("MT", "PLA - 1234", "PLA1234")]
        [TestCase("KR", "123123", "123123")]
        [TestCase("KR", "123-123", "123-123")]
        [TestCase("KR", "123 123", "123 123")]
        [TestCase("Random", "123456789", "123456789")]
        [TestCase("Random", "1234 567 89", "123456789")]
        [TestCase("Random", "1- 234-567 89", "123456789")]
        [TestCase("Random", "234DA44", "234DA44")]
        public void NormalizePostalCodeToMatchZones_ValidAndInvalidNonGbPostalCodes_ReturnsFormattedNonGbPostalCodes(
            string countryCode, string inputPostalCode, string expectedOutputPostalCode)
        {
            var outputPostalCode = PostalCodeInputNormalizer.NormalizePostalCodeToMatchZones(countryCode, inputPostalCode);
            Assert.AreEqual(expectedOutputPostalCode, outputPostalCode);
        }


        // Format 1
        [TestCase("A9 2", "A92")]
        [TestCase("A9 2B", "A92B")]
        [TestCase("A9 2BB", "A92")]

        // Format 2
        [TestCase("A9A 2", "A9A2")]
        [TestCase("A9A 2B", "A9A2B")]
        [TestCase("A9A 2BB", "A9A2")]

        // Format 3
        [TestCase("A99 2", "A992")]
        [TestCase("A99 2B", "A992B")]
        [TestCase("A99 2BB", "A992")]

        // Format 4
        [TestCase("AA9 2", "AA92")]
        [TestCase("AA9 2B", "AA92B")]
        [TestCase("AA9 2BB", "AA92")]
        
        // Format 5
        [TestCase("AA9A 2", "AA9A2")]
        [TestCase("AA9A 2B", "AA9A2B")]
        [TestCase("AA9A 2BB", "AA9A2")]

        // Format 6
        [TestCase("AA99 2", "AA992")]
        [TestCase("AA99 2B", "AA992B")]
        [TestCase("AA99 2BB", "AA992")]

        // normalize
        [TestCase("A9+A1ZZ", "A9A1")]
        [TestCase("A9+1ZZ", "A91")]
        [TestCase("AB-10AA", "AB10")]
        public void NormalizePostalCodeToMatchZones_ValidAndInvalidGbPostalCodes_ReturnsFormattedGbPostalCodes(string inputPostalCode, string expectedOutputPostalCode)
        {
            var outputPostalCode = PostalCodeInputNormalizer.NormalizePostalCodeToMatchZones("GB", inputPostalCode);
            Assert.AreEqual(expectedOutputPostalCode, outputPostalCode);
        }

        // Format 1 v.s. the other formats
        [TestCase("A11", "A92", true)]
        [TestCase("A1A1", "A92", false)]
        [TestCase("A111", "A92", false)]
        [TestCase("AA11", "A92", false)]
        [TestCase("AA1A1", "A92", false)]
        [TestCase("AA111", "A92", false)]

        // Format 2 v.s. the other formats
        [TestCase("A11", "A9A2", false)]
        [TestCase("A1A1", "A9A2", true)]
        [TestCase("A111", "A9A2", false)]
        [TestCase("AA11", "A9A2", false)]
        [TestCase("AA1A1", "A9A2", false)]
        [TestCase("AA111", "A9A2", false)]

        // Format 3 v.s. the other formats
        [TestCase("A11", "A992", false)]
        [TestCase("A1A1", "A992", false)]
        [TestCase("A111", "A992", true)]
        [TestCase("AA11", "A992", false)]
        [TestCase("AA1A1", "A992", false)]
        [TestCase("AA111", "A992", false)]

        // Format 4 v.s. the other formats
        [TestCase("A11", "AA92", false)]
        [TestCase("A1A1", "AA92", false)]
        [TestCase("A111", "AA92", false)]
        [TestCase("AA11", "AA92", true)]
        [TestCase("AA1A1", "AA92", false)]
        [TestCase("AA111", "AA92", false)]

        // Format 5 v.s. the other formats
        [TestCase("A11", "AA9A2", false)]
        [TestCase("A1A1", "AA9A2", false)]
        [TestCase("A111", "AA9A2", false)]
        [TestCase("AA11", "AA9A2", false)]
        [TestCase("AA1A1", "AA9A2", true)]
        [TestCase("AA111", "AA9A2", false)]

        // Format 6 v.s. the other formats
        [TestCase("A11", "AA992", false)]
        [TestCase("A1A1", "AA992", false)]
        [TestCase("A111", "AA992", false)]
        [TestCase("AA 11", "AA992", false)]
        [TestCase("AA1A1", "AA992", false)]
        [TestCase("AA11", "AA992", false)]
        [TestCase("AA111", "AA992", true)]
        public void CheckIfGBPostalCodeFormatsMatch_ValidAndInvalidGBPostalCodes_ReturnsTrueAndFalse(string startPostalCode, string quotePostalCode, bool expectedContains)
        {
            var contains = PostalCodeInputNormalizer.CheckIfGBPostalCodeFormatsMatch(quotePostalCode, startPostalCode);
            Assert.AreEqual(expectedContains, contains);
        }
       
    }
}
