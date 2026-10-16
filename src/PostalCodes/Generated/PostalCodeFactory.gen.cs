using System;
using PostalCodes.GenericPostalCodes;

namespace PostalCodes
{
    /// <summary>
    /// Factory implementation for PostalCode
    /// </summary>
    public class PostalCodeFactory : IPostalCodeFactory
    {
        private static readonly Lazy<PostalCodeFactory> LazyFactory = new Lazy<PostalCodeFactory>(() => new PostalCodeFactory());

        private readonly bool _useLegacyNetherlandsFormat;

        internal PostalCodeFactory()
        {
        }

        /// <summary>
        /// Creates a PostalCodeFactory with the provided options
        /// </summary>
        /// <param name="options">Options changing how postal codes are created</param>
        public PostalCodeFactory(PostalCodeFactoryOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _useLegacyNetherlandsFormat = options.UseLegacyNetherlandsFormat;
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

            return Create(country, PostalCode.ToUpperInvariantIfNeeded(postalCode), true);
        }

        /// <summary>
        /// Creates a PostalCode object using the provided Country and postal code, without throwing when the postal
        /// code matches no format of the country
        /// </summary>
        /// <param name="country">Country associated with the provided postal code</param>
        /// <param name="postalCode">Postal code value</param>
        /// <param name="result">The PostalCode representing the provided values, or null</param>
        /// <returns>True if the postal code is not null and matches a format of the country</returns>
        public bool TryCreatePostalCode(Country country, string postalCode, out PostalCode result)
        {
            result = null;
            if (postalCode == null)
            {
                return false;
            }

            var created = Create(country, PostalCode.ToUpperInvariantIfNeeded(postalCode), false);
            if (created.PostalCodeFormat == null)
            {
                return false;
            }

            result = created;
            return true;
        }

