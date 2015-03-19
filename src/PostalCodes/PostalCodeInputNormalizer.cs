using System;
using System.Text.RegularExpressions;

namespace PostalCodes
{
    /// <summary>
    /// Class PostalCodeInputNormalizer.
    /// </summary>
    public class PostalCodeInputNormalizer : IPostalCodeInputNormalizer
    {
        /// <summary>
        /// The great britain
        /// </summary>
        private static readonly Regex GreatBritain = new Regex("^[A-Z]{1,2}[0-9R][0-9A-Z]?[0-9]$", RegexOptions.Compiled);
        /// <summary>
        /// The great britain seven
        /// </summary>
        private static readonly Regex GreatBritainSeven = new Regex("^[A-Z]{1,2}[0-9R][0-9A-Z]?[0-9][ABD-HJLNP-UW-Z]{2}$", RegexOptions.Compiled);
        /// <summary>
        /// The portugal without extension
        /// </summary>
        private static readonly Regex PortugalWithoutExtension = new Regex("^[0-9]{1,4}$", RegexOptions.Compiled);
        /// <summary>
        /// The portugal
        /// </summary>
        private static readonly Regex Portugal = new Regex("^[0-9]{7}$", RegexOptions.Compiled);
        /// <summary>
        /// The russia without extension
        /// </summary>
        private static readonly Regex RussiaWithoutExtension = new Regex("^[0-9]{1,3}$", RegexOptions.Compiled);
        /// <summary>
        /// The russia
        /// </summary>
        private static readonly Regex Russia = new Regex("^[0-9]{6}$", RegexOptions.Compiled);
        /// <summary>
        /// The canada
        /// </summary>
        private static readonly Regex Canada = new Regex("^[A-Z][0-9][A-Z][0-9][A-Z][0-9]$", RegexOptions.Compiled);
        /// <summary>
        /// The barbados
        /// </summary>
        private static readonly Regex Barbados = new Regex("^(BB)?[0-9]{5}$", RegexOptions.Compiled);
        /// <summary>
        /// The malta
        /// </summary>
        private static readonly Regex Malta = new Regex("^[A-Z]{3}[0-9]{4}$", RegexOptions.Compiled);

        /// <summary>
        /// Normalizes postal codes for a given country for use in later comparisons; whether the postal code
        /// should be normalized to be the start or end of a range can be specified.
        /// </summary>
        /// <param name="country">Normalizes postal codes based on country</param>
        /// <param name="postalCode">Postal code to be normalized</param>
        /// <param name="start">Specifies whether postal code is start or end range</param>
        /// <returns>Normalized postal code depending on country</returns>
        public string Normalize(Country country, string postalCode, bool start)
        {
            postalCode = postalCode.Replace(" ", "").Replace("-", "").ToUpperInvariant();
            switch (country.Code)
            {
                case "AT":
                case "AU":
                case "BG":
                case "CH":
                case "DK":
                case "HU":
                case "NL":
                case "NO":
                case "SI":
                case "NZ":
                case "BE":
                case "CY":
                    return NormalizeDigits(country.Code, postalCode, 4);
                case "DE":
                case "CZ":
                case "EE":
                case "ES":
                case "FI":
                case "FR":
                case "GR":
                case "IT":
                case "PL":
                case "SE":
                case "SK":
                case "TR":
                case "US":
                case "PR":
                case "VI":
                case "AS":
                case "GU":
                case "MP":
                case "PW":
                case "FM":
                case "MH":
                case "MY":
                case "HR":
                case "MX":
                    return NormalizeDigits(country.Code, postalCode, 5);
                case "IN":
                case "SG":
                    return NormalizeDigits(country.Code, postalCode, 6);
                case "JP":
                    return NormalizeDigits(country.Code, postalCode, 7);
                case "PT":
                    return NormalizePortugal(country.Code, postalCode, start);
                case "RU":
                    return NormalizeRussia(country.Code, postalCode, start);
                case "GB":
                    return NormalizeGreatBritain(country.Code, postalCode);
                case "CA":
                    return NormalizeCanada(country.Code, postalCode);
                case "BB":
                    return NormalizeBarbados(country.Code, postalCode);
                case "MT":
                    return NormalizeMalta(country.Code, postalCode);
                default:
                    return postalCode;
            }
        }

