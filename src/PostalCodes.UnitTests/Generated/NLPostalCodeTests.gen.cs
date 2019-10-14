using System;
using NUnit.Framework;

namespace PostalCodes.UnitTests.Generated
{
    [TestFixture]
    internal class NLPostalCodeTests
    {

        [TestCase("9999","9998")]
        [TestCase("1000","1999")]
        [TestCase("9999ZZ","9999ZY")]
        [TestCase("1000AA","1999ZZ")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new NLPostalCode(postalCode);
            var codePredecessor = new NLPostalCode(postalCodePredecessor);
            Assert.AreEqual(codePredecessor, code.Predecessor);
            Assert.AreEqual(codePredecessor.ToString(), code.Predecessor.ToString());
            Assert.AreEqual(codePredecessor.ToHumanReadableString(), code.Predecessor.ToHumanReadableString());
        }

        [TestCase("1999","2000")]
        [TestCase("3456","3457")]
        [TestCase("1999ZZ","2000AA")]
        [TestCase("3456JT","3456JU")]
        public void Successor_ValidInput_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new NLPostalCode(postalCode);
            var codeSuccessor = new NLPostalCode(postalCodeSuccessor);
            Assert.AreEqual(codeSuccessor, code.Successor);
            Assert.AreEqual(codeSuccessor.ToString(), code.Successor.ToString());
            Assert.AreEqual(codeSuccessor.ToHumanReadableString(), code.Successor.ToHumanReadableString());
        }
        
        //[TestCase("1000AA")]
        //public void Predecessor_FirstInRange_ReturnsNull(string postalCode)
        //{
        //    Assert.IsNull((new NLPostalCode(postalCode)).Predecessor);
        //}

        [TestCase("9999ZZ")]
        public void Successor_LastInRange_ReturnsNull(string postalCode)
        {
            Assert.IsNull((new NLPostalCode(postalCode)).Successor);
        }

        [TestCase("12j4h")]
        [TestCase("k3j51l")]
        public void InvalidCode_ThrowsArgumentException(string postalCode)
        {
            Assert.Throws<ArgumentException>(() => new NLPostalCode(postalCode));
        }

        [TestCase("1234", "1236")]
        [TestCase("1234AA", "1236AA")]
        [TestCase("1235", "1237AA")]
        [TestCase("1235ZY", "1235ZZ")]
        [TestCase("1234ZZ", "1235")]
        public void CompareTo_ReturnsExpectedSign(string postalCodeBefore, string postalCodeAfter)
        {
            var b = new NLPostalCode(postalCodeBefore);
            var a = new NLPostalCode(postalCodeAfter);
            Assert.AreEqual(Math.Sign(-1), Math.Sign(b.CompareTo(a)));
            Assert.AreEqual(Math.Sign( 1), Math.Sign(a.CompareTo(b)));
        }
        [TestCase("1235DF")]
        [TestCase("5983DH")]
        public void Equals_WithNull_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new NLPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(null);
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        [TestCase("1235DF")]
        [TestCase("5983DH")]
        public void Equals_WithOtherObject_DoesntThrowAndReturnsFalse(string code)
        {
            var x = (new NLPostalCode(code)).Predecessor;
            bool result = true;
            TestDelegate equals = () => result = x.Equals(new object());
            Assert.DoesNotThrow(equals);
            Assert.IsFalse(result);
        }
        
        [TestCase("1235DF")]
        [TestCase("5983DH")]
        public void Predecessor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new NLPostalCode(code)).Predecessor;
            Assert.IsTrue(x.GetType() == typeof(NLPostalCode));
        }

        [TestCase("1235DF")]
        [TestCase("5983DH")]
        public void Successor_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new NLPostalCode(code)).Successor;
            Assert.IsTrue(x.GetType() == typeof(NLPostalCode));
        }

        [TestCase("1235DF")]
        [TestCase("5983DH")]
        public void ExpandPostalCodeAsHighestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new NLPostalCode(code)).ExpandPostalCodeAsHighestInRange();
            Assert.IsTrue(x.GetType() == typeof(NLPostalCode));
        }

        [TestCase("1235DF")]
        [TestCase("5983DH")]
        public void ExpandPostalCodeAsLowestInRange_ValidInput_ReturnsCorrectPostalCodeObject(string code)
        {
            var x = (new NLPostalCode(code)).ExpandPostalCodeAsLowestInRange();
            Assert.IsTrue(x.GetType() == typeof(NLPostalCode));
        }

        [TestCase("1235DF")]
        [TestCase("5983DH")]
        public void GetHashCode_WithEqualObject_EqualHashes(string code)
        {
            var x = new NLPostalCode(code);
            var y = new NLPostalCode(code);
            Assert.IsTrue(x.GetHashCode() == y.GetHashCode());
        }

        [TestCase("1235DF")]
        [TestCase("5983DH")]
        public void AreAdjacent_WithAdjacentPostalCodes_ReturnsTrue(string code)
        {
            var x = new NLPostalCode(code);
            var xPred = x.Predecessor;
            var xSucc = x.Successor;
            Assert.IsTrue(PostalCode.AreAdjacent(x, xPred));
            Assert.IsTrue(PostalCode.AreAdjacent(xPred, x));
            Assert.IsTrue(PostalCode.AreAdjacent(x, xSucc));
            Assert.IsTrue(PostalCode.AreAdjacent(xSucc, x));
            Assert.IsFalse(PostalCode.AreAdjacent(xPred, xSucc));
        }             

        [TestCase("1235DF")]
        [TestCase("5983DH")]
        public void CreateThroughFactoryIsSuccessful(string code)
        {
            var country = CountryFactory.Instance.CreateCountry("NL");
            var x = PostalCodeFactory.Instance.CreatePostalCode(country, code);
            
            Assert.IsTrue(x.GetType() == typeof(NLPostalCode));
        }             
    }
}
