using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class CAPostalCodeTests
    {

        [TestCase("A9A9A9","A9A9A8")]
        [TestCase("B6C3A0","B6C2Z9")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new CAPostalCode(postalCode);
            var codePredecessor = new CAPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("A9A9A9","A9A9B0")]
        [TestCase("A1B2C3","A1B2C4")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new CAPostalCode(postalCode);
            var codeSuccessor = new CAPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());

        }
        
        [TestCase("A0A0A0")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new CAPostalCode(postalCode)).Predecessor);
        }

        [TestCase("Z9Z9Z9")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new CAPostalCode(postalCode)).Successor);
        }

        [TestCase("123AAA")]
        [TestCase("12A5AA")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new CAPostalCode(postalCode));
        }

        [TestCase("A4B5X5")]
        [TestCase("A4B5A5")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new CAPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        [TestCase("A4B5X5")]
        [TestCase("A4B5A5")]
        public void Equals_WithOtherObject_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new CAPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(new object());
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("A4B5X5")]
        [TestCase("A4B5A5")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new CAPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(CAPostalCode));
        }

        [TestCase("A4B5X5")]
        [TestCase("A4B5A5")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new CAPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(CAPostalCode));
        }

        [TestCase("A4B5X5")]
        [TestCase("A4B5A5")]
        public void ExpandPostalCodeAsHighestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new CAPostalCode(code)).ExpandPostalCodeAsHighestInRange();
            Assert.IsTrue(x.GetType() == typeof(CAPostalCode));
        }

        [TestCase("A4B5X5")]
        [TestCase("A4B5A5")]
        public void ExpandPostalCodeAsLowestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new CAPostalCode(code)).ExpandPostalCodeAsLowestInRange();
            Assert.IsTrue(x.GetType() == typeof(CAPostalCode));
        }

        [TestCase("A4B5X5")]
        [TestCase("A4B5A5")]
        public void GetHashCode_WithEqualObject_EqualHashes(string code)
        {
            var x = new CAPostalCode(code);
            var y = new CAPostalCode(code);
            Assert.IsTrue(x.GetHashCode() == y.GetHashCode());
        }

        [TestCase("A4B5X5")]
        [TestCase("A4B5A5")]
        public void AreAdjacent_WithAdjacentPostalCodes_ReturnsTrue(string code)
        {
            var x = new CAPostalCode(code);
            var xPred = x.Predecessor;
            var xSucc = x.Successor;
            Assert.IsTrue(PostalCode.AreAdjacent(x, xPred));
            Assert.IsTrue(PostalCode.AreAdjacent(xPred, x));
            Assert.IsTrue(PostalCode.AreAdjacent(x, xSucc));
            Assert.IsTrue(PostalCode.AreAdjacent(xSucc, x));
            Assert.IsFalse(PostalCode.AreAdjacent(xPred, xSucc));
        }             

        [TestCase("A4B5X5")]
        [TestCase("A4B5A5")]
        public void CreateThroughFactoryIsSuccessful(string code)
        {
            var country = CountryFactory.Instance.CreateCountry("CA");
            var x = PostalCodeFactory.Instance.CreatePostalCode(country, code);
            
            Assert.IsTrue(x.GetType() == typeof(CAPostalCode));
        }             

    }
}
