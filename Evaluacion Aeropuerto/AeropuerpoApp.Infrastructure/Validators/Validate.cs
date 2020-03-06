using System;
using System.Linq;

namespace AeropuertoApp.Infrastructure.Validators
{
    public static class Validate<T>
    {
        public static bool Property(string property)
        {
            var myType = typeof(T);
            var propert = myType.GetProperties();
            return propert.Any(prop => prop.Name.Equals(property, StringComparison.OrdinalIgnoreCase));
        }
    }
}