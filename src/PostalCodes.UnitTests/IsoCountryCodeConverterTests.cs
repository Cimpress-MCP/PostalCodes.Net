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
		public void GetIso3166p3Code_WithIso3166p1Code_ReturnsIso3166p3Code(string input, string output)
        {
			Assert.AreEqual(output, IsoConverter.GetIso3166p3Code(input));
        }

        [Test]
        [TestCase("US")]
        [TestCase("BM")]
        [TestCase("CA")]
        [TestCase("DE")]
		public void GetIso3166p3Code_WithIso3166p1Code_ReturnsIso3166p1Code(string input)
        {
			Assert.AreEqual(input, IsoConverter.GetIso3166p3Code(input));
        }

        [Test]
        [TestCase("IH")]
        [TestCase("KJ")]
        [TestCase("IY")]
        [TestCase("ZU")]
		public void GetIso3166p3Code_WithUnassignedUserDefinedOrNotUsedCode_ThrowsInvalidOperationException(string input)
        {
			Assert.Throws<InvalidOperationException>(() => IsoConverter.GetIso3166p3Code(input));
        }
    }
}
