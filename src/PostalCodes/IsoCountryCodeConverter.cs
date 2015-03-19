using System;
using System.Linq;

namespace PostalCodes
{
    /// <summary>
    /// Class IsoToVistaprintCountryCodeConverter.
    /// </summary>
    public class IsoCountryCodeConverter
    {
        /// <summary>
        /// Gets the vistaprint country code.
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public string GetIso3166p3Code(string countryCode)
        {
			var oldCode = Iso3166Countries.Countries.FirstOrDefault (a => a.NewCountryCodes.Contains (countryCode.ToUpperInvariant ()));
			if (oldCode == default(Iso3166Country)) 
			{
				oldCode = Iso3166Countries.Countries.FirstOrDefault (a => a.Alpha2Code == countryCode);
				if (oldCode == default(Iso3166Country)) 
				{
					throw new InvalidOperationException (string.Format ("The specified country code is not valid: {0}", countryCode));
				}
			}

			if (oldCode.Status == Iso3166CountryCodeStatus.NotUsed || oldCode.Status == Iso3166CountryCodeStatus.Unassigned) 
			{
				throw new InvalidOperationException (string.Format ("The specified country code is not valid: {0}", countryCode));
			}

			return oldCode.Alpha2Code;
        }
    }
}
