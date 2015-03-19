using System;

namespace PostalCodes
{
    public class PostalCodeException : Exception
    {
        // Default constructor
        public PostalCodeException() {}

        // Constructor accepting only a string description
        public PostalCodeException(string message) : base(message) {}
    }
}
