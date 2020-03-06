
namespace AeropuertoApp.Infrastructure.Strings
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !IsNullOrEmpty(value);
        }

        public static bool IsEqualTo(this string value, string valueToCompare)
        {
            return value == valueToCompare;
        }

        public static bool IsNotEqualTo(this string value, string valueToCompare)
        {
            return !IsEqualTo(value, valueToCompare);
        }

        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }

        public static decimal ToDecimal(this string value)
        {
            return decimal.Parse(value);
        }
    }
}