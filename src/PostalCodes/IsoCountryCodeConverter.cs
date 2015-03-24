using System;
using System.Linq;

namespace PostalCodes
{
    /// <summary>
    /// ISO 3166 country code converter
    /// </summary>
    public class IsoCountryCodeConverter
    {
        /// <summary>
        /// Gets the iso3166-3 country code (if any).
        /// </summary>
        /// <param name="countryCode">Country code.</param>
        /// <returns>The iso3166p3 code.</returns>
        public string GetIso3166p3Code(string countryCode)
        {
            var oldCode = Iso3166Countries.Countries.FirstOrDefault (a => a.NewCountryCodes.Contains (countryCode));
            if (oldCode == default(Iso3166Country)) 
            {
                oldCode = Iso3166Countries.Countries.FirstOrDefault (a => a.Alpha2Code == countryCode);
            }

            if (oldCode == default(Iso3166Country)
                || oldCode.Status == Iso3166CountryCodeStatus.NotUsed
                || oldCode.Status == Iso3166CountryCodeStatus.Unassigned
                || oldCode.Status == Iso3166CountryCodeStatus.UserAssigned) 
            {
                throw new InvalidOperationException (string.Format ("The specified country code is not valid: {0}", countryCode));
            }

            return oldCode.Alpha2Code;
        }
    }
}
