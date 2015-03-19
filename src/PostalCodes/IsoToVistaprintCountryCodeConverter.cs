using System;

namespace PostalCodes
{
    public class IsoToVistaprintCountryCodeConverter
    {
        public string GetVistaprintCountryCode(string countryCode)
        {
            if (!CountryCodes.ValidCountryCodes.Contains(countryCode))
            {
                throw new InvalidOperationException(string.Format("The specified country code is not valid: {0}", countryCode));
            }

            return IsoToVistaprint(countryCode);
        }

        private string IsoToVistaprint(string countryCode)
        {
            switch (countryCode)
            {
                case "CW":
                case "SX":
                case "BQ":
                    return "AN";
                case "GB":
                    return "UK";
                default:
                    return countryCode;
            }
        }
    }
}
