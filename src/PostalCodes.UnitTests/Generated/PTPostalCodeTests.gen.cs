using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class PTPostalCodeTests
    {

        [TestCase("1234","1233")]
        [TestCase("1234567","1234566")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new PTPostalCode(postalCode);
            var codePredecessor = new PTPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("9999998","9999999")]
        [TestCase("1248192","1248193")]
        [TestCase("1235","1236")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new PTPostalCode(postalCode);
            var codeSuccessor = new PTPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());
        }
        
        [TestCase("0000000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new PTPostalCode(postalCode)).Predecessor);
        }

        [TestCase("9999999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new PTPostalCode(postalCode)).Successor);
        }

        [TestCase("1d2345")]
        [TestCase("xxx123")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new PTPostalCode(postalCode));
        }

        public void CompareTo_ReturnsExpectedSign(string postalCodeBefore, string postalCodeAfter)
        {
            var b = new PTPostalCode(postalCodeBefore);
            var a = new PTPostalCode(postalCodeAfter);
            Assert.AreEqual(-1, b.CompareTo(a));
            Assert.AreEqual( 1, a.CompareTo(b));
        }
        [TestCase("1231242")]
        [TestCase("1234")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new PTPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        [TestCase("1231242")]
        [TestCase("1234")]
        public void Equals_WithOtherObject_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new PTPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(new object());
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("1231242")]
        [TestCase("1234")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new PTPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(PTPostalCode));
        }

        [TestCase("1231242")]
        [TestCase("1234")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new PTPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(PTPostalCode));
        }

        [TestCase("1231242")]
        [TestCase("1234")]
        public void ExpandPostalCodeAsHighestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new PTPostalCode(code)).ExpandPostalCodeAsHighestInRange();
            Assert.IsTrue(x.GetType() == typeof(PTPostalCode));
        }

        [TestCase("1231242")]
        [TestCase("1234")]
        public void ExpandPostalCodeAsLowestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new PTPostalCode(code)).ExpandPostalCodeAsLowestInRange();
            Assert.IsTrue(x.GetType() == typeof(PTPostalCode));
        }

        [TestCase("1231242")]
        [TestCase("1234")]
        public void GetHashCode_WithEqualObject_EqualHashes(string code)
        {
            var x = new PTPostalCode(code);
            var y = new PTPostalCode(code);
            Assert.IsTrue(x.GetHashCode() == y.GetHashCode());
        }

        [TestCase("1231242")]
        [TestCase("1234")]
        public void AreAdjacent_WithAdjacentPostalCodes_ReturnsTrue(string code)
        {
            var x = new PTPostalCode(code);
            var xPred = x.Predecessor;
            var xSucc = x.Successor;
            Assert.IsTrue(PostalCode.AreAdjacent(x, xPred));
            Assert.IsTrue(PostalCode.AreAdjacent(xPred, x));
            Assert.IsTrue(PostalCode.AreAdjacent(x, xSucc));
            Assert.IsTrue(PostalCode.AreAdjacent(xSucc, x));
            Assert.IsFalse(PostalCode.AreAdjacent(xPred, xSucc));
        }             

        [TestCase("1231242")]
        [TestCase("1234")]
        public void CreateThroughFactoryIsSuccessful(string code)
        {
            var country = CountryFactory.Instance.CreateCountry("PT");
            var x = PostalCodeFactory.Instance.CreatePostalCode(country, code);
            
            Assert.IsTrue(x.GetType() == typeof(PTPostalCode));
        }             
    }
}
