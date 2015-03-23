using System;
using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
	internal class SGPostalCode : AlphaNumericPostalCode
    {
		public SGPostalCode(string postalCode) : this(postalCode, true) {}

		public SGPostalCode(string postalCode, bool allowConvertToShort) : base(_formats, postalCode, allowConvertToShort) 
		{
			_countryName = "SG";
		}

		private SGPostalCode(PostalCode other) : base(_formats, other.ToString()) {}

        protected override PostalCode PredecessorImpl
        {
			get 
			{ 
				var b = GenerateSuccesorOrPredecessor(GetInternalValue(), false);
				if (b == null) 
				{
					return null;
				}
				return new SGPostalCode (b, _allowConvertToShort);
			}
        }

        protected override PostalCode SuccessorImpl
        {
			get 
			{ 
				var b = GenerateSuccesorOrPredecessor(GetInternalValue(), true);
				if (b == null) 
				{
					return null;
				}
				return new SGPostalCode (b, _allowConvertToShort);
			}
        }

		public override bool Equals (object obj)
		{
			var other = obj as SGPostalCode;
			if (other == null)
				return false;

			return PostalCodeString.Equals (other.PostalCodeString);
		}

		public override int GetHashCode ()
		{
			return PostalCodeString.GetHashCode ();
		}

		public override string ToHumanReadableString ()
		{
			var outputFormat = _currentFormat.OutputDefault;
			if (_currentFormatType == FormatType.Short) 
			{
				if (_currentFormat.OutputShort != null) 
				{
					outputFormat = _currentFormat.OutputShort;
				}
			}

			return ToHumanReadableString(outputFormat);
		}
		
		public override PostalCode ExpandPostalCodeAsLowestInRange ()
		{
			if (_currentFormatType == FormatType.Short) {
				if (_currentFormat.ShortExpansionAsLowestInRange != null) {
					return new SGPostalCode (ToString () + _currentFormat.ShortExpansionAsLowestInRange, false);
				} else {
					throw new ArgumentException ("Requested short postal code expansion but no expansion provided (lowest)");
				}
			}

			return new SGPostalCode(ToString());
		}

		public override PostalCode ExpandPostalCodeAsHighestInRange ()
		{
			if (_currentFormatType == FormatType.Short) {
				if (_currentFormat.ShortExpansionAsHighestInRange != null) {
					return new SGPostalCode (ToString () + _currentFormat.ShortExpansionAsHighestInRange, false);
				} else {
					throw new ArgumentException ("Requested short postal code expansion but no expansion provided (highest)");
				}
			}

			return new SGPostalCode(ToString());
		}

		private static PostalCodeFormat[] _formats = new PostalCodeFormat[] {
			new PostalCodeFormat {
				Name = "6-Digits - 999999",
				RegexDefault = new Regex("^[0-9]{6}$", RegexOptions.Compiled),
				OutputDefault = "xxxxxx",
				AutoConvertToShort = false,
				ShortExpansionAsLowestInRange = "0",
				ShortExpansionAsHighestInRange = "9",
				LeftPaddingCharacter = "0",
			}
		};
	}
}
