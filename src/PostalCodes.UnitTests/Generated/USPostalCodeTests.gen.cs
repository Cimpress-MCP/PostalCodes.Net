using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class USPostalCodeTests
    {

        [TestCase("12346","12345")]
        [TestCase("12346-1234","12346-1233")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new USPostalCode(postalCode);
            var codePredecessor = new USPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("12346","12347")]
        [TestCase("12346-1234","12346-1235")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new USPostalCode(postalCode);
            var codeSuccessor = new USPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());
        }
        
        [TestCase("00000")]
        [TestCase("00000-0000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new USPostalCode(postalCode)).Predecessor);
        }

        [TestCase("99999")]
        [TestCase("99999-9999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new USPostalCode(postalCode)).Successor);
        }

        [TestCase("x1231s")]
        [TestCase("1231sd")]
        [TestCase("1010101010")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new USPostalCode(postalCode));
        }

        [TestCase("1234", "1236")]
        [TestCase("1234-1231", "1236")]
        [TestCase("1235", "1237-7777")]
        [TestCase("1234-9999", "1235-0000")]
        public void CompareTo_ReturnsExpectedSign(string postalCodeBefore, string postalCodeAfter)
        {
            var b = new USPostalCode(postalCodeBefore);
            var a = new USPostalCode(postalCodeAfter);
            Assert.AreEqual(Math.Sign(-1), Math.Sign(b.CompareTo(a)));
            Assert.AreEqual(Math.Sign( 1), Math.Sign(a.CompareTo(b)));
        }
        [TestCase("12345")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new USPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        [TestCase("12345")]
        public void Equals_WithOtherObject_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new USPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(new object());
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("12345")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new USPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(USPostalCode));
        }

        [TestCase("12345")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new USPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(USPostalCode));
        }

        [TestCase("12345")]
        public void ExpandPostalCodeAsHighestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new USPostalCode(code)).ExpandPostalCodeAsHighestInRange();
            Assert.IsTrue(x.GetType() == typeof(USPostalCode));
        }

        [TestCase("12345")]
        public void ExpandPostalCodeAsLowestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new USPostalCode(code)).ExpandPostalCodeAsLowestInRange();
            Assert.IsTrue(x.GetType() == typeof(USPostalCode));
        }

        [TestCase("12345")]
        public void GetHashCode_WithEqualObject_EqualHashes(string code)
        {
            var x = new USPostalCode(code);
            var y = new USPostalCode(code);
            Assert.IsTrue(x.GetHashCode() == y.GetHashCode());
        }

        [TestCase("12345")]
        public void AreAdjacent_WithAdjacentPostalCodes_ReturnsTrue(string code)
        {
            var x = new USPostalCode(code);
            var xPred = x.Predecessor;
            var xSucc = x.Successor;
            Assert.IsTrue(PostalCode.AreAdjacent(x, xPred));
            Assert.IsTrue(PostalCode.AreAdjacent(xPred, x));
            Assert.IsTrue(PostalCode.AreAdjacent(x, xSucc));
            Assert.IsTrue(PostalCode.AreAdjacent(xSucc, x));
            Assert.IsFalse(PostalCode.AreAdjacent(xPred, xSucc));
        }             

        [TestCase("12345")]
        public void CreateThroughFactoryIsSuccessful(string code)
        {
            var country = CountryFactory.Instance.CreateCountry("US");
            var x = PostalCodeFactory.Instance.CreatePostalCode(country, code);
            
            Assert.IsTrue(x.GetType() == typeof(USPostalCode));
        }             
    }
}
