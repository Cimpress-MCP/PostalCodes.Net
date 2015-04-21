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
                case "FO" :
                    return new FOPostalCode(postalCode);
                case "IS" :
                    return new ISPostalCode(postalCode);
                case "LS" :
                    return new LSPostalCode(postalCode);
                case "MG" :
                    return new MGPostalCode(postalCode);
                case "OM" :
                    return new OMPostalCode(postalCode);
                case "PG" :
                    return new PGPostalCode(postalCode);
                case "PS" :
                    return new PSPostalCode(postalCode);
                case "AF" :
                    return new AFPostalCode(postalCode);
                case "AL" :
                    return new ALPostalCode(postalCode);
                case "AM" :
                    return new AMPostalCode(postalCode);
                case "AR" :
                    return new ARPostalCode(postalCode);
                case "AT" :
                    return new ATPostalCode(postalCode);
                case "AU" :
                    return new AUPostalCode(postalCode);
                case "BD" :
                    return new BDPostalCode(postalCode);
                case "BE" :
                    return new BEPostalCode(postalCode);
                case "BG" :
                    return new BGPostalCode(postalCode);
                case "BO" :
                    return new BOPostalCode(postalCode);
                case "CC" :
                    return new CCPostalCode(postalCode);
                case "CH" :
                    return new CHPostalCode(postalCode);
                case "CV" :
                    return new CVPostalCode(postalCode);
                case "CX" :
                    return new CXPostalCode(postalCode);
                case "CY" :
                    return new CYPostalCode(postalCode);
                case "DK" :
                    return new DKPostalCode(postalCode);
                case "ET" :
                    return new ETPostalCode(postalCode);
                case "GE" :
                    return new GEPostalCode(postalCode);
                case "GL" :
                    return new GLPostalCode(postalCode);
                case "GW" :
                    return new GWPostalCode(postalCode);
                case "HM" :
                    return new HMPostalCode(postalCode);
                case "HT" :
                    return new HTPostalCode(postalCode);
                case "HU" :
                    return new HUPostalCode(postalCode);
                case "LI" :
                    return new LIPostalCode(postalCode);
                case "LR" :
                    return new LRPostalCode(postalCode);
                case "LU" :
                    return new LUPostalCode(postalCode);
                case "MK" :
                    return new MKPostalCode(postalCode);
                case "MZ" :
                    return new MZPostalCode(postalCode);
                case "NE" :
                    return new NEPostalCode(postalCode);
                case "NF" :
                    return new NFPostalCode(postalCode);
                case "NO" :
                    return new NOPostalCode(postalCode);
                case "NZ" :
                    return new NZPostalCode(postalCode);
                case "PH" :
                    return new PHPostalCode(postalCode);
                case "PY" :
                    return new PYPostalCode(postalCode);
                case "SI" :
                    return new SIPostalCode(postalCode);
                case "SJ" :
                    return new SJPostalCode(postalCode);
                case "TN" :
                    return new TNPostalCode(postalCode);
                case "ZA" :
                    return new ZAPostalCode(postalCode);
                case "AS" :
                    return new ASPostalCode(postalCode);
                case "BA" :
                    return new BAPostalCode(postalCode);
                case "BT" :
                    return new BTPostalCode(postalCode);
                case "CR" :
                    return new CRPostalCode(postalCode);
                case "CU" :
                    return new CUPostalCode(postalCode);
                case "CZ" :
                    return new CZPostalCode(postalCode);
                case "DE" :
                    return new DEPostalCode(postalCode);
                case "DO" :
                    return new DOPostalCode(postalCode);
                case "DZ" :
                    return new DZPostalCode(postalCode);
                case "EE" :
                    return new EEPostalCode(postalCode);
                case "EG" :
                    return new EGPostalCode(postalCode);
                case "ES" :
                    return new ESPostalCode(postalCode);
                case "FI" :
                    return new FIPostalCode(postalCode);
                case "FM" :
                    return new FMPostalCode(postalCode);
                case "FR" :
                    return new FRPostalCode(postalCode);
                case "GR" :
                    return new GRPostalCode(postalCode);
                case "GT" :
                    return new GTPostalCode(postalCode);
                case "GU" :
                    return new GUPostalCode(postalCode);
                case "HN" :
                    return new HNPostalCode(postalCode);
                case "HR" :
                    return new HRPostalCode(postalCode);
                case "ID" :
                    return new IDPostalCode(postalCode);
                case "IQ" :
                    return new IQPostalCode(postalCode);
                case "IT" :
                    return new ITPostalCode(postalCode);
                case "JO" :
                    return new JOPostalCode(postalCode);
                case "KE" :
                    return new KEPostalCode(postalCode);
                case "KH" :
                    return new KHPostalCode(postalCode);
                case "KW" :
                    return new KWPostalCode(postalCode);
                case "LA" :
                    return new LAPostalCode(postalCode);
                case "LK" :
                    return new LKPostalCode(postalCode);
                case "LY" :
                    return new LYPostalCode(postalCode);
                case "MA" :
                    return new MAPostalCode(postalCode);
                case "MC" :
                    return new MCPostalCode(postalCode);
                case "ME" :
                    return new MEPostalCode(postalCode);
                case "MH" :
                    return new MHPostalCode(postalCode);
                case "MM" :
                    return new MMPostalCode(postalCode);
                case "MN" :
                    return new MNPostalCode(postalCode);
                case "MP" :
                    return new MPPostalCode(postalCode);
                case "MQ" :
                    return new MQPostalCode(postalCode);
                case "MU" :
                    return new MUPostalCode(postalCode);
                case "MX" :
                    return new MXPostalCode(postalCode);
                case "MY" :
                    return new MYPostalCode(postalCode);
                case "NA" :
                    return new NAPostalCode(postalCode);
                case "NI" :
                    return new NIPostalCode(postalCode);
                case "NP" :
                    return new NPPostalCode(postalCode);
                case "PE" :
                    return new PEPostalCode(postalCode);
                case "PK" :
                    return new PKPostalCode(postalCode);
                case "PR" :
                    return new PRPostalCode(postalCode);
                case "PW" :
                    return new PWPostalCode(postalCode);
                case "RS" :
                    return new RSPostalCode(postalCode);
                case "SD" :
                    return new SDPostalCode(postalCode);
                case "SE" :
                    return new SEPostalCode(postalCode);
                case "SK" :
                    return new SKPostalCode(postalCode);
                case "SN" :
                    return new SNPostalCode(postalCode);
                case "TD" :
                    return new TDPostalCode(postalCode);
                case "TH" :
                    return new THPostalCode(postalCode);
                case "TR" :
                    return new TRPostalCode(postalCode);
                case "TW" :
                    return new TWPostalCode(postalCode);
                case "UA" :
                    return new UAPostalCode(postalCode);
                case "US" :
                    return new USPostalCode(postalCode);
                case "UY" :
                    return new UYPostalCode(postalCode);
                case "VI" :
                    return new VIPostalCode(postalCode);
                case "YT" :
                    return new YTPostalCode(postalCode);
                case "ZM" :
                    return new ZMPostalCode(postalCode);
                case "BY" :
                    return new BYPostalCode(postalCode);
                case "CN" :
                    return new CNPostalCode(postalCode);
                case "CO" :
                    return new COPostalCode(postalCode);
                case "EC" :
                    return new ECPostalCode(postalCode);
                case "IN" :
                    return new INPostalCode(postalCode);
                case "KG" :
                    return new KGPostalCode(postalCode);
                case "KZ" :
                    return new KZPostalCode(postalCode);
                case "NG" :
                    return new NGPostalCode(postalCode);
                case "PA" :
                    return new PAPostalCode(postalCode);
                case "RO" :
                    return new ROPostalCode(postalCode);
                case "RU" :
                    return new RUPostalCode(postalCode);
                case "SG" :
                    return new SGPostalCode(postalCode);
                case "TJ" :
                    return new TJPostalCode(postalCode);
                case "TM" :
                    return new TMPostalCode(postalCode);
                case "TT" :
                    return new TTPostalCode(postalCode);
                case "VN" :
                    return new VNPostalCode(postalCode);
                case "IL" :
                    return new ILPostalCode(postalCode);
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
            }

            // Default behavior so far was to just return NumericPostalCode
            return new DefaultPostalCode(postalCode);
        }
    }
}


