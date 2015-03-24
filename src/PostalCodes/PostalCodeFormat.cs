using System.Text.RegularExpressions;

namespace PostalCodes
{
	/// <summary>
	/// Postal code format.
	/// </summary>
	public sealed class PostalCodeFormat {
		
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; internal set;} 

		/// <summary>
		/// Gets or sets the regex default.
		/// </summary>
		/// <value>The regex default.</value>
		public Regex RegexDefault { get; internal set;}

		/// <summary>
		/// Gets or sets the output default.
		/// </summary>
		/// <value>The output default.</value>
		public string OutputDefault { get; internal set;}

		/// <summary>
		/// Gets or sets the regex short.
		/// </summary>
		/// <value>The regex short.</value>
		public Regex RegexShort { get; internal set;} 

		/// <summary>
		/// Gets or sets the output short.
		/// </summary>
		/// <value>The output short.</value>
		public string OutputShort { get; internal set;}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="PostalCodes.PostalCodeFormat"/> auto convert to short.
		/// </summary>
		/// <value><c>true</c> if auto convert to short; otherwise, <c>false</c>.</value>
		public bool AutoConvertToShort { get; set; }

		/// <summary>
		/// Gets or sets the short expansion as lowest in range.
		/// </summary>
		/// <value>The short expansion as lowest in range.</value>
		public string ShortExpansionAsLowestInRange { get; internal set; }

		/// <summary>
		/// Gets or sets the short expansion as highest in range.
		/// </summary>
		/// <value>The short expansion as highest in range.</value>
		public string ShortExpansionAsHighestInRange { get; internal set; }

		/// <summary>
		/// Gets or sets the pad left character.
		/// </summary>
		/// <value>The pad left character.</value>
		public string LeftPaddingCharacter { get; internal set;}
	}
}

