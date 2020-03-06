using System.Collections.Generic;
using System.Linq;

namespace AeropuertoApp.Infrastructure.Objects
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object value)
        {
            return value == null;
        }

        public static bool IsNotNull(this object value)
        {
            return !IsNull(value);
        }

        public static Dictionary<string, string> ConvertToDictionary(this object value)
        {
            if (value.IsNull())
                return null;

            var dictionary = (from x in value.GetType().GetProperties() select x).ToDictionary(x => x.Name,
                x => (x.GetGetMethod().Invoke(value, null) == null ? "" : x.GetGetMethod().Invoke(value, null).ToString()));
            return dictionary;
        }
    }
}