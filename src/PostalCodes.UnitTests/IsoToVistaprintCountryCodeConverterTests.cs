using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests
{
    internal class IsoToVistaprintCountryCodeConverterTests
    {
        private static readonly IsoToVistaprintCountryCodeConverter IsoConverter = new IsoToVistaprintCountryCodeConverter();

        [Test]
        [TestCase("GB", "UK")]
        [TestCase("BQ", "AN")]
        [TestCase("SX", "AN")]
        [TestCase("CW", "AN")]
        public void GetVistaprintCountryCode_VPSpecificCountryCode_ReturnsVistaprintCountryCode(string input,
                                                                                                string output)
        {
            Assert.AreEqual(output, IsoConverter.GetVistaprintCountryCode(input));
        }

        [Test]
        [TestCase("US")]
        [TestCase("BM")]
        [TestCase("CA")]
        [TestCase("DE")]
        public void GetVistaprintCountryCode_NonVPSpecificCountryCode_ReturnsSameCountryCode(string input)
        {
            Assert.AreEqual(input, IsoConverter.GetVistaprintCountryCode(input));
        }

        [Test]
        [TestCase("IH")]
        [TestCase("KJ")]
        [TestCase("IY")]
        [TestCase("ZU")]
        public void GetVistaprintCountryCode_NonIsoCountryCode_ThrowsException(string input)
        {
            Assert.Throws<InvalidOperationException>(() => IsoConverter.GetVistaprintCountryCode(input));
        }
    }
}
