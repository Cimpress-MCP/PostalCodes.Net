using System;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
	internal partial class BBPostalCode : AlphaNumericPostalCode
	{
		protected override string Normalize (string code)
		{
			return code.StartsWith ("BB") ? base.Normalize (code.Substring (2)) : base.Normalize (code);
		}
	}
}

