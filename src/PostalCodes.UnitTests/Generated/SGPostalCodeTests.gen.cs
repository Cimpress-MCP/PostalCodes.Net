using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class SGPostalCodeTests
    {

        [TestCase("112634","112633")]
        [TestCase("828688","828687")]
        [TestCase("000001","000000")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new SGPostalCode(postalCode);
            var codePredecessor = new SGPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("142934","142935")]
        [TestCase("448952","448953")]
        [TestCase("999998","999999")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new SGPostalCode(postalCode);
            var codeSuccessor = new SGPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());
        }
        
        [TestCase("000000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new SGPostalCode(postalCode)).Predecessor);
        }

        [TestCase("999999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new SGPostalCode(postalCode)).Successor);
        }

        [TestCase("12233345")]
        [TestCase("1s223s")]
        [TestCase("x12xx3")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new SGPostalCode(postalCode));
        }

        public void CompareTo_ReturnsExpectedSign(string postalCodeBefore, string postalCodeAfter)
        {
            var b = new SGPostalCode(postalCodeBefore);
            var a = new SGPostalCode(postalCodeAfter);
            Assert.AreEqual(-1, b.CompareTo(a));
            Assert.AreEqual( 1, a.CompareTo(b));
        }
        [TestCase("122334")]
        [TestCase("525678")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new SGPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        [TestCase("122334")]
        [TestCase("525678")]
        public void Equals_WithOtherObject_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new SGPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(new object());
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("122334")]
        [TestCase("525678")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new SGPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(SGPostalCode));
        }

        [TestCase("122334")]
        [TestCase("525678")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new SGPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(SGPostalCode));
        }

        [TestCase("122334")]
        [TestCase("525678")]
        public void ExpandPostalCodeAsHighestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new SGPostalCode(code)).ExpandPostalCodeAsHighestInRange();
            Assert.IsTrue(x.GetType() == typeof(SGPostalCode));
        }

        [TestCase("122334")]
        [TestCase("525678")]
        public void ExpandPostalCodeAsLowestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new SGPostalCode(code)).ExpandPostalCodeAsLowestInRange();
            Assert.IsTrue(x.GetType() == typeof(SGPostalCode));
        }

        [TestCase("122334")]
        [TestCase("525678")]
        public void GetHashCode_WithEqualObject_EqualHashes(string code)
        {
            var x = new SGPostalCode(code);
            var y = new SGPostalCode(code);
            Assert.IsTrue(x.GetHashCode() == y.GetHashCode());
        }

        [TestCase("122334")]
        [TestCase("525678")]
        public void AreAdjacent_WithAdjacentPostalCodes_ReturnsTrue(string code)
        {
            var x = new SGPostalCode(code);
            var xPred = x.Predecessor;
            var xSucc = x.Successor;
            Assert.IsTrue(PostalCode.AreAdjacent(x, xPred));
            Assert.IsTrue(PostalCode.AreAdjacent(xPred, x));
            Assert.IsTrue(PostalCode.AreAdjacent(x, xSucc));
            Assert.IsTrue(PostalCode.AreAdjacent(xSucc, x));
            Assert.IsFalse(PostalCode.AreAdjacent(xPred, xSucc));
        }             

        [TestCase("122334")]
        [TestCase("525678")]
        public void CreateThroughFactoryIsSuccessful(string code)
        {
            var country = CountryFactory.Instance.CreateCountry("SG");
            var x = PostalCodeFactory.Instance.CreatePostalCode(country, code);
            
            Assert.IsTrue(x.GetType() == typeof(SGPostalCode));
        }             
    }
}
