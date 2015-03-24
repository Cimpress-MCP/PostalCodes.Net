using NUnit.Framework;
namespace PostalCodes.UnitTests.CountrySpecificPostalCodes
{
    [TestFixture]
    internal class PTPostalCodeManualTests
    {
        [Test]
        [TestCase("2660", "2659999")]
        [TestCase("2660023", "2660022")]
        public void Predecessor_ValidInputWithStart_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new PTPostalCode (postalCode);
            var expansion = code.ExpandPostalCodeAsLowestInRange ();
            Assert.AreEqual(postalCodePredecessor, expansion.Predecessor.ToString());
        }

        [Test]
        [TestCase("2660", "2660998")]
        [TestCase("2660023", "2660022")]
        public void Predecessor_ValidInputWithEnd_ReturnsCorrectPostalCode(string postalCode, string postalCodePredecessor)
        {
            var code = new PTPostalCode (postalCode);
            var expansion = code.ExpandPostalCodeAsHighestInRange ();
            Assert.AreEqual(postalCodePredecessor, expansion.Predecessor.ToString());
        }

        [Test]
        [TestCase("2660", "2660001")]
        [TestCase("2660023", "2660024")]
        public void Successor_ValidInputWithStart_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new PTPostalCode (postalCode);
            var expansion = code.ExpandPostalCodeAsLowestInRange ();
            Assert.AreEqual(postalCodeSuccessor, expansion.Successor.ToString());
        }

        [Test]
        [TestCase("2660", "2661000")]
        [TestCase("2660023", "2660024")]
        public void Successor_ValidInputWithEnd_ReturnsCorrectPostalCode(string postalCode, string postalCodeSuccessor)
        {
            var code = new PTPostalCode (postalCode);
            var expansion = code.ExpandPostalCodeAsHighestInRange ();
            Assert.AreEqual(postalCodeSuccessor, expansion.Successor.ToString());
        }

        [Test]
        [TestCase("0000")]
        [TestCase("0000000")]
        public void Predecessor_FirstInRangeWithStart_ReturnsNull(string postalCode)
        {
            var code = new PTPostalCode (postalCode);
            var expansion = code.ExpandPostalCodeAsLowestInRange ();
            Assert.IsNull(expansion.Predecessor);
        }

        [Test]
        [TestCase("0000000")]
        public void Predecessor_FirstInRangeWithEnd_ReturnsNull(string postalCode)
        {
            var code = new PTPostalCode (postalCode);
            var expansion = code.ExpandPostalCodeAsHighestInRange ();
            Assert.IsNull(expansion.Predecessor);
        }

        [Test]
        [TestCase("9999999")]
        public void Successor_LastInRangeWithStart_ReturnsNull(string postalCode)
        {
            var code = new PTPostalCode (postalCode);
            var expansion = code.ExpandPostalCodeAsLowestInRange ();
            Assert.IsNull(expansion.Successor);
        }

        [Test]
        [TestCase("9999")]
        [TestCase("9999999")]
        public void Successor_LastInRangeWithEnd_ReturnsNull(string postalCode)
        {
            var code = new PTPostalCode (postalCode);
            var expansion = code.ExpandPostalCodeAsHighestInRange ();
            Assert.IsNull(expansion.Successor);
        }

        [Test]
        public void Predecessor_ValidInput_ReturnsPortugalPostalCodeObject()
        {
            var code = new PTPostalCode ("1234");
            var x = code.Predecessor;
            Assert.IsTrue(x.GetType() == typeof(PTPostalCode));
        }

        [Test]
        public void Successor_ValidInput_ReturnsPortugalPostalCodeObject()
        {
            var x = (new PTPostalCode("1241123")).Successor;
            Assert.IsTrue(x.GetType() == typeof(PTPostalCode));
        }
    }
}
