using System;
using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
	internal class CHPostalCode : AlphaNumericPostalCode
    {
		public CHPostalCode(string postalCode) : this(postalCode, true) {}

		public CHPostalCode(string postalCode, bool allowConvertToShort) : base(_formats, postalCode, allowConvertToShort) 
		{
			_countryName = "CH";
		}

		private CHPostalCode(PostalCode other) : base(_formats, other.ToString()) {}

        protected override PostalCode PredecessorImpl
        {
			get 
			{ 
				var b = GenerateSuccesorOrPredecessor(GetInternalValue(), false);
				if (b == null) 
				{
					return null;
				}
				return new CHPostalCode (b, _allowConvertToShort);
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
				return new CHPostalCode (b, _allowConvertToShort);
			}
        }

		public override bool Equals (object obj)
		{
			var other = obj as CHPostalCode;
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
					return new CHPostalCode (ToString () + _currentFormat.ShortExpansionAsLowestInRange, false);
				} else {
					throw new ArgumentException ("Requested short postal code expansion but no expansion provided (lowest)");
				}
			}

			return new CHPostalCode(ToString());
		}

		public override PostalCode ExpandPostalCodeAsHighestInRange ()
		{
			if (_currentFormatType == FormatType.Short) {
				if (_currentFormat.ShortExpansionAsHighestInRange != null) {
					return new CHPostalCode (ToString () + _currentFormat.ShortExpansionAsHighestInRange, false);
				} else {
					throw new ArgumentException ("Requested short postal code expansion but no expansion provided (highest)");
				}
			}

			return new CHPostalCode(ToString());
		}

		private static PostalCodeFormat[] _formats = new PostalCodeFormat[] {
			new PostalCodeFormat {
				Name = "4-Digits - 9999",
				RegexDefault = new Regex("^[0-9]{4}$", RegexOptions.Compiled),
				OutputDefault = "xxxx",
				AutoConvertToShort = false,
				ShortExpansionAsLowestInRange = "0",
				ShortExpansionAsHighestInRange = "9",
				LeftPaddingCharacter = "0",
			}
		};
	}
}