        private PostalCode Create(Country country, string postalCode, bool throwOnInvalid)
        {
            if (_useLegacyNetherlandsFormat && country.Code == "NL")
            {
                return new NLLegacyPostalCode(postalCode, true, throwOnInvalid);
            }

            switch(country.Code) {
                case "AF" :
                    return new AFPostalCode(postalCode, " -", true, throwOnInvalid);
                case "AL" :
                    return new ALPostalCode(postalCode, " -", true, throwOnInvalid);
                case "AM" :
                    return new AMPostalCode(postalCode, " -", true, throwOnInvalid);
                case "AR" :
                    return new ARPostalCode(postalCode, " -", true, throwOnInvalid);
                case "AS" :
                    return new ASPostalCode(postalCode, " -", true, throwOnInvalid);
                case "AT" :
                    return new ATPostalCode(postalCode, " -", true, throwOnInvalid);
                case "AU" :
                    return new AUPostalCode(postalCode, " -", true, throwOnInvalid);
                case "BA" :
                    return new BAPostalCode(postalCode, " -", true, throwOnInvalid);
                case "BB" :
                    return new BBPostalCode(postalCode, " -", true, throwOnInvalid);
                case "BD" :
                    return new BDPostalCode(postalCode, " -", true, throwOnInvalid);
                case "BE" :
                    return new BEPostalCode(postalCode, " -", true, throwOnInvalid);
                case "BG" :
                    return new BGPostalCode(postalCode, " -", true, throwOnInvalid);
                case "BO" :
                    return new BOPostalCode(postalCode, " -", true, throwOnInvalid);
                case "BT" :
                    return new BTPostalCode(postalCode, " -", true, throwOnInvalid);
                case "BY" :
                    return new BYPostalCode(postalCode, " -", true, throwOnInvalid);
                case "CA" :
                    return new CAPostalCode(postalCode, " -", true, throwOnInvalid);
                case "CC" :
                    return new CCPostalCode(postalCode, " -", true, throwOnInvalid);
                case "CH" :
                    return new CHPostalCode(postalCode, " -", true, throwOnInvalid);
                case "CN" :
                    return new CNPostalCode(postalCode, " -", true, throwOnInvalid);
                case "CO" :
                    return new COPostalCode(postalCode, " -", true, throwOnInvalid);
                case "CR" :
                    return new CRPostalCode(postalCode, " -", true, throwOnInvalid);
                case "CU" :
                    return new CUPostalCode(postalCode, " -", true, throwOnInvalid);
                case "CV" :
                    return new CVPostalCode(postalCode, " -", true, throwOnInvalid);
                case "CX" :
                    return new CXPostalCode(postalCode, " -", true, throwOnInvalid);
                case "CY" :
                    return new CYPostalCode(postalCode, " -", true, throwOnInvalid);
                case "CZ" :
                    return new CZPostalCode(postalCode, " -", true, throwOnInvalid);
                case "DE" :
                    return new DEPostalCode(postalCode, " -", true, throwOnInvalid);
                case "DK" :
                    return new DKPostalCode(postalCode, " -", true, throwOnInvalid);
                case "DO" :
                    return new DOPostalCode(postalCode, " -", true, throwOnInvalid);
                case "DZ" :
                    return new DZPostalCode(postalCode, " -", true, throwOnInvalid);
                case "EC" :
                    return new ECPostalCode(postalCode, " -", true, throwOnInvalid);
                case "EE" :
                    return new EEPostalCode(postalCode, " -", true, throwOnInvalid);
                case "EG" :
                    return new EGPostalCode(postalCode, " -", true, throwOnInvalid);
                case "ES" :
                    return new ESPostalCode(postalCode, " -", true, throwOnInvalid);
                case "ET" :
                    return new ETPostalCode(postalCode, " -", true, throwOnInvalid);
                case "FI" :
                    return new FIPostalCode(postalCode, " -", true, throwOnInvalid);
                case "FM" :
                    return new FMPostalCode(postalCode, " -", true, throwOnInvalid);
                case "FO" :
                    return new FOPostalCode(postalCode, " -", true, throwOnInvalid);
                case "FR" :
                    return new FRPostalCode(postalCode, " -", true, throwOnInvalid);
                case "GB" :
                    return new GBPostalCode(postalCode, " -", true, throwOnInvalid);
                case "GE" :
                    return new GEPostalCode(postalCode, " -", true, throwOnInvalid);
                case "GL" :
                    return new GLPostalCode(postalCode, " -", true, throwOnInvalid);
                case "GR" :
                    return new GRPostalCode(postalCode, " -", true, throwOnInvalid);
                case "GT" :
                    return new GTPostalCode(postalCode, " -", true, throwOnInvalid);
                case "GU" :
                    return new GUPostalCode(postalCode, " -", true, throwOnInvalid);
                case "GW" :
                    return new GWPostalCode(postalCode, " -", true, throwOnInvalid);
                case "HM" :
                    return new HMPostalCode(postalCode, " -", true, throwOnInvalid);
                case "HN" :
                    return new HNPostalCode(postalCode, " -", true, throwOnInvalid);
                case "HR" :
                    return new HRPostalCode(postalCode, " -", true, throwOnInvalid);
                case "HT" :
                    return new HTPostalCode(postalCode, " -", true, throwOnInvalid);
                case "HU" :
                    return new HUPostalCode(postalCode, " -", true, throwOnInvalid);
                case "ID" :
                    return new IDPostalCode(postalCode, " -", true, throwOnInvalid);
                case "IL" :
                    return new ILPostalCode(postalCode, " -", true, throwOnInvalid);
                case "IN" :
                    return new INPostalCode(postalCode, " -", true, throwOnInvalid);
                case "IQ" :
                    return new IQPostalCode(postalCode, " -", true, throwOnInvalid);
                case "IS" :
                    return new ISPostalCode(postalCode, " -", true, throwOnInvalid);
                case "IT" :
                    return new ITPostalCode(postalCode, " -", true, throwOnInvalid);
                case "JO" :
                    return new JOPostalCode(postalCode, " -", true, throwOnInvalid);
                case "JP" :
                    return new JPPostalCode(postalCode, " -", true, throwOnInvalid);
                case "KE" :
                    return new KEPostalCode(postalCode, " -", true, throwOnInvalid);
                case "KG" :
                    return new KGPostalCode(postalCode, " -", true, throwOnInvalid);
                case "KH" :
                    return new KHPostalCode(postalCode, " -", true, throwOnInvalid);
                case "KW" :
                    return new KWPostalCode(postalCode, " -", true, throwOnInvalid);
                case "KZ" :
                    return new KZPostalCode(postalCode, " -", true, throwOnInvalid);
                case "LA" :
                    return new LAPostalCode(postalCode, " -", true, throwOnInvalid);
                case "LI" :
                    return new LIPostalCode(postalCode, " -", true, throwOnInvalid);
                case "LK" :
                    return new LKPostalCode(postalCode, " -", true, throwOnInvalid);
                case "LR" :
                    return new LRPostalCode(postalCode, " -", true, throwOnInvalid);
                case "LS" :
                    return new LSPostalCode(postalCode, " -", true, throwOnInvalid);
                case "LU" :
                    return new LUPostalCode(postalCode, " -", true, throwOnInvalid);
                case "LY" :
                    return new LYPostalCode(postalCode, " -", true, throwOnInvalid);
                case "MA" :
                    return new MAPostalCode(postalCode, " -", true, throwOnInvalid);
                case "MC" :
                    return new MCPostalCode(postalCode, " -", true, throwOnInvalid);
                case "ME" :
                    return new MEPostalCode(postalCode, " -", true, throwOnInvalid);
                case "MG" :
                    return new MGPostalCode(postalCode, " -", true, throwOnInvalid);
                case "MH" :
                    return new MHPostalCode(postalCode, " -", true, throwOnInvalid);
                case "MK" :
                    return new MKPostalCode(postalCode, " -", true, throwOnInvalid);
                case "MM" :
                    return new MMPostalCode(postalCode, " -", true, throwOnInvalid);
                case "MN" :
                    return new MNPostalCode(postalCode, " -", true, throwOnInvalid);
                case "MP" :
                    return new MPPostalCode(postalCode, " -", true, throwOnInvalid);
                case "MQ" :
                    return new MQPostalCode(postalCode, " -", true, throwOnInvalid);
                case "MT" :
                    return new MTPostalCode(postalCode, " -", true, throwOnInvalid);
                case "MU" :
                    return new MUPostalCode(postalCode, " -", true, throwOnInvalid);
                case "MX" :
                    return new MXPostalCode(postalCode, " -", true, throwOnInvalid);
                case "MY" :
                    return new MYPostalCode(postalCode, " -", true, throwOnInvalid);
                case "MZ" :
                    return new MZPostalCode(postalCode, " -", true, throwOnInvalid);
                case "NA" :
                    return new NAPostalCode(postalCode, " -", true, throwOnInvalid);
                case "NE" :
                    return new NEPostalCode(postalCode, " -", true, throwOnInvalid);
                case "NF" :
                    return new NFPostalCode(postalCode, " -", true, throwOnInvalid);
                case "NG" :
                    return new NGPostalCode(postalCode, " -", true, throwOnInvalid);
                case "NI" :
                    return new NIPostalCode(postalCode, " -", true, throwOnInvalid);
                case "NL" :
                    return new NLPostalCode(postalCode, " -", true, throwOnInvalid);
                case "NO" :
                    return new NOPostalCode(postalCode, " -", true, throwOnInvalid);
                case "NP" :
                    return new NPPostalCode(postalCode, " -", true, throwOnInvalid);
                case "NZ" :
                    return new NZPostalCode(postalCode, " -", true, throwOnInvalid);
                case "OM" :
                    return new OMPostalCode(postalCode, " -", true, throwOnInvalid);
                case "PA" :
                    return new PAPostalCode(postalCode, " -", true, throwOnInvalid);
                case "PE" :
                    return new PEPostalCode(postalCode, " -", true, throwOnInvalid);
                case "PG" :
                    return new PGPostalCode(postalCode, " -", true, throwOnInvalid);
                case "PH" :
                    return new PHPostalCode(postalCode, " -", true, throwOnInvalid);
                case "PK" :
                    return new PKPostalCode(postalCode, " -", true, throwOnInvalid);
                case "PL" :
                    return new PLPostalCode(postalCode, " -", true, throwOnInvalid);
                case "PR" :
                    return new PRPostalCode(postalCode, " -", true, throwOnInvalid);
                case "PS" :
                    return new PSPostalCode(postalCode, " -", true, throwOnInvalid);
                case "PT" :
                    return new PTPostalCode(postalCode, " -", true, throwOnInvalid);
                case "PW" :
                    return new PWPostalCode(postalCode, " -", true, throwOnInvalid);
                case "PY" :
                    return new PYPostalCode(postalCode, " -", true, throwOnInvalid);
                case "RO" :
                    return new ROPostalCode(postalCode, " -", true, throwOnInvalid);
                case "RS" :
                    return new RSPostalCode(postalCode, " -", true, throwOnInvalid);
                case "RU" :
                    return new RUPostalCode(postalCode, " -", true, throwOnInvalid);
                case "SD" :
                    return new SDPostalCode(postalCode, " -", true, throwOnInvalid);
                case "SE" :
                    return new SEPostalCode(postalCode, " -", true, throwOnInvalid);
                case "SG" :
                    return new SGPostalCode(postalCode, " -", true, throwOnInvalid);
                case "SI" :
                    return new SIPostalCode(postalCode, " -", true, throwOnInvalid);
                case "SJ" :
                    return new SJPostalCode(postalCode, " -", true, throwOnInvalid);
                case "SK" :
                    return new SKPostalCode(postalCode, " -", true, throwOnInvalid);
                case "SN" :
                    return new SNPostalCode(postalCode, " -", true, throwOnInvalid);
                case "TD" :
                    return new TDPostalCode(postalCode, " -", true, throwOnInvalid);
                case "TH" :
                    return new THPostalCode(postalCode, " -", true, throwOnInvalid);
                case "TJ" :
                    return new TJPostalCode(postalCode, " -", true, throwOnInvalid);
                case "TM" :
                    return new TMPostalCode(postalCode, " -", true, throwOnInvalid);
                case "TN" :
                    return new TNPostalCode(postalCode, " -", true, throwOnInvalid);
                case "TR" :
                    return new TRPostalCode(postalCode, " -", true, throwOnInvalid);
                case "TT" :
                    return new TTPostalCode(postalCode, " -", true, throwOnInvalid);
                case "TW" :
                    return new TWPostalCode(postalCode, " -", true, throwOnInvalid);
                case "UA" :
                    return new UAPostalCode(postalCode, " -", true, throwOnInvalid);
                case "US" :
                    return new USPostalCode(postalCode, " -", true, throwOnInvalid);
                case "UY" :
                    return new UYPostalCode(postalCode, " -", true, throwOnInvalid);
                case "VI" :
                    return new VIPostalCode(postalCode, " -", true, throwOnInvalid);
                case "VN" :
                    return new VNPostalCode(postalCode, " -", true, throwOnInvalid);
                case "YT" :
                    return new YTPostalCode(postalCode, " -", true, throwOnInvalid);
                case "ZA" :
                    return new ZAPostalCode(postalCode, " -", true, throwOnInvalid);
                case "ZM" :
                    return new ZMPostalCode(postalCode, " -", true, throwOnInvalid);
            }

            // Default behavior so far was to just return NumericPostalCode
            return new DefaultPostalCode(postalCode, true, throwOnInvalid);
        }
    }
}


