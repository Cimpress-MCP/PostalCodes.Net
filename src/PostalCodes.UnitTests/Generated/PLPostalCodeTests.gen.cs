using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class PLPostalCodeTests
    {

        [TestCase("44-100","44-099")]
        [TestCase("00-001","00000")]
        [TestCase("44100","44-099")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new PLPostalCode(postalCode);
            var codePredecessor = new PLPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("44-100","44-101")]
        [TestCase("00-001","00002")]
        [TestCase("44100","44-101")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new PLPostalCode(postalCode);
            var codeSuccessor = new PLPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());

        }
        
        [TestCase("00000")]
        [TestCase("00-000")]
        public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new PLPostalCode(postalCode)).Predecessor);
        }

        [TestCase("99999")]
        [TestCase("99-999")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new PLPostalCode(postalCode)).Successor);
        }

        [TestCase("44f00")]
        [TestCase("e4410")]
        [TestCase("44-100d")]
        [TestCase("c44-100")]
        [TestCase("b44100")]
        [TestCase("44100a")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new PLPostalCode(postalCode));
        }

        [TestCase("44100")]
        [TestCase("44-100")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new PLPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        [TestCase("44100")]
        [TestCase("44-100")]
        public void Equals_WithOtherObject_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new PLPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(new object());
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("44100")]
        [TestCase("44-100")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new PLPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(PLPostalCode));
        }

        [TestCase("44100")]
        [TestCase("44-100")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new PLPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(PLPostalCode));
        }

        [TestCase("44100")]
        [TestCase("44-100")]
        public void ExpandPostalCodeAsHighestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new PLPostalCode(code)).ExpandPostalCodeAsHighestInRange();
            Assert.IsTrue(x.GetType() == typeof(PLPostalCode));
        }

        [TestCase("44100")]
        [TestCase("44-100")]
        public void ExpandPostalCodeAsLowestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new PLPostalCode(code)).ExpandPostalCodeAsLowestInRange();
            Assert.IsTrue(x.GetType() == typeof(PLPostalCode));
        }

        [TestCase("44100")]
        [TestCase("44-100")]
        public void GetHashCode_WithEqualObject_EqualHashes(string code)
        {
            var x = new PLPostalCode(code);
            var y = new PLPostalCode(code);
            Assert.IsTrue(x.GetHashCode() == y.GetHashCode());
        }

        [TestCase("44100")]
        [TestCase("44-100")]
        public void AreAdjacent_WithAdjacentPostalCodes_ReturnsTrue(string code)
        {
            var x = new PLPostalCode(code);
            var xPred = x.Predecessor;
            var xSucc = x.Successor;
            Assert.IsTrue(PostalCode.AreAdjacent(x, xPred));
            Assert.IsTrue(PostalCode.AreAdjacent(xPred, x));
            Assert.IsTrue(PostalCode.AreAdjacent(x, xSucc));
            Assert.IsTrue(PostalCode.AreAdjacent(xSucc, x));
            Assert.IsFalse(PostalCode.AreAdjacent(xPred, xSucc));
        }             

        [TestCase("44100")]
        [TestCase("44-100")]
        public void CreateThroughFactoryIsSuccessful(string code)
        {
            var country = CountryFactory.Instance.CreateCountry("PL");
            var x = PostalCodeFactory.Instance.CreatePostalCode(country, code);
            
            Assert.IsTrue(x.GetType() == typeof(PLPostalCode));
        }             

    }
}
