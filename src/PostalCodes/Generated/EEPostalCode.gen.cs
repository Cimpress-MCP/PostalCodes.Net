using System;
using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
	internal class EEPostalCode : AlphaNumericPostalCode
    {
		public EEPostalCode(string postalCode) : this(postalCode, true) {}

		public EEPostalCode(string postalCode, bool allowConvertToShort) : base(_formats, postalCode, allowConvertToShort) 
		{
			_countryName = "EE";
		}

		private EEPostalCode(PostalCode other) : base(_formats, other.ToString()) {}

        protected override PostalCode PredecessorImpl
        {
			get 
			{ 
				var b = GenerateSuccesorOrPredecessor(GetInternalValue(), false);
				if (b == null) 
				{
					return null;
				}
				return new EEPostalCode (b, _allowConvertToShort);
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
				return new EEPostalCode (b, _allowConvertToShort);
			}
        }

		public override bool Equals (object obj)
		{
			var other = obj as EEPostalCode;
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
					return new EEPostalCode (ToString () + _currentFormat.ShortExpansionAsLowestInRange, false);
				} else {
					throw new ArgumentException ("Requested short postal code expansion but no expansion provided (lowest)");
				}
			}

			return new EEPostalCode(ToString());
		}

		public override PostalCode ExpandPostalCodeAsHighestInRange ()
		{
			if (_currentFormatType == FormatType.Short) {
				if (_currentFormat.ShortExpansionAsHighestInRange != null) {
					return new EEPostalCode (ToString () + _currentFormat.ShortExpansionAsHighestInRange, false);
				} else {
					throw new ArgumentException ("Requested short postal code expansion but no expansion provided (highest)");
				}
			}

			return new EEPostalCode(ToString());
		}

		private static PostalCodeFormat[] _formats = new PostalCodeFormat[] {
			new PostalCodeFormat {
				Name = "5-Digits - 99999",
				RegexDefault = new Regex("^[0-9]{5}$", RegexOptions.Compiled),
				OutputDefault = "xxxxx",
				AutoConvertToShort = false,
				ShortExpansionAsLowestInRange = "0",
				ShortExpansionAsHighestInRange = "9",
				LeftPaddingCharacter = "0",
			}
		};
	}
}
