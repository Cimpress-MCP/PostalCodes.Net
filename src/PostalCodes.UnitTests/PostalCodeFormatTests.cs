using System;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml;

namespace PostalCodes.UnitTests
{
    [TestFixture]
    public class PostalCodeFormatTests
    {
        [Test]
        public void Equals_WithEqualObjects_ReturnsTrue()
        {
            var o1 = new PostalCodeFormat {
                Name = "5-Digits - 99999",
                RegexDefault = new Regex("^[0-9]{5}$", RegexOptions.Compiled),
                OutputDefault = "xxxxx",
                RegexShort = new Regex("aa", RegexOptions.Compiled),
                OutputShort = "asdasd",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "0",
                ShortExpansionAsHighestInRange = "9",
                LeftPaddingCharacter = "0",
            };

            var o2 = new PostalCodeFormat {
                Name = "5-Digits - 99999",
                RegexDefault = new Regex("^[0-9]{5}$", RegexOptions.Compiled),
                OutputDefault = "xxxxx",
                RegexShort = new Regex("aa", RegexOptions.Compiled),
                OutputShort = "asdasd",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "0",
                ShortExpansionAsHighestInRange = "9",
                LeftPaddingCharacter = "0",
            };

            Assert.IsTrue(o1.Equals(o2));
            Assert.IsTrue(o2.Equals(o1));
            Assert.IsTrue(o1.Equals(o1));
            Assert.IsTrue(o2.Equals(o2));
        }

        [Test]
        public void Equals_WithInequalObjects_ReturnsFalse()
        {
            var o1 = new PostalCodeFormat {
                Name = "5-Digits - 99999",
                RegexDefault = new Regex("^[0-9]{5}$", RegexOptions.Compiled),
                OutputDefault = "xxxxx",
                RegexShort = new Regex("aaaaaaaaa", RegexOptions.Compiled),
                OutputShort = "asdasd",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "0",
                ShortExpansionAsHighestInRange = "9",
                LeftPaddingCharacter = "0",
            };
           
            var x = new PostalCodeFormat {
                Name = o1.Name,
                RegexDefault = o1.RegexDefault,
                OutputDefault = o1.OutputDefault,
                RegexShort = o1.RegexShort,
                OutputShort = o1.OutputShort,
                AutoConvertToShort = o1.AutoConvertToShort,
                ShortExpansionAsLowestInRange = o1.ShortExpansionAsLowestInRange,
                ShortExpansionAsHighestInRange = o1.ShortExpansionAsHighestInRange,
                LeftPaddingCharacter = o1.LeftPaddingCharacter,
            };

            x.Name = "aa";
            Assert.IsFalse(o1.Equals(x));

            x.Name = o1.Name;
            x.RegexDefault = new Regex("aaa");
            Assert.IsFalse(o1.Equals(x));

            x.RegexDefault = o1.RegexDefault;
            x.OutputDefault = "x";
            Assert.IsFalse(o1.Equals(x));

            x.OutputDefault = o1.OutputDefault;
            x.RegexShort = new Regex("aaa");
            Assert.IsFalse(o1.Equals(x));

            x.RegexShort = o1.RegexShort;
            x.AutoConvertToShort = true;
            Assert.IsFalse(o1.Equals(x));

            x.AutoConvertToShort = o1.AutoConvertToShort;
            x.ShortExpansionAsLowestInRange = "dddd";
            Assert.IsFalse(o1.Equals(x));

            x.ShortExpansionAsLowestInRange = o1.ShortExpansionAsLowestInRange;
            x.ShortExpansionAsHighestInRange = "dddd";
            Assert.IsFalse(o1.Equals(x));

            x.ShortExpansionAsHighestInRange = o1.ShortExpansionAsHighestInRange;
            x.LeftPaddingCharacter = "dddd";
            Assert.IsFalse(o1.Equals(x));
        }

        [Test]
        public void Equals_NullOrIncorrectObject_ReturnsFalse()
        {
            var o1 = new PostalCodeFormat {
                Name = "5-Digits - 99999",
                RegexDefault = new Regex("^[0-9]{5}$", RegexOptions.Compiled),
                OutputDefault = "xxxxx",
                RegexShort = new Regex("aa", RegexOptions.Compiled),
                OutputShort = "asdasd",
                AutoConvertToShort = false,
                ShortExpansionAsLowestInRange = "0",
                ShortExpansionAsHighestInRange = "9",
                LeftPaddingCharacter = "0",
            };
           
            Assert.IsFalse(o1.Equals(null));
            Assert.IsFalse(o1.Equals("sdfsd"));
        }
    }
}

