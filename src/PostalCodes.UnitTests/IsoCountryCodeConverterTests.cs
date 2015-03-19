using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests
{
	internal class IsoCountryCodeConverterTests
    {
		private static readonly IsoCountryCodeConverter IsoConverter = new IsoCountryCodeConverter();

        [Test]
        [TestCase("GB", "UK")]
        [TestCase("BQ", "AN")]
        [TestCase("SX", "AN")]
        [TestCase("CW", "AN")]
        public void GetVistaprintCountryCode_VPSpecificCountryCode_ReturnsVistaprintCountryCode(string input,
                                                                                                string output)
        {
			Assert.AreEqual(output, IsoConverter.GetIso3166p3Code(input));
        }

        [Test]
        [TestCase("US")]
        [TestCase("BM")]
        [TestCase("CA")]
        [TestCase("DE")]
        public void GetVistaprintCountryCode_NonVPSpecificCountryCode_ReturnsSameCountryCode(string input)
        {
			Assert.AreEqual(input, IsoConverter.GetIso3166p3Code(input));
        }

        [Test]
        [TestCase("IH")]
        [TestCase("KJ")]
        [TestCase("IY")]
        [TestCase("ZU")]
        public void GetVistaprintCountryCode_NonIsoCountryCode_ThrowsException(string input)
        {
			Assert.Throws<InvalidOperationException>(() => IsoConverter.GetIso3166p3Code(input));
        }
    }
}
