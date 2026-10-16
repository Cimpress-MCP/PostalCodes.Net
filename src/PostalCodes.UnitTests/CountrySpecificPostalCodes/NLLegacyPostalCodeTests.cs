using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.CountrySpecificPostalCodes
{
    [TestFixture]
    internal class NLLegacyPostalCodeTests
    {
        private static readonly Country NL = CountryFactory.Instance.CreateCountry("NL");
        private static readonly PostalCodeFactory LegacyFactory = new PostalCodeFactory(new PostalCodeFactoryOptions { UseLegacyNetherlandsFormat = true });

        [Test]
        [TestCase("0024", "0024")]
        [TestCase("0024 ZZ", "0024ZZ")]
        [TestCase("0000AA", "0000AA")]
        [TestCase("1234SA", "1234SA")]
        [TestCase("1234 SD", "1234SD")]
        [TestCase("1234SS", "1234SS")]
        [TestCase("24", "0024")]
        [TestCase("1012 JT", "1012JT")]
        public void CreatePostalCode_LegacyValidCode_IsCreated(string postalCode, string expected)
        {
            var x = LegacyFactory.CreatePostalCode(NL, postalCode);

            Assert.AreEqual(expected, x.ToString());
            Assert.IsInstanceOf<NLPostalCode>(x);
        }

        [Test]
        [TestCase("0024")]
        [TestCase("1234SA")]
        [TestCase("24")]
        public void CreatePostalCode_DefaultFactory_StillRejectsLegacyCode(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => PostalCodeFactory.Instance.CreatePostalCode(NL, postalCode));
            Assert.Throws<ArgumentException>(() => new PostalCodeFactory(new PostalCodeFactoryOptions()).CreatePostalCode(NL, postalCode));
        }

        [Test]
        [TestCase("12j4h")]
        [TestCase("k3j51l")]
        public void CreatePostalCode_InvalidCode_Throws(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => LegacyFactory.CreatePostalCode(NL, postalCode));
        }

        [Test]
        public void TryCreatePostalCode_LegacyCode_ReturnsTrue()
        {
            PostalCode x;

            Assert.IsTrue(LegacyFactory.TryCreatePostalCode(NL, "0024 AB", out x));
            Assert.AreEqual("0024AB", x.ToString());
            Assert.IsFalse(LegacyFactory.TryCreatePostalCode(NL, "12j4h", out x));
            Assert.IsNull(x);
        }

        [Test]
        [TestCase("1000", "0999")]
        [TestCase("1000AA", "0999ZZ")]
        [TestCase("9999ZZ", "9999ZY")]
        public void Predecessor_StaysLegacy(string postalCode, string expected)
        {
            var x = LegacyFactory.CreatePostalCode(NL, postalCode).Predecessor;

            Assert.AreEqual(expected, x.ToString());
            Assert.IsTrue(x.GetType() == typeof(NLLegacyPostalCode));
            Assert.AreEqual(expected, x.Predecessor.Successor.ToString());
        }

        [Test]
        [TestCase("0999", "1000")]
        [TestCase("0999ZZ", "1000AA")]
        public void Successor_StaysLegacy(string postalCode, string expected)
        {
            var x = LegacyFactory.CreatePostalCode(NL, postalCode).Successor;

            Assert.AreEqual(expected, x.ToString());
            Assert.IsTrue(x.GetType() == typeof(NLLegacyPostalCode));
        }

        [Test]
        [TestCase("0000")]
        [TestCase("0000AA")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull(LegacyFactory.CreatePostalCode(NL, postalCode).Predecessor);
        }

        [Test]
        public void LegacyAndStrict_SameCode_AreEqualWithSameFormatName()
        {
            var legacy = LegacyFactory.CreatePostalCode(NL, "1012 JT");
            var strict = PostalCodeFactory.Instance.CreatePostalCode(NL, "1012 JT");

            Assert.AreEqual(strict, legacy);
            Assert.AreEqual(legacy, strict);
            Assert.AreEqual(strict.GetHashCode(), legacy.GetHashCode());
            Assert.AreEqual(strict.PostalCodeFormat.Name, legacy.PostalCodeFormat.Name);
        }

        [Test]
        public void LegacyFactory_OtherCountry_Unchanged()
        {
            var x = LegacyFactory.CreatePostalCode(CountryFactory.Instance.CreateCountry("PT"), "0042");

            Assert.AreEqual("PTPostalCode", x.GetType().Name);
        }

        [Test]
        public void Constructor_NullOptions_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new PostalCodeFactory(null));
        }
    }
}