        /// <summary>
        /// Normalizes the portugal.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="start">if set to <c>true</c> [start].</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        private static string NormalizePortugal(string country, string postalCode, bool start)
        {
            // First see if the we need to add 000/999 to the range.
            if (PortugalWithoutExtension.Match(postalCode).Success)
            {
                postalCode = postalCode.PadLeft(4, '0');

                postalCode += (start ? "000" : "999");
            }
            else
            {
                postalCode = postalCode.PadLeft(7, '0');
            }
            if (!Portugal.Match(postalCode).Success && !PortugalWithoutExtension.Match(postalCode).Success)
            {
                throw new InvalidOperationException(string.Format("Postal code {0} contains an invalid character for country {1}", postalCode, country));
            }
            return postalCode;
        }

        /// <summary>
        /// Normalizes the russia.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="start">if set to <c>true</c> [start].</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        private static string NormalizeRussia(string country, string postalCode, bool start)
        {
            // First see if we need to add 000/999 to the range.
            if (RussiaWithoutExtension.Match(postalCode).Success)
            {
                postalCode = postalCode.PadLeft(3, '0');

                postalCode += (start ? "000" : "999");
            }
            else
            {
                postalCode = postalCode.PadLeft(6, '0');
            }
            if (!Russia.Match(postalCode).Success && !RussiaWithoutExtension.Match(postalCode).Success)
            {
                throw new InvalidOperationException(string.Format("Postal code {0} contains an invalid character for country {1}", postalCode, country));
            }
            return postalCode;
        }

        /// <summary>
        /// Normalizes the great britain.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        private static string NormalizeGreatBritain(string country, string postalCode)
        {
            if (!GreatBritain.Match(postalCode).Success)
            {
                throw new InvalidOperationException(string.Format("Postal code {0} contains an invalid character for country {1}", postalCode, country));
            }
            return postalCode;
        }

        /// <summary>
        /// Normalizes the canada.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        private static string NormalizeCanada(string country, string postalCode)
        {
            // Check if postal code follows alphabet/number/alphabet/number/alphabet/number format.
            if (!Canada.Match(postalCode).Success)
            {
                throw new InvalidOperationException(string.Format("Postal code {0} contains an invalid character for country {1}", postalCode, country));
            }
            // Insert a space to match Address.FormatPostalCode.
            return postalCode;
        }

        /// <summary>
        /// Normalizes the barbados.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        private static string NormalizeBarbados(string country, string postalCode)
        {
            if (!Barbados.Match(postalCode).Success)
            {
                throw new InvalidOperationException(string.Format("Postal code {0} contains an invalid character for country {1}", postalCode, country));
            }
            return postalCode.StartsWith("BB") ? postalCode.Substring(2) : postalCode;
        }

        /// <summary>
        /// Normalizes the malta.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        private static string NormalizeMalta(string country, string postalCode)
        {
            if (!Malta.Match(postalCode).Success)
            {
                throw new InvalidOperationException(string.Format("Postal code {0} contains an invalid character for country {1}", postalCode, country));
            }
            return postalCode;
        }

        /// <summary>
        /// Normalizes the digits.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="digits">The digits.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// </exception>
        private static string NormalizeDigits(string country, string postalCode, int digits)
        {
            var intPostalCode = -1;
            if (!int.TryParse(postalCode, out intPostalCode))
            {
                throw new InvalidOperationException(string.Format("Postal code {0} contains an invalid character for country {1}", postalCode, country));
            }
            var maxPostalCodeExpected = (int)(Math.Pow(10, digits) - 1);
            if (intPostalCode > maxPostalCodeExpected)
            {
                throw new InvalidOperationException(string.Format("Postal code {0} contains an invalid character for country {1}", postalCode, country));
            }

            return postalCode.Trim().PadLeft(digits, '0');
        }

