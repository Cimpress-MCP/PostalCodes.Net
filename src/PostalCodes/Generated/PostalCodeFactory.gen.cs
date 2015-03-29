using System;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    /// <summary>
    /// Factory implementation for PostalCode
    /// </summary>
    public class PostalCodeFactory
    {
        private static readonly Lazy<PostalCodeFactory> LazyFactory = new Lazy<PostalCodeFactory>(() => new PostalCodeFactory());

        internal PostalCodeFactory()
        {
        }

        /// <summary>
        /// Gets an instance of PostalCodeFactory
        /// </summary>
        public static PostalCodeFactory Instance
        {
            get { return LazyFactory.Value; }
        }

        /// <summary>
        /// Creates a PostalCode object using the provided Country and postal code
        /// </summary>
        /// <param name="country">Country associated with the provided postal code</param>
        /// <param name="postalCode">Postal code value</param>
        /// <returns>A PostalCode representing the provided values</returns>
        public PostalCode CreatePostalCode(Country country, string postalCode)
        {
            if (postalCode == null)
            {
                return null;
            }

            switch(country.Code) {
				case "AT" :
   					return new ATPostalCode(postalCode);
				case "AU" :
   					return new AUPostalCode(postalCode);
				case "BG" :
   					return new BGPostalCode(postalCode);
				case "CH" :
   					return new CHPostalCode(postalCode);
				case "DK" :
   					return new DKPostalCode(postalCode);
				case "HU" :
   					return new HUPostalCode(postalCode);
				case "NO" :
   					return new NOPostalCode(postalCode);
				case "SI" :
   					return new SIPostalCode(postalCode);
				case "NZ" :
   					return new NZPostalCode(postalCode);
				case "BE" :
   					return new BEPostalCode(postalCode);
				case "CY" :
   					return new CYPostalCode(postalCode);
				case "DE" :
   					return new DEPostalCode(postalCode);
				case "CZ" :
   					return new CZPostalCode(postalCode);
				case "EE" :
   					return new EEPostalCode(postalCode);
				case "ES" :
   					return new ESPostalCode(postalCode);
				case "FI" :
   					return new FIPostalCode(postalCode);
				case "FR" :
   					return new FRPostalCode(postalCode);
				case "GR" :
   					return new GRPostalCode(postalCode);
				case "IT" :
   					return new ITPostalCode(postalCode);
				case "SE" :
   					return new SEPostalCode(postalCode);
				case "SK" :
   					return new SKPostalCode(postalCode);
				case "TR" :
   					return new TRPostalCode(postalCode);
				case "US" :
   					return new USPostalCode(postalCode);
				case "PR" :
   					return new PRPostalCode(postalCode);
				case "VI" :
   					return new VIPostalCode(postalCode);
				case "AS" :
   					return new ASPostalCode(postalCode);
				case "GU" :
   					return new GUPostalCode(postalCode);
				case "MP" :
   					return new MPPostalCode(postalCode);
				case "PW" :
   					return new PWPostalCode(postalCode);
				case "FM" :
   					return new FMPostalCode(postalCode);
				case "MH" :
   					return new MHPostalCode(postalCode);
				case "MY" :
   					return new MYPostalCode(postalCode);
				case "HR" :
   					return new HRPostalCode(postalCode);
				case "MX" :
   					return new MXPostalCode(postalCode);
				case "IN" :
   					return new INPostalCode(postalCode);
				case "SG" :
   					return new SGPostalCode(postalCode);
				case "JP" :
   					return new JPPostalCode(postalCode);
				case "BB" :
   					return new BBPostalCode(postalCode);
				case "CA" :
   					return new CAPostalCode(postalCode);
				case "GB" :
   					return new GBPostalCode(postalCode);
				case "MT" :
   					return new MTPostalCode(postalCode);
				case "NL" :
   					return new NLPostalCode(postalCode);
				case "PL" :
   					return new PLPostalCode(postalCode);
				case "PT" :
   					return new PTPostalCode(postalCode);
				case "RU" :
   					return new RUPostalCode(postalCode);
            }

            // Default behavior so far was to just return NumericPostalCode
            return new DefaultPostalCode(postalCode);
        }
    }
}


