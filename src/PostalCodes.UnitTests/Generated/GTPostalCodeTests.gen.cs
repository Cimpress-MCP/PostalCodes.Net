using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class GTPostalCodeTests
    {

        [TestCase("11234","11233")]
        [TestCase("82888","82887")]
        [TestCase("00001","00000")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new GTPostalCode(postalCode);
            var codePredecessor = new GTPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("14234","14235")]
        [TestCase("44852","44853")]
        [TestCase("99998","99999")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new GTPostalCode(postalCode);
            var codeSuccessor = new GTPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());
        }
        
        [TestCase("00000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new GTPostalCode(postalCode)).Predecessor);
        }

        [TestCase("99999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new GTPostalCode(postalCode)).Successor);
        }

        [TestCase("122345")]
        [TestCase("1223s")]
        [TestCase("x12u3")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new GTPostalCode(postalCode));
        }

        public void CompareTo_ReturnsExpectedSign(string postalCodeBefore, string postalCodeAfter)
        {
            var b = new GTPostalCode(postalCodeBefore);
            var a = new GTPostalCode(postalCodeAfter);
            Assert.AreEqual(-1, b.CompareTo(a));
            Assert.AreEqual( 1, a.CompareTo(b));
        }
        [TestCase("12234")]
        [TestCase("52678")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new GTPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        [TestCase("12234")]
        [TestCase("52678")]
        public void Equals_WithOtherObject_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new GTPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(new object());
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("12234")]
        [TestCase("52678")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new GTPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(GTPostalCode));
        }

        [TestCase("12234")]
        [TestCase("52678")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new GTPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(GTPostalCode));
        }

        [TestCase("12234")]
        [TestCase("52678")]
        public void ExpandPostalCodeAsHighestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new GTPostalCode(code)).ExpandPostalCodeAsHighestInRange();
            Assert.IsTrue(x.GetType() == typeof(GTPostalCode));
        }

        [TestCase("12234")]
        [TestCase("52678")]
        public void ExpandPostalCodeAsLowestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new GTPostalCode(code)).ExpandPostalCodeAsLowestInRange();
            Assert.IsTrue(x.GetType() == typeof(GTPostalCode));
        }

        [TestCase("12234")]
        [TestCase("52678")]
        public void GetHashCode_WithEqualObject_EqualHashes(string code)
        {
            var x = new GTPostalCode(code);
            var y = new GTPostalCode(code);
            Assert.IsTrue(x.GetHashCode() == y.GetHashCode());
        }

        [TestCase("12234")]
        [TestCase("52678")]
        public void AreAdjacent_WithAdjacentPostalCodes_ReturnsTrue(string code)
        {
            var x = new GTPostalCode(code);
            var xPred = x.Predecessor;
            var xSucc = x.Successor;
            Assert.IsTrue(PostalCode.AreAdjacent(x, xPred));
            Assert.IsTrue(PostalCode.AreAdjacent(xPred, x));
            Assert.IsTrue(PostalCode.AreAdjacent(x, xSucc));
            Assert.IsTrue(PostalCode.AreAdjacent(xSucc, x));
            Assert.IsFalse(PostalCode.AreAdjacent(xPred, xSucc));
        }             

        [TestCase("12234")]
        [TestCase("52678")]
        public void CreateThroughFactoryIsSuccessful(string code)
        {
            var country = CountryFactory.Instance.CreateCountry("GT");
            var x = PostalCodeFactory.Instance.CreatePostalCode(country, code);
            
            Assert.IsTrue(x.GetType() == typeof(GTPostalCode));
        }             
    }
}
