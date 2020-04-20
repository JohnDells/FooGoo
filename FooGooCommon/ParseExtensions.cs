using System;

namespace FooGooCommon
{
    public static class ParseExtensions
    {
        public static int ToSafeInt(string value)
        {
            int result;
            if (!int.TryParse(value, out result))
            {
                return 0;
            }
            return result;
        }

        public static int? ToSafeNullableInt(string value)
        {
            int result;
            if (!int.TryParse(value, out result))
            {
                return null;
            }
            return result;
        }
    }
}
