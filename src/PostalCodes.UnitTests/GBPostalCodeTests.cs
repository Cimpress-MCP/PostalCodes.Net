using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace PostalCodes.UnitTests
{
    [TestFixture]
    internal class GBPostalCodeTests
    {
        [Test, Category("Integration")]
        public void WithBritishZipCode_ReturnsFalseIfFormatsDifferentLength()
        {
            var cFactory = new CountryFactory(new IsoCountryCodeValidator());
            var pcFactory = new PostalCodeFactory();
            var country = cFactory.CreateCountry("GB");
            var right = new PostalCodeRange(pcFactory.CreatePostalCode(country, "AA99 9AA"), pcFactory.CreatePostalCode(country, "BB99 9AA"));
            var left = new PostalCodeRange(pcFactory.CreatePostalCode(country, "A9A 9AA"), pcFactory.CreatePostalCode(country, "C9A 9AA"));
            Assert.IsFalse(PostalCodeRange.Contains(left, right));
        }

        [Test, Category("Integration")]
        public void WithBritishZipCode_ReturnsFalseIfFormatsSameLengthButDifferentFormat()
        {
            var cFactory = new CountryFactory(new IsoCountryCodeValidator());
            var pcFactory = new PostalCodeFactory();
            var country = cFactory.CreateCountry("GB");
            var right = new PostalCodeRange(pcFactory.CreatePostalCode(country, "A99 9AA"), pcFactory.CreatePostalCode(country, "B99 9AA"));
            var left = new PostalCodeRange(pcFactory.CreatePostalCode(country, "AA9 9AA"), pcFactory.CreatePostalCode(country, "CC9 9AA"));
            Assert.IsFalse(PostalCodeRange.Contains(left, right));
        }

        [Test, Category("Integration")]
        public void WithBritishZipCode_ReturnsTrueIfFormatMatchedAndCodesAreContained()
        {
            var cFactory = new CountryFactory(new IsoCountryCodeValidator());
            var pcFactory = new PostalCodeFactory();
            var country = cFactory.CreateCountry("GB");
            var right = new PostalCodeRange(pcFactory.CreatePostalCode(country, "AA9 9AA"), pcFactory.CreatePostalCode(country, "BB9 9AA"));
            var left = new PostalCodeRange(pcFactory.CreatePostalCode(country, "AA9 9AA"), pcFactory.CreatePostalCode(country, "CC9 9AA"));
            Assert.IsTrue(PostalCodeRange.Contains(left, right));
        }
    }
}
