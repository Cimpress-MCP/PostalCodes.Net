namespace PostalCodes
{
    /// <summary>
    /// Options for a <see cref="PostalCodeFactory"/> created with <see cref="PostalCodeFactory(PostalCodeFactoryOptions)"/>
    /// </summary>
    public class PostalCodeFactoryOptions
    {
        /// <summary>
        /// Deprecated: accepts Netherlands postal codes that do not exist. Only for consumers migrating from 3.0; will
        /// be removed in a future major version.
        /// <para>
        /// Validate Netherlands postal codes with the pre-3.1 rules: accept a leading zero ("0024 AB"), the letter pairs
        /// SA, SD and SS, and left pad short input with zeros ("24" becomes "0024"). Defaults to false.
        /// </para>
        /// <para>
        /// Create all Netherlands postal codes of an application with one factory. Codes from a factory with this option
        /// are of a different type than codes from <see cref="PostalCodeFactory.Instance"/>, so a
        /// <see cref="PostalCodeRange"/> mixing the two throws <see cref="System.ArgumentException"/>.
        /// </para>
        /// </summary>
        public bool UseLegacyNetherlandsFormat { get; set; }
    }
}
