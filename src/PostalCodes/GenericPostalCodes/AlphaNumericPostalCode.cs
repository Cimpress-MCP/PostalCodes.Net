namespace PostalCodes.GenericPostalCodes
{
    internal abstract class AlphaNumericPostalCode : PostalCode
    {
        internal AlphaNumericPostalCode(PostalCodeFormat[] formats, string postalCode) : this(formats, postalCode, true) {}

        internal AlphaNumericPostalCode(PostalCodeFormat[] formats, string postalCode, bool allowConvertToShort) : base(formats, postalCode, allowConvertToShort) {}


        /// <summary>
        /// Gets the internal value.
        /// </summary>
        /// <returns>The internal value.</returns>
        protected string GetInternalValue() {
            return PostalCodeString;
        }

        /// <summary>
        /// Generates the succesor or predecessor.
        /// </summary>
        /// <returns>The succesor or predecessor.</returns>
        /// <param name="postalCode">Postal code.</param>
        /// <param name="getSuccessor">If set to <c>true</c> get successor.</param>
        protected string GenerateSuccesorOrPredecessor(string postalCode, bool getSuccessor)
        {
            var nextTriggerNumber = getSuccessor ? '9' : '0';
            var nextTriggerLetter = getSuccessor ? 'Z' : 'A';

            var radix = postalCode.Length - 1;
            var suffix = "";
            while (radix >= 0 && (postalCode[radix] == nextTriggerNumber || postalCode[radix] == nextTriggerLetter))
            {
                if (postalCode[radix] == nextTriggerLetter)
                {
                    suffix = (getSuccessor ? 'A' : 'Z') + suffix;
                }
                else
                {
                    suffix = (getSuccessor ? '0' : '9') + suffix;
                }
                --radix;
            }

            if (radix < 0)
            {
                return null;
            }

            var newChar = (char) (postalCode[radix] + (getSuccessor ? 1 : -1));
            var nextPostalCode = postalCode.Substring(0, radix) + newChar + suffix;
            
            return nextPostalCode;
        }
    }
}
