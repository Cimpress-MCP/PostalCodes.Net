using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class ILPostalCodeTests
    {

        [TestCase("1126394","1126393")]
        [TestCase("8286898","8286897")]
        [TestCase("0000001","0000000")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new ILPostalCode(postalCode);
            var codePredecessor = new ILPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("1427934","1427935")]
        [TestCase("4487952","4487953")]
        [TestCase("9999998","9999999")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new ILPostalCode(postalCode);
            var codeSuccessor = new ILPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());
        }
        
        [TestCase("0000000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new ILPostalCode(postalCode)).Predecessor);
        }

        [TestCase("9999999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new ILPostalCode(postalCode)).Successor);
        }

        [TestCase("12233344235")]
        [TestCase("d1s223s")]
        [TestCase("x12d3")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new ILPostalCode(postalCode));
        }

        public void CompareTo_ReturnsExpectedSign(string postalCodeBefore, string postalCodeAfter)
        {
            var b = new ILPostalCode(postalCodeBefore);
            var a = new ILPostalCode(postalCodeAfter);
            Assert.AreEqual(-1, b.CompareTo(a));
            Assert.AreEqual( 1, a.CompareTo(b));
        }
        [TestCase("1122334")]
        [TestCase("2525678")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new ILPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        [TestCase("1122334")]
        [TestCase("2525678")]
        public void Equals_WithOtherObject_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new ILPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(new object());
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("1122334")]
        [TestCase("2525678")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new ILPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(ILPostalCode));
        }

        [TestCase("1122334")]
        [TestCase("2525678")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new ILPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(ILPostalCode));
        }

        [TestCase("1122334")]
        [TestCase("2525678")]
        public void ExpandPostalCodeAsHighestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new ILPostalCode(code)).ExpandPostalCodeAsHighestInRange();
            Assert.IsTrue(x.GetType() == typeof(ILPostalCode));
        }

        [TestCase("1122334")]
        [TestCase("2525678")]
        public void ExpandPostalCodeAsLowestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new ILPostalCode(code)).ExpandPostalCodeAsLowestInRange();
            Assert.IsTrue(x.GetType() == typeof(ILPostalCode));
        }

        [TestCase("1122334")]
        [TestCase("2525678")]
        public void GetHashCode_WithEqualObject_EqualHashes(string code)
        {
            var x = new ILPostalCode(code);
            var y = new ILPostalCode(code);
            Assert.IsTrue(x.GetHashCode() == y.GetHashCode());
        }

        [TestCase("1122334")]
        [TestCase("2525678")]
        public void AreAdjacent_WithAdjacentPostalCodes_ReturnsTrue(string code)
        {
            var x = new ILPostalCode(code);
            var xPred = x.Predecessor;
            var xSucc = x.Successor;
            Assert.IsTrue(PostalCode.AreAdjacent(x, xPred));
            Assert.IsTrue(PostalCode.AreAdjacent(xPred, x));
            Assert.IsTrue(PostalCode.AreAdjacent(x, xSucc));
            Assert.IsTrue(PostalCode.AreAdjacent(xSucc, x));
            Assert.IsFalse(PostalCode.AreAdjacent(xPred, xSucc));
        }             

        [TestCase("1122334")]
        [TestCase("2525678")]
        public void CreateThroughFactoryIsSuccessful(string code)
        {
            var country = CountryFactory.Instance.CreateCountry("IL");
            var x = PostalCodeFactory.Instance.CreatePostalCode(country, code);
            
            Assert.IsTrue(x.GetType() == typeof(ILPostalCode));
        }             
    }
}
