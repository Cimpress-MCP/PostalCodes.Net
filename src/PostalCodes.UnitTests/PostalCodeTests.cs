using NUnit.Framework;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes.UnitTests
{
    [TestFixture]
    public class PostalCodeTests
    {
        [Test]
        public void PostalCode_OperatorLessThanGreaterThan()
        {
            var p1 = new DefaultPostalCode("1000");
            var p2 = new DefaultPostalCode("1001");
            Assert.IsTrue(p1 < p2);
            Assert.IsFalse(p2 < p1);

            Assert.IsTrue(p2 > p1);
            Assert.IsFalse(p1 > p2);

            Assert.IsTrue(p2 > null);
            Assert.IsTrue(null > p2);
            Assert.IsTrue(null < p2);

            PostalCode nullPostalCode = null;

            Assert.IsFalse(nullPostalCode < null);
            Assert.IsFalse(nullPostalCode > null);

            Assert.IsFalse(p2 < null);
        }

        [Test]
        public void PostalCode_Equals()
        {
            var p1 = new DefaultPostalCode("1000"); 
            var p11 = new DefaultPostalCode("1000");
            var p2 = new DefaultPostalCode("1001");

            Assert.IsFalse(p1.Equals((object)p2));
            Assert.IsFalse(p2.Equals((object)p1));

            Assert.IsFalse(p1.Equals(p2));
            Assert.IsFalse(p2.Equals(p1));
            Assert.IsFalse(p1.Equals(null));

            Assert.IsTrue(p1.Equals(p11));
            Assert.IsTrue(p11.Equals(p1));
        }

        [Test]
        public void PostalCode_CompareTo()
        {
            var p1 = PostalCodeFactory.Instance.CreatePostalCode(new Country("BG"), "456");
            var p2 = PostalCodeFactory.Instance.CreatePostalCode(new Country("BG"), "1123");

            Assert.IsTrue(p1 < p2);
        }
    }
}
