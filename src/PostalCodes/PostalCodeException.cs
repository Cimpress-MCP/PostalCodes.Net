using System;

namespace PostalCodes
{
    /// <summary>
    /// Class PostalCodeException.
    /// </summary>
    public class PostalCodeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostalCodeException"/> class.
        /// Default constructor
        /// </summary>
        public PostalCodeException() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PostalCodeException" /> class with a specified error message.
        /// Constructor accepting only a string description
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PostalCodeException(string message) : base(message) {}
    }
}