        /// <summary>
        /// Normalize a postal code to be a consistent format, so that we can check it against beginning/ending values for a particular zone.
        /// </summary>
        /// <param name="country">The country code of the postal code to normalize.</param>
        /// <param name="postalCode">The postal code to normalize.</param>
        /// <returns>A normalized version of the postal code suitable for comparison against start/end ranges (from Normalize).</returns>
        public static string NormalizePostalCodeToMatchZones(string country, string postalCode)
        {
            switch (country)
            {
                case "KR":
                    // Korea is ###-###
                    return postalCode;
                case "JP":
                    // Japan is ###-####
                    var normalizedJapan = postalCode.Replace(" ","").Replace("-","");
                    return normalizedJapan.Length < 7 ? string.Format("{0:0000000}", int.Parse(normalizedJapan)) : normalizedJapan.Substring(0, 7);
                case "PL":
                    // Poland is ##-###                    
                    var normalizedPoland = postalCode.Replace(" ", "").Replace("-", "");
                    return normalizedPoland.Length < 5 ? string.Format("{0:00000}", int.Parse(normalizedPoland)) : normalizedPoland.Substring(0, 5);                                  
                case "CA":
                    // Canada is A#A 9#9
                    var normalizedCanada = postalCode.Replace(" ", "");
                    return normalizedCanada.Length < 6 ? normalizedCanada : normalizedCanada.Substring(0, 6);
                case "SE":
                    // Sweden is ### ##
                    var normalizedSweden = postalCode.Replace(" ", "");
                    return normalizedSweden.Length < 5 ? string.Format("{0:00000}", int.Parse(normalizedSweden)) : normalizedSweden.Substring(0, 5);
                case "NL":
                    // Netherlands is #### AA
                    var spaceIndex = postalCode.IndexOf(' ');
                    postalCode = (spaceIndex == -1) ? postalCode : postalCode.Substring(0, spaceIndex);
                    return postalCode.Length < 4 ? string.Format("{0:0000}", int.Parse(postalCode)) : postalCode.Substring(0, 4);
                case "US":
                    // US is #####-#### 
                    var dashIndex = postalCode.IndexOf('-');
                    postalCode = (dashIndex == -1) ? postalCode : postalCode.Substring(0, dashIndex);
                    postalCode = postalCode.Replace(" ", "");
                    return postalCode.Length < 5 ? string.Format("{0:00000}", int.Parse(postalCode)) : postalCode.Substring(0, 5);
                case "GB":
                    // The 6 GB formats
                    // ----------------
                    // A9   9ZZ
                    // A9A  9ZZ
                    // A99  9ZZ
                    // AA9  9ZZ
                    // AA9A 9ZZ
                    // AA99 9ZZ
                    postalCode = Regex.Replace(postalCode, "[^a-z^A-Z0-9]", "").ToUpperInvariant();
                    return GreatBritainSeven.Match(postalCode).Success ? postalCode.Substring(0, postalCode.Length - 2) : postalCode;
                case "BB":
                    // Barbados is commonly (BB)?#####
                    postalCode = postalCode.Replace(" ", "").Replace("-", "");
                    postalCode = Regex.Replace(postalCode, "^(BB|bb|bB|Bb)", "");
                    int postalCodeAsInt;
                    var isInt = int.TryParse(postalCode, out postalCodeAsInt);
                    if (postalCode.Length < 5 && isInt)
                    {
                        return string.Format("{0:00000}", int.Parse(postalCode));
                    }
                    return postalCode.Length < 5 ? postalCode : postalCode.Substring(0, 5);
                case "MT":
                    // Malta is commonly AAA ####
                    return postalCode.Replace(" ", "").Replace("-", "").ToUpperInvariant();
                default:
                    return postalCode.Replace(" ", "").Replace("-", "");
            }
        }

        // <returns> false if the GB postalCodeA is not the same format as postalCodeB
        /// <summary>
        /// Checks if gb postal code formats match.
        /// </summary>
        /// <param name="quotePostalCode">The quote postal code.</param>
        /// <param name="startPostalCodeRange">The start postal code range.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CheckIfGBPostalCodeFormatsMatch(string quotePostalCode, string startPostalCodeRange)
        {
            var quotePostalCodeLength = quotePostalCode.Length;

            if (!GreatBritain.Match(quotePostalCode).Success || !GreatBritain.Match(startPostalCodeRange).Success)
            {
                return false;
            }

            if (quotePostalCodeLength != startPostalCodeRange.Length)
            {
                return false;
            }

            // The "In Code" is allways the same, so we don't need to compare it, only the "Out Code"
            var outCodeLength = quotePostalCodeLength - 1;

            // The first digit in all formats is a char so it can be skipped
            for (var i = 1; i < outCodeLength; ++i)
            {
                if (Char.IsDigit(quotePostalCode, i) != Char.IsDigit(startPostalCodeRange, i))
                {
                    return false;
                }
            }

            return true;
        }
    }
}