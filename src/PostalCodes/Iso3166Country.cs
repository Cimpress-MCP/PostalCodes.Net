using System;

namespace PostalCodes
{
	internal class Iso3166Country
	{
		public readonly string CountryName;
		public readonly string Alpha2Code;
		public readonly Iso3166CountryCodeStatus Status;
		public readonly string[] NewCountryCodes;

		public Iso3166Country (string alpha2Code, string name, Iso3166CountryCodeStatus status)
			: this (alpha2Code, name, status, new string[] { })
		{
		}

		public Iso3166Country (string alpha2Code, string name, Iso3166CountryCodeStatus status, string[] newCodes)
		{
			Alpha2Code = alpha2Code;
			CountryName = name;
			Status = status;
			NewCountryCodes = newCodes;
		}
	}


}

