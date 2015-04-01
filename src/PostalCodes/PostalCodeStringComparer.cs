using System;

namespace PostalCodes
{
    internal class PostalCodeStringComparer : StringComparer
    {
        private static readonly Lazy<PostalCodeStringComparer> LazyComparer = 
            new Lazy<PostalCodeStringComparer>(() => new PostalCodeStringComparer());

        public static StringComparer Default
        {
            get { return LazyComparer.Value; }
        }

        #region Overrides of StringComparer

        public override int Compare(string x, string y)
        {
            return OrdinalIgnoreCase.Compare(x, y);
        }

        public override bool Equals(string x, string y)
        {
            return OrdinalIgnoreCase.Equals(x, y);
        }

        public override int GetHashCode(string obj)
        {
            return OrdinalIgnoreCase.GetHashCode(obj);
        }

        #endregion
    }
}
