using System;
using System.Text.RegularExpressions;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
	internal class RUPostalCode : AlphaNumericPostalCode
    {
		public RUPostalCode(string postalCode) : this(postalCode, true) {}

		public RUPostalCode(string postalCode, bool allowConvertToShort) : base(_formats, postalCode, allowConvertToShort) 
		{
			_countryName = "Russia";
		}

		private RUPostalCode(PostalCode other) : base(_formats, other.ToString()) {}

        protected override PostalCode PredecessorImpl
        {
			get 
			{ 
				var b = GenerateSuccesorOrPredecessor(GetInternalValue(), false);
				if (b == null) 
				{
					return null;
				}
				return new RUPostalCode (b, _allowConvertToShort);
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
				return new RUPostalCode (b, _allowConvertToShort);
			}
        }

		public override bool Equals (object obj)
		{
			var other = obj as RUPostalCode;
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
					return new RUPostalCode (ToString () + _currentFormat.ShortExpansionAsLowestInRange, false);
				} else {
					throw new ArgumentException ("Requested short postal code expansion but no expansion provided (lowest)");
				}
			}

			return new RUPostalCode(ToString());
		}

		public override PostalCode ExpandPostalCodeAsHighestInRange ()
		{
			if (_currentFormatType == FormatType.Short) {
				if (_currentFormat.ShortExpansionAsHighestInRange != null) {
					return new RUPostalCode (ToString () + _currentFormat.ShortExpansionAsHighestInRange, false);
				} else {
					throw new ArgumentException ("Requested short postal code expansion but no expansion provided (highest)");
				}
			}

			return new RUPostalCode(ToString());
		}

		private static PostalCodeFormat[] _formats = new PostalCodeFormat[] {
			new PostalCodeFormat {
				Name = "RU : 999999",
				RegexDefault = new Regex("^[0-9]{6}$", RegexOptions.Compiled),
				RegexShort = new Regex("^[0-9]{3}$", RegexOptions.Compiled),
				OutputDefault = "xxxxxx",
				OutputShort = "xxx",
				ShortExpansionAsLowestInRange = "000",
				ShortExpansionAsHighestInRange = "999",
				LeftPaddingCharacter = "0",
			}
		};
	}
}
