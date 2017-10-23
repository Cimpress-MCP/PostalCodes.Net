using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class GBPostalCodeTests
    {

        [TestCase("ZZ9Z9ZZ","ZZ9Z9ZY")]
        [TestCase("Z999ZZ","Z999ZY")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new GBPostalCode(postalCode);
            var codePredecessor = new GBPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("Z989ZZ","Z990AA")]
        [TestCase("ZZ0Z9","ZZ1A0")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new GBPostalCode(postalCode);
            var codeSuccessor = new GBPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());
        }
        
        [TestCase("AA0A0AA")]
        [TestCase("A0A0AA")]
        [TestCase("A00AA")]
        [TestCase("A000AA")]
        [TestCase("AA00AA")]
        [TestCase("AA000AA")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new GBPostalCode(postalCode)).Predecessor);
        }

        [TestCase("ZZ9Z9ZZ")]
        [TestCase("Z9Z9ZZ")]
        [TestCase("Z99ZZ")]
        [TestCase("Z999ZZ")]
        [TestCase("ZZ99ZZ")]
        [TestCase("ZZ999ZZ")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new GBPostalCode(postalCode)).Successor);
        }

        [TestCase("12345")]
        [TestCase("xxx123")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new GBPostalCode(postalCode));
        }

        public void CompareTo_ReturnsExpectedSign(string postalCodeBefore, string postalCodeAfter)
        {
            var b = new GBPostalCode(postalCodeBefore);
            var a = new GBPostalCode(postalCodeAfter);
            Assert.AreEqual(Math.Sign(-1), Math.Sign(b.CompareTo(a)));
            Assert.AreEqual(Math.Sign( 1), Math.Sign(a.CompareTo(b)));
        }
        [TestCase("ZZ9A9ZZ")]
        [TestCase("ZZ9A9")]
        [TestCase("A9Z9ZZ")]
        [TestCase("Z29ZZ")]
        [TestCase("Z699ZZ")]
        [TestCase("ZX99ZZ")]
        [TestCase("ZC999ZZ")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new GBPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        [TestCase("ZZ9A9ZZ")]
        [TestCase("ZZ9A9")]
        [TestCase("A9Z9ZZ")]
        [TestCase("Z29ZZ")]
        [TestCase("Z699ZZ")]
        [TestCase("ZX99ZZ")]
        [TestCase("ZC999ZZ")]
        public void Equals_WithOtherObject_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new GBPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(new object());
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("ZZ9A9ZZ")]
        [TestCase("ZZ9A9")]
        [TestCase("A9Z9ZZ")]
        [TestCase("Z29ZZ")]
        [TestCase("Z699ZZ")]
        [TestCase("ZX99ZZ")]
        [TestCase("ZC999ZZ")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new GBPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(GBPostalCode));
        }

        [TestCase("ZZ9A9ZZ")]
        [TestCase("ZZ9A9")]
        [TestCase("A9Z9ZZ")]
        [TestCase("Z29ZZ")]
        [TestCase("Z699ZZ")]
        [TestCase("ZX99ZZ")]
        [TestCase("ZC999ZZ")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new GBPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(GBPostalCode));
        }

        [TestCase("ZZ9A9ZZ")]
        [TestCase("ZZ9A9")]
        [TestCase("A9Z9ZZ")]
        [TestCase("Z29ZZ")]
        [TestCase("Z699ZZ")]
        [TestCase("ZX99ZZ")]
        [TestCase("ZC999ZZ")]
        public void ExpandPostalCodeAsHighestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new GBPostalCode(code)).ExpandPostalCodeAsHighestInRange();
            Assert.IsTrue(x.GetType() == typeof(GBPostalCode));
        }

        [TestCase("ZZ9A9ZZ")]
        [TestCase("ZZ9A9")]
        [TestCase("A9Z9ZZ")]
        [TestCase("Z29ZZ")]
        [TestCase("Z699ZZ")]
        [TestCase("ZX99ZZ")]
        [TestCase("ZC999ZZ")]
        public void ExpandPostalCodeAsLowestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new GBPostalCode(code)).ExpandPostalCodeAsLowestInRange();
            Assert.IsTrue(x.GetType() == typeof(GBPostalCode));
        }

        [TestCase("ZZ9A9ZZ")]
        [TestCase("ZZ9A9")]
        [TestCase("A9Z9ZZ")]
        [TestCase("Z29ZZ")]
        [TestCase("Z699ZZ")]
        [TestCase("ZX99ZZ")]
        [TestCase("ZC999ZZ")]
        public void GetHashCode_WithEqualObject_EqualHashes(string code)
        {
            var x = new GBPostalCode(code);
            var y = new GBPostalCode(code);
            Assert.IsTrue(x.GetHashCode() == y.GetHashCode());
        }

        [TestCase("ZZ9A9ZZ")]
        [TestCase("ZZ9A9")]
        [TestCase("A9Z9ZZ")]
        [TestCase("Z29ZZ")]
        [TestCase("Z699ZZ")]
        [TestCase("ZX99ZZ")]
        [TestCase("ZC999ZZ")]
        public void AreAdjacent_WithAdjacentPostalCodes_ReturnsTrue(string code)
        {
            var x = new GBPostalCode(code);
            var xPred = x.Predecessor;
            var xSucc = x.Successor;
            Assert.IsTrue(PostalCode.AreAdjacent(x, xPred));
            Assert.IsTrue(PostalCode.AreAdjacent(xPred, x));
            Assert.IsTrue(PostalCode.AreAdjacent(x, xSucc));
            Assert.IsTrue(PostalCode.AreAdjacent(xSucc, x));
            Assert.IsFalse(PostalCode.AreAdjacent(xPred, xSucc));
        }             

        [TestCase("ZZ9A9ZZ")]
        [TestCase("ZZ9A9")]
        [TestCase("A9Z9ZZ")]
        [TestCase("Z29ZZ")]
        [TestCase("Z699ZZ")]
        [TestCase("ZX99ZZ")]
        [TestCase("ZC999ZZ")]
        public void CreateThroughFactoryIsSuccessful(string code)
        {
            var country = CountryFactory.Instance.CreateCountry("GB");
            var x = PostalCodeFactory.Instance.CreatePostalCode(country, code);
            
            Assert.IsTrue(x.GetType() == typeof(GBPostalCode));
        }             
    }
}
