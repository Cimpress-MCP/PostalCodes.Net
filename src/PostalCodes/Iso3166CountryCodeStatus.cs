using System;

namespace PostalCodes
{
	/// <summary>
	/// Statues taken from http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2
	/// </summary>
	internal enum Iso3166CountryCodeStatus
	{
		OfficiallyAssigned,
		UserAssigned,
		ExceptionallyReserved,
		TransitionallyReserved,
		IndeterminateReserved,
		NotUsed,
		Unassigned
	}
}
